using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamgeNumber : MonoBehaviour {
    public TextMeshProUGUI text;
    private Color color;
    public Vector3 position { get; set; }
	// Use this for initialization
	void Start () {
        text = GetComponent<TextMeshProUGUI>();
        color = text.color;
	}

    // Update is called once per frame
    void Update() {
        color.a -= (Time.deltaTime / 2);
        text.color = color;
        transform.position = Camera.main.WorldToScreenPoint(position);
        if (color.a <= 0) {
            Destroy(this.gameObject);
        }
    }
}
