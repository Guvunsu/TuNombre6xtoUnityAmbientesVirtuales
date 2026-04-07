using UnityEngine;

public class OpenTheDoor : MonoBehaviour
{
    [Header("Configuración de botones")]
    public string[] requiredButtonTags;
    private bool[] buttonStates;

    [Header("Puerta")]
    public GameObject door;
    private bool isUnlocked = false;

    void Start()
    {
        buttonStates = new bool[requiredButtonTags.Length];
    }
    public bool IsUnlocked() //mi getter lo uso para DoorPanelUI
    {
        return isUnlocked;
    }
    public void ButtonPressed(string tag)
    {
        for (int i = 0; i < requiredButtonTags.Length; i++)
        {
            if (requiredButtonTags[i] == tag)
            {
                buttonStates[i] = true;
            }
        }
        CheckUnlock();
    }
    private void CheckUnlock()
    {
        foreach (bool state in buttonStates)
        {
            if (!state) return;
        }
        UnlockDoor();
    }

    private void UnlockDoor()
    {
        if (isUnlocked) return;

        isUnlocked = true;
        Rigidbody rb = door.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // libera la física
            rb.useGravity = true;   // asegura que la gravedad esté activa
        }
    }

    //private void OnCollisionEnter(Collision other) //pienso en hacerlo trigger 
    //{
    //    // Cuando el jugador choca con la puerta, si está desbloqueada, la puerta cae
    //    if (isUnlocked && other.gameObject.CompareTag("Player"))
    //    {
    //        Rigidbody rb = door.GetComponent<Rigidbody>();
    //        if (rb != null) rb.isKinematic = false; // Activa la física para que caiga
    //    }
    //}
}
