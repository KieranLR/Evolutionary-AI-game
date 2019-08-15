using System;

public interface IEnemy
{
    CharacterStats Stats { get; set; }
    float CurrentHealth { get; set; }

    void TakeDamage(float damage);
    void Die();
}
