using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ChangeColor : XRGrabInteractable
{
    [SerializeField] Renderer _renderer;
    [SerializeField] Color _originalColor;
    [SerializeField] Color _changeColor;

    protected override void OnSelectEntered(SelectEnterEventArgs rarw)
    {
        base.OnSelectEntered(rarw);
        _originalColor = _changeColor;
    }
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        _originalColor = _changeColor;
    }
}
