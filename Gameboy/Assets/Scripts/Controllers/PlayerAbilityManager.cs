using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAbilityManager : MonoBehaviour
{
    public Ability initAbility1;
    public Ability initAbility2;
    public List<KeyCode> abilityKeys;   // Store hotkeys like this so that controls can be configurable via options

    private Dictionary<KeyCode, Ability> abilities;     // Abilities bound to hotkeys
    private List<Ability> extraAbilities;   // Abilities not bound to hotkeys

    // Use this for initialization
    void Start()
    {
        // For now, set abilityKeys manually
        abilityKeys.Add(KeyCode.Mouse0);
        abilityKeys.Add(KeyCode.Mouse1);
        abilityKeys.Add(KeyCode.Space);
        abilityKeys.Add(KeyCode.Q);
        abilities = new Dictionary<KeyCode, Ability>
        {
            { abilityKeys[0], initAbility1 },
            { abilityKeys[1], initAbility2 }
        };
        for (int i = 2; i < abilityKeys.Count; i++) {
            abilities[abilityKeys[i]] = null;
        }
    }

    public Ability GetAbility(KeyCode keyPressed) 
    {
        return abilities[keyPressed];
    }

    public void AddNewAbility(Ability ability) 
    {
        // Use this method to add an ability when chosen by a perk
        foreach (KeyCode key in abilityKeys)
        {
            if (!abilities[key])
            {
                abilities[key] = ability;
                return;
            }
        }
        // If here, all hotkeys full, so add to extraAbilities
        extraAbilities.Add(ability);
    }

}
