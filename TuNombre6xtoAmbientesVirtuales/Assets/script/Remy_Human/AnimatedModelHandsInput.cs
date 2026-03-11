using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
[System.Serializable]
public class animatorInput
{
    public string _animatonParametersName;
    public InputActionProperty _actionProperty;
}
public class AnimatedModelHandsInput : MonoBehaviour
{
    Animator _animator;
    [SerializeField] List<animatorInput> _listAnimationInputs;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        foreach (animatorInput input in _listAnimationInputs)
        {
            float actionValue = input._actionProperty.action.ReadValue<float>();
            _animator.SetFloat(input._animatonParametersName, actionValue);
        }
    }
}
