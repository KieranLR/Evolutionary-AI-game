using UnityEngine;
using System.Collections;

public abstract class Attack : Ability
{
    public float damageMultiplier;
    public AttackHitbox attackHitbox;
    public int animation;   // index of animation

    protected float damage;
    protected int fps;        // animation fps

}
