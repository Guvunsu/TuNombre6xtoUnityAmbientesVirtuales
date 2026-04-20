using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Content.Interaction;

public class ButtonManager : MonoBehaviour {
    [SerializeField] private List<GameObject> buttons;
    [SerializeField] private List<GameObject> lightbulbs;
    [SerializeField] private List<GameObject> activeButtons;
    [SerializeField] private Material[] material;


    public List<GameObject> Buttons {
        get { return buttons; }
        private set { buttons = value; }
    }
    public List<GameObject> ActiveButtons {
        get { return activeButtons; }
        set { activeButtons = value; }
    }
    public List<GameObject> Lightbulbs {
        get { return lightbulbs; }
        private set { lightbulbs = value; }
    }
    public Material[] Materials{
        get { return material; }
        private set { material = value; }
    }

}
