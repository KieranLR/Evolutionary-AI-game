using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Adaptable {
    public string name;
    public string condition;
    public string cSymbol;
    public float cValue;
    public string response;
}

[System.Serializable]
public class AdaptableData {
    public Adaptable[] adaptables;
}
