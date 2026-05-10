using UnityEngine;

public class XRButtonReadyFood : MonoBehaviour
{
    public enum FoodState
    {
        TORTILLAS,
        FRIJOLES,
        CARNE,
        QUESILLO,
        CEBOLLA,
        TOMNATE,
        SALSAROJA,
        AGUACATE,
        COMPLETA
    }

    [Header("Estado Actual")]
    public FoodState currentState;

    [Header("Modelos o Etapas")]
    public GameObject[] foodStages;

    [Header("FX Finales")]
    public ParticleSystem fireParticles;
    public ParticleSystem vaporParticles;

    [Header("Canvas Final")]
    public GameObject finalCanvas;

    [Header("Delivery")]
    public XRDeliveryFood deliverySystem;

    private int currentIndex = 0;

    void Start()
    {
        UpdateFoodVisual();

        if (finalCanvas != null)
            finalCanvas.SetActive(false);

        if (fireParticles != null)
            fireParticles.gameObject.SetActive(false);

        if (vaporParticles != null)
            vaporParticles.gameObject.SetActive(false);
    }

    //BOTÓN XR
    public void NextFoodState()
    {
        currentIndex++;

        if (currentIndex >= foodStages.Length)
        {
            currentIndex = foodStages.Length - 1;

            currentState = FoodState.COMPLETA;

            ActivateFinalEffects();

            if (deliverySystem != null)
                deliverySystem.canDeliver = true;

            return;
        }

        currentState = (FoodState)currentIndex;

        UpdateFoodVisual();
    }

    void UpdateFoodVisual()
    {
        for (int i = 0; i < foodStages.Length; i++)
        {
            foodStages[i].SetActive(i == currentIndex);
        }
    }

    void ActivateFinalEffects()
    {
        if (finalCanvas != null)
            finalCanvas.SetActive(true);

        if (fireParticles != null)
        {
            fireParticles.gameObject.SetActive(true);
            fireParticles.Play();
        }

        if (vaporParticles != null)
        {
            vaporParticles.gameObject.SetActive(true);
            vaporParticles.Play();
        }
    }
}