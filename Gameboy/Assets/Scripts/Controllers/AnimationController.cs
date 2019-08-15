using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour {
    public float SecondsPerFrame;
    public SpriteRenderer spr;
    public SpriteAnimation[] Animations; // Array of all animations for the character
    private int Current_AnimFrame;
    private int current_id;

    // Awake is called before Start
    void Awake() {
        current_id = 0;
        Current_AnimFrame = 0;
        spr = this.gameObject.GetComponent<SpriteRenderer>();
        //PlayAnimation(0, 0.25f);
        SecondsPerFrame = 0.25f;
        StartCoroutine("Animate", 0);
    }

    // Plays the animation in AnimationSets of the given ID
    public void PlayAnimation(int ID, int fps) {
        SecondsPerFrame = 1f/fps;

        // A switch statement is used in case certain Animations should
        // Behave differently, for example, taking damage should always finish,
        // while a casting animation may be able to be cancelled. 
        switch (ID) {
            default:
                Current_AnimFrame = 0;
                current_id = ID;
                //StopCoroutine("Animate");
                //StartCoroutine("Animate", ID);
                break;
        }
    }

    IEnumerator Animate (int ID) {
        switch (ID) {
            default:

                while (true) {
                    yield return new WaitForSeconds(SecondsPerFrame);
                    ID = current_id;
                    if (Current_AnimFrame >= Animations[ID].sprites.Length) {
                        Current_AnimFrame = 0;
                    }
                    spr.sprite = Animations[ID].sprites[Current_AnimFrame];
                    Current_AnimFrame+= 1;
                    // Loop the Animation when finished
                    if (Current_AnimFrame >= Animations[ID].sprites.Length) {
                        Current_AnimFrame = 0;
                    }
                }




        }
    }

    // Class for animations
    [Serializable]
    public class SpriteAnimation {
        public string name;
        public Sprite[] sprites;
    }
}
