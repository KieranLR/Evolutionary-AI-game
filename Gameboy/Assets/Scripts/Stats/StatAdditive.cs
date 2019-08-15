using System.Collections;
using System.Collections.Generic;

public class StatAdditive {
    public int Value;
    public Stat.StatType AffectedStat;
    public string Source;

    public StatAdditive(int value, Stat.StatType stat, string source) 
    {
        this.Value = value;
        this.AffectedStat = stat;
        this.Source = source;
    }
}
