using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;
public class OpenTheDoor : MonoBehaviour
{
    [Header("Botones requeridos")]
    public List<XRButtonBridge> botones;

    [Header("Puerta a liberar")]
    public XRGrabInteractable puerta;
    public List<HingeJoint> hinges;

    private bool puertaLiberada = false;

    void Start()
    {
        SetHingesActive(true);
        if (puerta != null)
            puerta.enabled = false;
    }
    void Update()
    {
        VerifyButtons();
    }
   public void VerifyButtons()
    {
        int presionados = 0;

        for (int i = 0; i < botones.Count; i++)
        {
            if (botones[i] != null && botones[i].estaPresionado)
                presionados++;
        }
        if (presionados == botones.Count && !puertaLiberada)
        {
            DoorLiberty();
        }
    }
    void DoorLiberty()
    {
        puertaLiberada = false;
        SetHingesActive(true);

        if (puerta != null)
            puerta.enabled = true;

        Debug.Log("Puerta liberada!");
    }
    void SetHingesActive(bool active)
    {
        foreach (var hinge in hinges)
        {
            if (hinge != null)
                hinge.gameObject.SetActive(active);
        }
    }
}