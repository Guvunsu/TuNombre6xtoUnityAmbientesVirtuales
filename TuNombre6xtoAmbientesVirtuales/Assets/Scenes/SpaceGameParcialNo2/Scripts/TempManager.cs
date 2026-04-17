using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using static UnityEngine.Rendering.DebugUI;


[Serializable]
enum States {
    None,
    Stable,
    Unstable,
    Exploded
}

public class TempManager : MonoBehaviour {
    [SerializeField, Range(0f, 100f)] private float temp;
    [SerializeField] private float initialTemp;
    [SerializeField] private float threshold;
    [SerializeField] private TextMeshPro tempTextIndicator;
    [SerializeField] private ButtonManager buttonManager;
    [SerializeField] private States states;
    [SerializeField, Range(0f, 120f)] private float timer = 120f;
    [SerializeField, Range(0f, 5f)] private float timer2 = 5f;

    private void Start() {
        StartCoroutine(StartGame());
    }

    private void Update() {
        timer -= Mathf.Round(Time.deltaTime * 100f) / 100f;
        temp += Time.deltaTime * 0.5f;
        tempTextIndicator.text = temp.ToString("F2");
        if (timer == 0 && temp < 100) {
            states = States.Stable;
            temp = 0;
            tempTextIndicator.text = "You Win";
        }
        if (temp >= 100f) {
            timer2 -= Time.deltaTime;
            if (timer2 <= 0) {
                states = States.Exploded;
                tempTextIndicator.text = "You lose";
            }
        }
    }

    public void ChangeTemp(GameObject correctButton) {
        if (!buttonManager.ActiveButtons.Contains(correctButton)) {
            return;
        }
        if (correctButton.GetComponent<XRSlider>()) {
            if (correctButton.GetComponent<XRSlider>().value > threshold) {

                BulbFixed(correctButton);
                if (temp > 25) {
                    temp -= 5f;
                }
            }
        } else if (correctButton.GetComponent<XRKnob>()) {
            if (correctButton.GetComponent<XRKnob>().value > threshold) {

                if (temp > 25) {
                    temp -= 5f;
                }

                BulbFixed(correctButton);
            }
        } else {
            BulbFixed(correctButton);
            if ((temp -= 20f) > 25) {
                temp -= 20f;
            } else {
                temp = 25f;
            }

        }
    }

    public void SetSliderValue(XRSlider slider) {
        slider.value = 0f;
    }
    public void SetSliderValue(XRKnob knob) {
        knob.value = 0f;
    }

    void BulbFixed(GameObject button) {
        int buttonIndex = buttonManager.Buttons.IndexOf(button);
        if (timer > 0) {
            buttonManager.Lightbulbs[buttonIndex].GetComponent<Renderer>().material = buttonManager.Materials[1];
            buttonManager.Lightbulbs[buttonIndex].GetComponent<Lightbulb>().active = false;
            buttonManager.ActiveButtons.Remove(button);
            int newbutton;
#if UNITY_EDITOR
            print($"{button} is no longer active");
#endif
            do {
                newbutton = UnityEngine.Random.Range(0, buttonManager.Buttons.Count);
            } while (newbutton == buttonIndex || buttonManager.ActiveButtons.Contains(buttonManager.Buttons[newbutton]));
#if UNITY_EDITOR
            print($"{newbutton}, {buttonManager.Buttons[newbutton].gameObject}");
#endif
            buttonManager.Lightbulbs[newbutton].GetComponent<Renderer>().material = buttonManager.Materials[0];
            buttonManager.Lightbulbs[newbutton].GetComponent<Lightbulb>().active = true;
            buttonManager.ActiveButtons.Add(buttonManager.Buttons[newbutton].gameObject);
        }
    }

    IEnumerator StartGame() {
        states = States.Unstable;
        float value = 5f;
        while (value >= 0) {
            int objectID = UnityEngine.Random.Range(1, buttonManager.Lightbulbs.Count);
            buttonManager.Lightbulbs[objectID].GetComponent<Renderer>().material = buttonManager.Materials[0];
            buttonManager.Lightbulbs[objectID].GetComponent<Lightbulb>().active = true;
            if (!buttonManager.ActiveButtons.Contains(buttonManager.Buttons[objectID])) {
#if UNITY_EDITOR
                buttonManager.Lightbulbs[objectID].GetComponent<Lightbulb>().Debug();
#endif
                buttonManager.ActiveButtons.Add(buttonManager.Buttons[objectID].gameObject);
                value--;
            }
        }

        initialTemp = UnityEngine.Random.Range(25, 40);
        temp = initialTemp;

        yield return null;
    }
}
