using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Abilities/Attacks/ProjectileAttack")]
public class ProjectileAttack : Attack
{
    private Transform projectileSpawn;
    public Projectile projectile;

    public override IEnumerator UseAbility(PlayerController player)
    {
        projectileSpawn = player.transform;
        projectile.damage = (projectile.baseDamage + player.stats.GetStat(Stat.StatType.Arcana).GetFinalStatValue()) * damageMultiplier;
        projectile.direction = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0)).normalized;
        Instantiate(projectile, projectileSpawn.position, Quaternion.identity);
        yield return null;
    }
}
