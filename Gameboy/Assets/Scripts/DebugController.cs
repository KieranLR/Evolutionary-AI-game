using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour {
    public GameObject attack;
    public GameObject defense;
    public GameObject speed;
    public GameObject attThresh;
    public GameObject disThresh;
    public GameObject GenText;
    public GameObject PopText;
	// Use this for initialization
	void Start () {
        GameObject player = GameObject.Find("Player");
        transform.parent = player.transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
