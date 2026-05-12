using UnityEngine;

public class XRDeliveryFood : MonoBehaviour
{
    [Header("Tlayuda")]
    public GameObject foodObject;

    [Header("Punto de Entrega")]
    public Transform deliveryPoint;

    [Header("Distancia para Entregar")]
    public float deliveryDistance = 0.25f;

    [Header("Canvas UI")]
    public GameObject deliveryCanvas;

    [Header("FX")]
    public ParticleSystem deliveryParticles;

    [HideInInspector]
    public bool canDeliver = false;

    private bool delivered = false;

    void Start()
    {
        if (deliveryCanvas != null)
            deliveryCanvas.SetActive(false);

        if (deliveryParticles != null)
            deliveryParticles.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!canDeliver || delivered)
            return;

        if (foodObject == null || deliveryPoint == null)
            return;

        float distance = Vector3.Distance(
            foodObject.transform.position,
            deliveryPoint.position
        );

        if (distance <= deliveryDistance)
        {
            DeliverFood();
        }
    }

    void DeliverFood()
    {
        delivered = true;

        Debug.Log("Tlayuda Entregada");

        if (deliveryCanvas != null)
            deliveryCanvas.SetActive(true);

        if (deliveryParticles != null)
        {
            deliveryParticles.gameObject.SetActive(true);
            deliveryParticles.Play();
        }

        if (foodObject != null)
        {
            foodObject.SetActive(false);
        }
    }
}