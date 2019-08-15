using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class AdaptableManager : MonoBehaviour {
    public Dictionary<string, Adaptable> adaptables;
    public static AdaptableManager aInstance;
    private bool isReady = false;
    private float missingVarValue = -1f;

    private void Awake() {
        if (aInstance == null) {
            aInstance = this;
        } 
        else if (aInstance != this) {
            Destroy(gameObject);
        }
        adaptables = new Dictionary<string, Adaptable>();
    }

    private void Start() {
        LoadData("Adaptables.json");

    }

    public void Update() {;
    }

    public void LoadData(string fileName) {
        adaptables = new Dictionary<string, Adaptable>();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath)) {
            string dataAsJson = File.ReadAllText(filePath);
            AdaptableData loadedData = JsonUtility.FromJson<AdaptableData>(dataAsJson);

            for (int i = 0; i < loadedData.adaptables.Length; i++) 
            {
                adaptables.Add(loadedData.adaptables[i].name, loadedData.adaptables[i]);
            }

            Debug.Log("Data loaded from " + fileName + ", database contains: " + adaptables.Count + " entries");
        }
        else {
            Debug.LogError("Cannot find file!");
        }

        isReady = true;
    }
    // Gets an adaptable from a string
    public Adaptable GetAdaptable(string key) {
        Adaptable result = null;
        if (adaptables.ContainsKey(key)) {
            adaptables.TryGetValue(key, out result);
        }
        else {
            Debug.Log("Error: Adaptable Not Found: " + key);
        }

        return result;

    }


    public bool GetIsReady() {
        return isReady;
    }

}
