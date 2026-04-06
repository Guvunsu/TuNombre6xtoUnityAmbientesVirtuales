using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] private OpenTheDoor doorScript;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            doorScript.ActivateButton(gameObject.tag);
        }
    }
}