using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Abilities/Attacks/MeleeAttack")]
public class MeleeAttack : Attack {

    public override IEnumerator UseAbility(PlayerController player)
    {
        // Halt the player
        player.busy = true;
        player.myBody.velocity = Vector2.zero;
        // Rotate attack towards mouse
        Vector3 mousePos = Input.mousePosition;
        mousePos.x -= Screen.width / 2;
        mousePos.y -= Screen.height / 2;
        float newAngle = 190 + Vector3.SignedAngle(new Vector3(1, 0, 0), mousePos, new Vector3(0, 0, -1));
        player.currentWeapon.GetComponent<IWeapon>().PerformAttack(20);
        player.hand.RotateAround(player.transform.position, new Vector3(0, 0, 1), player.currentHandAngle - newAngle);
        player.currentHandAngle = newAngle;

        damage = (player.currentWeapon.GetComponent<IWeapon>().BaseDamage + player.stats.GetStat(Stat.StatType.Strength).GetFinalStatValue()) * damageMultiplier;
        player.currentWeapon.GetComponent<IWeapon>().PerformAttack(damage);     // This will activate the hitbox and deal damage to whatever is hit.
        fps = 15;
        player.animationController.PlayAnimation(animation, fps);
        yield return new WaitForSeconds(5 * 1f/fps);
        player.busy = false;
    }
}

