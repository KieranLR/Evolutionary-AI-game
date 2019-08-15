using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class EnemySpawn : MonoBehaviour {
    string path;
    string enemydatabase;
    public GameObject enemy;
    public GameObject player;
    public GameObject indicator;
    public GameObject Graph;
    public int generation = 0;
    public List<GameObject> Enemies;
    public List<Child> current_population;
    public List<Child> new_population;
    private float nextSpawnTime = 5.0f;
    public float period = 10.0f;
    private float nextPrintDebugTime = 0.0f;
    // Use this for initialization
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        if ((int)Time.time > nextPrintDebugTime) {
            Debug.Log("Number of Enemies: " + Enemies.Count);
            Debug.Log("Total Populatoin: " + current_population.Count);
            nextPrintDebugTime += 3.0f;
        }
        
        if ((int)Time.time > nextSpawnTime) {
            generation++;
            nextSpawnTime += period;
            DebugController DG = Graph.GetComponent<DebugController>();
            float att = 0;
            float def = 0;
            float spd = 0;
            float atThr = 0;
            float dtThr = 0;
            int size = 1;
            float[] probabilities = new float[current_population.Count];
            float totalDamage = 0;
            foreach (Child c in current_population) {
                att += c.chromosome[0];
                def += c.chromosome[1];
                spd += c.chromosome[2];
                atThr += c.chromosome[3];
                dtThr += c.chromosome[4];
                totalDamage += c.damageDealt;
            }

            for (int i = 0; i < current_population.Count; i++) {
                probabilities[i] = current_population[i].damageDealt / totalDamage;
            }


            if ((size = current_population.Count) > 0) {
                att = att / size;
                def = def / size;
                spd = spd / size;
                atThr = atThr / size;
                dtThr = dtThr / size;
                float avatt = Mathf.Lerp(-220, -170, att / 6);
                DG.attack.GetComponent<RectTransform>().offsetMax = new Vector2(DG.attack.GetComponent<RectTransform>().offsetMax.x, avatt);
                float avdef = Mathf.Lerp(-220, -170, def / .8f);
                DG.defense.GetComponent<RectTransform>().offsetMax = new Vector2(DG.defense.GetComponent<RectTransform>().offsetMax.x, avdef);
                float avspd = Mathf.Lerp(-220, -170, spd / 12);
                DG.speed.GetComponent<RectTransform>().offsetMax = new Vector2(DG.speed.GetComponent<RectTransform>().offsetMax.x, avspd);
                float avatThr = Mathf.Lerp(-220, -170, atThr / 3);
                DG.attThresh.GetComponent<RectTransform>().offsetMax = new Vector2(DG.attThresh.GetComponent<RectTransform>().offsetMax.x, avatThr);
                float avdtThr = Mathf.Lerp(-220, -170, dtThr / 17);
                DG.disThresh.GetComponent<RectTransform>().offsetMax = new Vector2(DG.disThresh.GetComponent<RectTransform>().offsetMax.x, avdtThr);
            }

            DG.GenText.GetComponent<TextMeshProUGUI>().SetText(generation.ToString());
            DG.PopText.GetComponent<TextMeshProUGUI>().SetText(size.ToString());
            GameObject E1;
            for (int i = -75; i < 75; i+= 15) {
                for (int j = -75; j < 75; j+= 15) {
                    EnemyController ec;
                    for (int k = 0; k < 4; k++) {
                        float l = Random.value;
                        float inc = 0;
                        int count = 0;
                        while (inc <= l && count < size - 1) {
                            inc += probabilities[count];
                            count++;
                        }
                        if (size != 0) {
                            E1 = Instantiate(enemy, new Vector3(i + Random.value * 5 - 2.5f, j + Random.value * 5 - 2.5f, 0), Quaternion.identity);
                            ec = E1.GetComponent<EnemyController>();
                            ec.target = player;
                            ec.attack = current_population[count].chromosome[0] + (Random.value * 1f - .5f) * (current_population[count].chromosome[0] - att) + Random.value * 2 - 1;
                            ec.defense = current_population[count].chromosome[1] + (Random.value * 1f - .5f) * (current_population[count].chromosome[1] - def) + Random.value * .5f - .25f;
                            ec.speed = current_population[count].chromosome[2] + (Random.value * 1f - .5f) * (current_population[count].chromosome[2] - spd) + Random.value * 3 - 1;
                            ec.attackThreshold = current_population[count].chromosome[3] + (Random.value * 1f - .5f) * (current_population[count].chromosome[3] - atThr) + Random.value * 1 - .5f;
                            ec.distanceThreshold = current_population[count].chromosome[4] + (Random.value * 1f - .5f) * (current_population[count].chromosome[4] - dtThr) + Random.value * 4 - 2;
                            if (ec.attack < 0)
                                ec.attack = 0;
                            if (ec.attack > 6)
                                ec.attack = 6;
                            if (ec.defense < .1f)
                                ec.defense = .1f;
                            if (ec.defense > 1)
                                ec.defense = 1;
                            if (ec.speed < 0)
                                ec.speed = 0;
                            if (ec.speed > 12)
                                ec.speed = 12;
                            if (ec.attackThreshold < 0)
                                ec.attackThreshold = 0;
                            if (ec.attackThreshold > 3)
                                ec.attackThreshold = 3;
                            if (ec.distanceThreshold < 0)
                                ec.distanceThreshold = 0;
                            if (ec.distanceThreshold > 18)
                                ec.distanceThreshold = 18;
                            if (ec.distanceThreshold < ec.attackThreshold)
                                ec.distanceThreshold = ec.attackThreshold;
                            ec.damageIndicator = indicator;
                            Enemies.Add(E1);
                        }
                    }
                }
            }
            current_population.Clear();
        }
    }

    void Start () {
        current_population = new List<Child>();
        player = GameObject.Find("Player");
        indicator = GameObject.Find("Damage Indicator");
        path = Application.streamingAssetsPath + "/Enemies.json";
        enemydatabase = File.ReadAllText(path);
        Enemy en = JsonUtility.FromJson<Enemy>(enemydatabase);
        Sprite[] sprites = Resources.LoadAll<Sprite>("Art/CharacterSpriteSheet");
        AnimationController eac = enemy.GetComponent<AnimationController>();
        eac.SecondsPerFrame = 0.25f;
        eac.spr = enemy.GetComponent<SpriteRenderer>();
        eac.Animations = new AnimationController.SpriteAnimation[2];
        eac.Animations[0] = new AnimationController.SpriteAnimation();
        eac.Animations[0].name = "Idle";
        eac.Animations[0].sprites = new Sprite[4];
        eac.Animations[0].sprites[0] = sprites[45];
        eac.Animations[0].sprites[1] = sprites[46];
        eac.Animations[0].sprites[2] = sprites[47];
        eac.Animations[0].sprites[3] = sprites[48];

        eac.Animations[1] = new AnimationController.SpriteAnimation();
        eac.Animations[1].name = "Attack";
        eac.Animations[1].sprites = new Sprite[5];
        eac.Animations[1].sprites[0] = sprites[24];
        eac.Animations[1].sprites[1] = sprites[25];
        eac.Animations[1].sprites[2] = sprites[26];
        eac.Animations[1].sprites[3] = sprites[27];
        eac.Animations[1].sprites[4] = sprites[28];
        EnemyController ec;
        GameObject E1;
        for (int i = -75; i < 75; i += 35) {
            for (int j = -75; j < 75; j += 35) {
                for (int k = 0; k < 4; k++) {
                    E1 = Instantiate(enemy, new Vector3(i + Random.value * 5 - 2.5f, j + Random.value * 5 - 2.5f, 0), Quaternion.identity);
                    ec = E1.GetComponent<EnemyController>();
                    ec.target = player;
                    ec.distanceThreshold = Random.value * 15 + 2;
                    ec.attackThreshold = Random.value * 3 + 0;
                    ec.damageIndicator = indicator;
                    ec.attack = Random.value * 5 + 1;
                    ec.speed = Random.value * 10 + 2;
                    ec.defense = Random.value * .7f + .1f;
                    Enemies.Add(E1);
                }
            }
        }
    }


    [System.Serializable]
    public class Enemy {
        public int id;
        string name;
        public float moveSpeed;
        public float distanceThreshold;
        public float attackThreshold;
        public int[] Stats;
    }

    public struct Child {
        public float[] chromosome;
        public float damageDealt;
        public float percentInRange;
    }


    public float fitness(float DamageDealt, float PercentInRange) {
        return (DamageDealt - (.5f - PercentInRange) * DamageDealt);
    }
}
