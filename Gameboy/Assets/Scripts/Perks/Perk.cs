using UnityEngine;
using System.Collections;

public abstract class Perk : ScriptableObject
{
    public string perkName = "New perk";
    public string perkDescription = "An unimplemented perk";

    public abstract void OnChoose();
}
