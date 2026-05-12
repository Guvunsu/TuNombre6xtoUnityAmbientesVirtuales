using UnityEngine;
using UnityEngine.InputSystem;
public class TurnOnActionj : MonoBehaviour
{
    [Header("Reference Input")]
    [Tooltip("Variable para activar objeto")]
    [SerializeField] InputActionProperty _inputActionValue;

    [Header("Objeto para activar")]
    [SerializeField] GameObject _objectToActivate;

    private bool _button;
    void Start()
    {
        _objectToActivate.SetActive(false);
        _button = false;
    }

    // Update is called once per frame
    void Update()
    {
        _button = _inputActionValue.action.IsPressed();   
        _objectToActivate.SetActive(_button);
       // print("[TunButton, _button" + _button);
    }
}
