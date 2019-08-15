using System.Collections;
using System.Collections.Generic;

public class StatMultiplier {
    public int Value;
    public Stat.StatType AffectedStat;
    public string Source;

    public StatMultiplier(int value, Stat.StatType stat, string source)
    {
        this.Value = value;
        this.AffectedStat = stat;
        this.Source = source;
    }
}
