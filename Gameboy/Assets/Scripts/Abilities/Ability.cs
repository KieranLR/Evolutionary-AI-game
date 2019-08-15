using UnityEngine;
using System.Collections;

public abstract class Ability : ScriptableObject
{
    public string abilityName = "New ability";
    public string abilityDescription = "Undefined ability";
    public Sprite icon;
    public float baseCooldown = 1.0f;

    public abstract IEnumerator UseAbility(PlayerController player);   // Pass in player controller for stats to work
}
