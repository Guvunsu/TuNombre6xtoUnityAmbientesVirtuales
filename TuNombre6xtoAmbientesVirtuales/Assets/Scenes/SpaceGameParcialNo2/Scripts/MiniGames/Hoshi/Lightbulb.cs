using UnityEngine;

public class Lightbulb : MonoBehaviour {
    [SerializeField] public bool active;
    [SerializeField] private ButtonManager buttonManager;

    public void Debug() {
        print($"{name} bool is active: {active}");
    }

}
