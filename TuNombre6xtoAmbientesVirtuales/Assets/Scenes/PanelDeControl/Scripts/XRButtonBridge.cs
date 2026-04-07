using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class XRButtonBridge : MonoBehaviour
{
    public OpenTheDoor manager;
    private XRPushButton pushButton;

    void Awake()
    {
        pushButton = GetComponent<XRPushButton>();
        if (pushButton != null)
        {
            pushButton.onPress.AddListener(OnButtonPressed);
        }
    }
    private void OnButtonPressed()
    {
        manager.ButtonPressed(gameObject.tag);
        Debug.Log("Botón presionado: " + gameObject.tag);
    }
}
