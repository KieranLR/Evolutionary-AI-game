using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Abilities/Dash")]
public class Dash : Ability
{
    public float dashTime = 0.8f;
    public float dashSpeed = 30;

    public override IEnumerator UseAbility(PlayerController player) {
        // Dash towards the mouse position
        player.busy = true;
        Vector3 dashDirection = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0)).normalized;
        player.GetComponent<Rigidbody2D>().velocity = dashDirection * dashSpeed;
        float dashTimeRemaining = dashTime;
        yield return null;
        while (dashTimeRemaining > 0) {
            dashTimeRemaining -= Time.deltaTime;
            yield return null;
        }
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.busy = false;
    }
}
