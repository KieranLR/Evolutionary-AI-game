using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUpMenu : MonoBehaviour {
    public static bool LevelingUp = false;
    public GameObject[] PerkCards;
    public GameObject LevelMenu;
    public GameObject PauseMenu;
    public GameObject Player;
    Perk[] currentPerks;
    // Use this for initialization


    public void Start() {
        DontDestroyOnLoad(this);
        currentPerks = new Perk[5];
        LevelMenu.SetActive(false);
    }

    public void LevelUp(Perk[] perks) {
        currentPerks = perks;
        PauseMenu.GetComponent<PauseMenu>().StopTime();
        LevelingUp = true;
        PerkCards[0].GetComponent<TextMeshProUGUI>().text = perks[0].perkName;
        PerkCards[1].GetComponent<TextMeshProUGUI>().text = perks[1].perkName;
        PerkCards[2].GetComponent<TextMeshProUGUI>().text = perks[2].perkName;
        PerkCards[3].GetComponent<TextMeshProUGUI>().text = perks[3].perkName;
        PerkCards[4].GetComponent<TextMeshProUGUI>().text = perks[4].perkName;
        LevelMenu.SetActive(true);
    }

    public void StopLevelUp(int perknum) {
        Debug.Log("pee");
        PauseMenu.GetComponent<PauseMenu>().ResumeTime();
        currentPerks[perknum].OnChoose();
        LevelingUp = false;
        LevelMenu.SetActive(false);
    }
}
