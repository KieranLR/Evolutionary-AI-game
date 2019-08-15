using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats {
    
    public List<Stat> stats;

    public CharacterStats(int strength, int toughness, int speed, int agility, int luck, int arcana, int maxhealth, int experience = 0) {
        stats = new List<Stat>() {
            new Stat(strength, Stat.StatType.Strength, "Damage dealt by attacks."),
            new Stat(toughness, Stat.StatType.Toughness, "Resistance to damage."),
            new Stat(speed, Stat.StatType.Speed, "Movement speed."),
            new Stat(agility, Stat.StatType.Agility, "Attack speed."),
            new Stat(luck, Stat.StatType.Luck, "Effect chance and crit rate."),
            new Stat(arcana, Stat.StatType.Arcana, "Spellcasting ability."),
            new Stat(maxhealth, Stat.StatType.MaxHealth, "Maximum health."),
            new Stat(experience, Stat.StatType.Experience, "Experience owned")
        };
    }

    public Stat GetStat(Stat.StatType stat) {
        return this.stats.Find(x => x.Type == stat);
    }

    public void AddStatAdditive(StatAdditive statAdditive) {
        GetStat(statAdditive.AffectedStat).AddStatAdditive(statAdditive);
    }

    public void AddStatMultiplier(StatMultiplier statMultiplier)
    {
        GetStat(statMultiplier.AffectedStat).AddStatMultiplier(statMultiplier);
    }

    public void RemoveStatAdditive(StatAdditive statAdditive)
    {
        GetStat(statAdditive.AffectedStat).RemoveStatAdditive(statAdditive);
    }

    public void RemoveStatMultiplier(StatMultiplier statMultiplier)
    {
        GetStat(statMultiplier.AffectedStat).RemoveStatMultplier(statMultiplier);
    }
}
