using TPDev;
using UnityEngine;
public class OvenShapeshifterDeliveryManager : MonoBehaviour
{
    public enum OvenMode
    {
        OVEN,
        DELIVERY
    }
    public OvenMode currentMode = OvenMode.OVEN;

    [Header("Referencias")]
    [SerializeField] private OrderManager orderManager;
    [SerializeField] private MoneyManager moneyManager; 
    private TlayudaManager currentTlayuda;

    [Header("Dinero")]
    [SerializeField] private float deliveryMoney = 50f;

    // BOTÓN XR 1 
    public void ChangeToDeliveryMode()
    {
        currentMode = OvenMode.DELIVERY;
        Debug.Log("Modo DELIVERY activado");
    }
    // BOTÓN XR 2
    public void ChangeToOvenMode()
    {
        currentMode = OvenMode.OVEN;
        Debug.Log("Modo OVEN activado");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out TlayudaManager tlayuda)) return;

        currentTlayuda = tlayuda;

        if (currentMode == OvenMode.OVEN)
        {
            Debug.Log("Tlayuda lista en horno");
        } else if (currentMode == OvenMode.DELIVERY)
        {
            DeliverTlayuda(tlayuda);
        }
    }

    void DeliverTlayuda(TlayudaManager tlayuda)
    {
        Debug.Log("Intento entregar tlayuda");

        if (orderManager != null)
        {
            orderManager.CheckOrdenes();
        }

        if (moneyManager != null)
        {
            moneyManager.GiveMoney(deliveryMoney);
            Debug.Log("Dinero entregado: " + deliveryMoney);
        }
        tlayuda.TlayudaIngredientList.Clear();

        ResetSystem();
    }
    void ResetSystem()
    {
        currentTlayuda = null;
        currentMode = OvenMode.OVEN;
        Debug.Log("Sistema reiniciado");
    }
}