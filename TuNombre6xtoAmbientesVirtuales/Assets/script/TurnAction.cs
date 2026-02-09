using UnityEngine;
using UnityEngine.InputSystem;

public class TurnAction : MonoBehaviour
{// debug.log("[NombreScript,variable]"+variable);
    [Header("Referencia Input")]
    [Tooltip("Variable para activar objecto")]
    [SerializeField] InputActionProperty _actionValue;

    [Header("Objecto Para Activar")]
    [SerializeField] GameObject _objectActivate;
    void Start()
    {
        _objectActivate.SetActive(false);

    }
    void Update()
    {
        bool _buttom = _actionValue.action.IsPressed();
        _objectActivate.SetActive(_buttom);
        Debug.Log("[TurnAction,_buttom]" + _buttom);
    }
}
