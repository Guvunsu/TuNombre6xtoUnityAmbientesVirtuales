using UnityEngine;

public class OpenTheDoor : MonoBehaviour
{
    [SerializeField] private GameObject _PushButton;
    [SerializeField] private GameObject _PushButton2;
    void Start()
    {

    }
    void Update()
    {

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Button") && gameObject.CompareTag("Button2"))
        {
            _PushButton.SetActive(true);
            _PushButton2.SetActive(true);
        }
    }
}
