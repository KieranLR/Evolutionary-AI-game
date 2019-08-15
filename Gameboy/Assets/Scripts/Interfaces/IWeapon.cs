using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon {
    int BaseDamage { get; set; }
    int BaseAttackSpeed { get; set; }

    void PerformAttack(float damage);
}
