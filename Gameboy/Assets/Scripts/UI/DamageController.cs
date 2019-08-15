using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageController : MonoBehaviour {
    public GameObject text;
    // Use this for initialization
    public void Start() {
        DontDestroyOnLoad(this);
        //SpawnText(12);
    }
    public void SpawnText(int damage, Vector3 position) {
        //Debug.Log("I spawned a text");
        GameObject t = Instantiate(text);
        t.transform.SetParent(transform);
        t.transform.position = Camera.main.WorldToScreenPoint(position);
        t.GetComponent<DamgeNumber>().position = position;
        t.GetComponent<TextMeshProUGUI>().SetText(damage.ToString());
    }
}
