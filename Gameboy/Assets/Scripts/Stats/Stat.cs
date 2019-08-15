using System.Collections;
using System.Collections.Generic;

public class Stat {
    public enum StatType { Strength, Toughness, Speed, Agility, Luck, Arcana, MaxHealth, Experience }

    public int BaseValue { get; set; }
    public object StatDescription { get; set; }
    public StatType Type { get; set; }

    private int FinalValue { get; set; }
    private List<StatAdditive> StatAdditives;
    private List<StatMultiplier> StatMultipliers;

    public Stat(int baseValue, StatType t, string description)
    {
        this.BaseValue = baseValue;
        this.Type = t;
        this.StatDescription = description;
        this.StatAdditives = new List<StatAdditive>();
        this.StatMultipliers = new List<StatMultiplier>();
    }

    public void AddStatAdditive(StatAdditive additive) 
    {
        this.StatAdditives.Add(additive);
    }

    public void RemoveStatAdditive(StatAdditive additive) 
    {
        this.StatAdditives.Remove(additive);
    }

    public void AddStatMultiplier(StatMultiplier multiplier)
    {
        this.StatMultipliers.Add(multiplier);
    }

    public void RemoveStatMultplier(StatMultiplier multiplier)
    {
        this.StatMultipliers.Remove(multiplier);
    }

    public int GetFinalStatValue()
    {
        FinalValue = BaseValue;
        this.StatAdditives.ForEach(x => FinalValue += x.Value);
        this.StatMultipliers.ForEach(x => FinalValue *= x.Value);
        return FinalValue;
    }

}

