using UnityEngine;

public class OpenTheDoor : MonoBehaviour
{
    [SerializeField] private Transform door;
    [SerializeField] private float openHeight = 3f;
    [SerializeField] private float speed = 2f;

    private bool button1Pressed = false;
    private bool button2Pressed = false;

    private Vector3 closedPosition;
    private Vector3 openPosition;

    void Start()
    {
        closedPosition = door.position;
        openPosition = closedPosition + new Vector3(0, openHeight, 0);
    }

    void Update()
    {
        if (button1Pressed && button2Pressed)
        {
            door.position = Vector3.Lerp(door.position, openPosition, Time.deltaTime * speed);
        }
    }
    public void ActivateButton(string buttonTag)
    {
        if (buttonTag == "Button")
        {
            button1Pressed = true;
        }
        else if (buttonTag == "Button2")
        {
            button2Pressed = true;
        }
    }
}