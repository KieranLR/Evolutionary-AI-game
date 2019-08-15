using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour {
    public Slider volumeSlider;
    public Text text;
    public void Start() {
        volumeSlider.value = .75f;
        text.text = "75%";
    }
    public void UpdateText() {
        text.text = Mathf.Floor(volumeSlider.value * 100f) + "%";
    }
}
