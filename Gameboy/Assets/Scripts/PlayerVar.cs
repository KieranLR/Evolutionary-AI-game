using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    public PlayerVar[] playervars;
}


[System.Serializable]
public class PlayerVar {
    public string name;
    public float value;
    public string[] adaptables;
}
