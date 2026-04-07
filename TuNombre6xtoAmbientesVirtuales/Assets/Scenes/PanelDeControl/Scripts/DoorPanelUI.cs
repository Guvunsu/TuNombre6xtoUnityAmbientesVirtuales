using UnityEngine;
using TMPro;

public class DoorPanelUI : MonoBehaviour
{
    [Header("Referencias")]
    public OpenTheDoor doorManager;
    public TMP_Text closedText;     
    public TMP_Text openText;      

    void Update()
    {
        if (doorManager == null) return;
        if (doorManager.IsUnlocked())
        {
            openText.gameObject.SetActive(true);
            closedText.gameObject.SetActive(false);
        }
        else
        {
            closedText.gameObject.SetActive(true);
            openText.gameObject.SetActive(false);
        }
    }
}