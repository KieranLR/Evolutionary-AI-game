using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon {
    public Animator anim;
    public ParticleSystem[] p1;
    public int BaseDamage { get; set; }
    public int BaseAttackSpeed { get; set; }


    public void Start() {
        anim = this.GetComponent<Animator>();
    }

    public void PerformAttack(float damage) {
        transform.GetComponentInChildren<AttackHitbox>().CurrentDamage = damage;
        if (p1[0].particleCount == 0) {
            p1[0].Emit(1);
        }
        anim.Play("WeaponSwing");
    }
}
