using UnityEngine;

public class WinConditionStarWars : MonoBehaviour
{
    public GameObject winPanelActivation;
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("RedSable") || col.gameObject.CompareTag("GreenSable"))
        {
            winPanelActivation.SetActive(true);
        }
    }
}
