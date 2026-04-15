using UnityEngine;

public class Meteorite : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")){
            other.gameObject.SetActive(false);
        }
    }
}
