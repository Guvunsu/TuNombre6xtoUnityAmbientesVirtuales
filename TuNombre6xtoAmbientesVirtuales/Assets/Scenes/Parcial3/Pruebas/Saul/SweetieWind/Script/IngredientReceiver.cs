using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using TPDev;
using UnityEngine.UIElements;

public class IngredientReceiver : MonoBehaviour
{
    public TlayudaManager manager;

    public List<TipoIngrediente> tiposPermitidos;

    public TipoIngrediente ultimoIngrediente;


    public void OnIngredientPlaced(SelectEnterEventArgs args)
    {
        GameObject obj = args.interactableObject.transform.gameObject;
        IngredientClass ingrediente = obj.GetComponentInChildren<IngredientClass>();
        if (ingrediente == null) return;
        XRSocketInteractor socket = GetComponent<XRSocketInteractor>();

        //Check if the ingredient is permitted no se porque los hago en spanglish se me va mucho la onda jajaja
        if (!tiposPermitidos.Contains(ingrediente.tipo))
        {

            if (socket != null)
            {
                socket.interactionManager.SelectExit(socket, args.interactableObject);
            }

            return;
        }
        if (manager != null)
        {
            manager.ingredientToAdd = ConvertToManagerType(ingrediente.tipo);
            manager.AddIngredient();
        }

        ultimoIngrediente = ingrediente.tipo;

        //This logic scales the "tortilla" jaajaj
        //TPGG: no ocupavas escalarla, mi logica ya lo hacia, por eso quedaba extremadamente chica la tortilla
        //TPGG: solo cambie para que lo setee al tamaño de tu tortilla grande y la logica de tlayudamaneager haga el resto
        Vector3 scale = obj.transform.localScale;
        if (manager.TlayudaIngredientList.Contains(IngredientType.MediumTortilla))
        {
            obj.transform.localScale = new Vector3(1f, 0.03f, 1f);
        }
        else if (manager.TlayudaIngredientList.Contains(IngredientType.SmallTortilla))
        {
            obj.transform.localScale = new Vector3(1f, 0.03f, 1f);
        }
        //EndOFlOGIC

        Transform attach = args.interactorObject.GetAttachTransform(args.interactableObject);

        //puesto para que el objeto se haga hijo TPGG pa que se mueva con la tlayuda
        if (socket != null)
        {
            socket.interactionManager.SelectExit(socket, args.interactableObject);
        }

        //puesto para que el objeto se haga hijo TPGG pa que se mueva con la tlayuda
        //los ingredientes que no son frijoles o tortillas no los hago hijos sino hermanos del objeto, porque los colliders estan distorcionados y pues se jode
        if (ingrediente.tipo == TipoIngrediente.FrijolesNormales ||
            ingrediente.tipo == TipoIngrediente.FrijolesBayos ||
            ingrediente.tipo == TipoIngrediente.FrijolesPuercos ||
            ingrediente.tipo == TipoIngrediente.TortillaChica ||
            ingrediente.tipo == TipoIngrediente.TortillaMediana ||
            ingrediente.tipo == TipoIngrediente.TortillaGrande)
        {
            obj.transform.SetParent(this.transform, false);
        }
        else
        {
            obj.transform.SetParent(this.transform.parent, false);
        }

        obj.transform.position = attach.position;
        obj.transform.rotation = attach.rotation;

        XRGrabInteractable grab = obj.GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            grab.enabled = false;
        }

        Collider col = obj.GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = true;
        }

        // Remove physics
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Turn off socket to avoid more interactions
        if (socket != null)
        {
            socket.enabled = false;
        }

    }
    private bool HasTortilla()
    {
        return manager != null && manager.TlayudaIngredientList.Contains(IngredientType.SmallTortilla)
            || manager.TlayudaIngredientList.Contains(IngredientType.MediumTortilla)
            || manager.TlayudaIngredientList.Contains(IngredientType.BigTortilla);
    }
    private IngredientType ConvertToManagerType(TipoIngrediente tipo)
    {
        Debug.Log("ConvertToManagerType recibido: " + tipo);

        IngredientType resultado;

        switch (tipo)
        {
            case TipoIngrediente.TortillaChica: resultado = IngredientType.SmallTortilla; break;
            case TipoIngrediente.TortillaMediana: resultado = IngredientType.MediumTortilla; break;
            case TipoIngrediente.TortillaGrande: resultado = IngredientType.BigTortilla; break;

            case TipoIngrediente.FrijolesNormales: resultado = IngredientType.NormalBeans; break;
            case TipoIngrediente.FrijolesBayos: resultado = IngredientType.Bayos; break;
            case TipoIngrediente.FrijolesPuercos: resultado = IngredientType.Puercos; break;

            case TipoIngrediente.Carne: resultado = IngredientType.Carne; break;
            case TipoIngrediente.Lechuga: resultado = IngredientType.Lechuga; break;
            case TipoIngrediente.Quesillo: resultado = IngredientType.Quesillo; break;
            case TipoIngrediente.Cebolla: resultado = IngredientType.Cebolla; break;
            case TipoIngrediente.Tomate: resultado = IngredientType.Tomate; break;
            case TipoIngrediente.Salsa: resultado = IngredientType.SalsaRoja; break;
            case TipoIngrediente.Aguacate: resultado = IngredientType.Aguacate; break;

            default:
                resultado = IngredientType.None;
                Debug.LogWarning("Tipo no reconocido: " + tipo);
                break;
        }

        Debug.Log("Convertido a: " + resultado);
        return resultado;
    }
}