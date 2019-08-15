using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Perks/AbilityPerk")]
public class AbilityPerk : Perk
{
    public Ability ability;

    public override void OnChoose()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerAbilityManager>().AddNewAbility(ability);
    }
}
