using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearController : MonoBehaviour {

	public int thrustLength;
	public int thrustSpeed;

	private int counter;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (counter < thrustLength) {
			transform.Translate(transform.forward * thrustSpeed);
		}
		if (counter < thrustLength / 2) {
			transform.Translate(transform.forward * thrustSpeed * -1);
		} else {
			Destroy(this);
		}
		counter = counter + 1;
	}
}
