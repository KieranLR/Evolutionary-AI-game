using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerVarManager : MonoBehaviour {
    public static PlayerVarManager instance;

    private Dictionary<string, float> playervars;
    private bool isReady = false;
    private float missingVarValue = -1f;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } 
        else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        LoadData("PlayerVars.json");
    }

    public void LoadData(string fileName) {
        playervars = new Dictionary<string, float>();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath)) {
            string dataAsJson = File.ReadAllText(filePath);
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(dataAsJson);

            for (int i = 0; i < loadedData.playervars.Length; i++) 
            {
                playervars.Add(loadedData.playervars[i].name, loadedData.playervars[i].value);
            }

            Debug.Log("Data loaded from " + fileName + ", database contains: " + playervars.Count + " entries");
        }
        else {
            Debug.LogError("Cannot find file!");
        }

        isReady = true;
    }

    public float GetVar(string key) {
        float result = missingVarValue;
        if (playervars.ContainsKey(key)) {
            playervars.TryGetValue(key, out result);
        }

        return result;

    }

    public void SetVar(string key, float value) {
        if (playervars.ContainsKey(key)) {
            playervars.Remove(key);
            playervars.Add(key, value);
        }
    }

    public bool GetIsReady() {
        return isReady;
    }

}
