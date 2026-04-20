using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class XRButtonBridge : MonoBehaviour
{
    public bool estaPresionado = false;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{gameObject.name} lo toque?");
        if (collision.gameObject.CompareTag("Untagged"))
        {
            estaPresionado = true;
            Debug.Log($"{gameObject.name} presionado");
        }
    }
}