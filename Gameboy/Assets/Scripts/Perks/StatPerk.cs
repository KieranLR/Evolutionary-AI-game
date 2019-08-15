using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Perks/StatPerk")]
public class StatPerk : Perk
{
    public Stat.StatType affectedStat;
    public int amount;

    public override void OnChoose()
    {
        GameObject player = GameObject.Find("Player");
        StatAdditive perkAdditive = new StatAdditive(amount, affectedStat, this.perkName);
        player.GetComponent<PlayerController>().stats.AddStatAdditive(perkAdditive);
    }
}
