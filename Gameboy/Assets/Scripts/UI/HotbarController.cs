using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarController : MonoBehaviour {

    private SpriteRenderer ability1;
    private SpriteRenderer ability2;
    private SpriteRenderer ability3;
    private SpriteRenderer ability4;

	// Use this for initialization
	void Start () {
        ability1 = transform.GetChild(0).GetComponent<SpriteRenderer>();
        ability2 = transform.GetChild(1).GetComponent<SpriteRenderer>();
        ability3 = transform.GetChild(2).GetComponent<SpriteRenderer>();
        ability4 = transform.GetChild(3).GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetAbility(Ability ability, KeyCode key)
    {
        switch (key)
        {
            case KeyCode.Mouse0:        // Eventually, don't do this manually
                ability1.sprite = ability.icon;
                break;
            case KeyCode.Mouse1:
                ability2.sprite = ability.icon;
                break;
            case KeyCode.Space:
                ability3.sprite = ability.icon;
                break;
            case KeyCode.Q:
                ability4.sprite = ability.icon;
                break;
            default:
                break;
        }
    }
}
