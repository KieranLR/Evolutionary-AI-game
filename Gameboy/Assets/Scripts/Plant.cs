using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {
    public Animator anim;
	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
	}

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log("Boom boom");
        //anim.SetTrigger("move");
    }

}

