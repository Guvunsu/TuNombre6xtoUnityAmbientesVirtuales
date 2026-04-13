using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class XRButtonBridge : MonoBehaviour
{
    public bool estaPresionado = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            estaPresionado = true;
            Debug.Log($"{gameObject.name} presionado");
        }
    }
}