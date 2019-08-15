using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Actions : MonoBehaviour {
    public Dictionary<string, Action> actions;
    // Use this for initialization
    void Start () {
        actions = new Dictionary<string, Action>();
        actions.Add("FireMania", () => FireMania("FireMania"));
        actions.Add("Grind", () => Grind("Grind"));
	}

    public void FireMania(string name) {
        Debug.Log("I Used FireMania!!!");
    }

    public void Grind(string name) {
        Debug.Log(AdaptableManager.aInstance.GetAdaptable(name).response);
    }

    private void Update() {
        //Activate an activatable by doing: actions["ActivatableName"]();
    }
}
