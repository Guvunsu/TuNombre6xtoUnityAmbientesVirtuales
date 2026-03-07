using UnityEngine;

public class SableInteractor : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Bala"))
        {
            Destroy(col.gameObject);
        }
    }
}
