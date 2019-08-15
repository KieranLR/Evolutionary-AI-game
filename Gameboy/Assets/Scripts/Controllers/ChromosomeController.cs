using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChromosomeController : MonoBehaviour {
    public GameObject Attack;
    public GameObject Speed;
    public GameObject Defense;
    public GameObject AThresh;
    public GameObject DThresh;
    public GameObject Enemy;
    private EnemyController ec;

    // Use this for initialization
    void Start () {
        EnemyController ec = Enemy.GetComponent<EnemyController>();
    }
	
	// Update is called once per frame
	void Update () {
        EnemyController ec = Enemy.GetComponent<EnemyController>();
        Attack.transform.position = Camera.main.WorldToScreenPoint(Enemy.transform.position + new Vector3(1, 0, 0));
        Speed.transform.position = Camera.main.WorldToScreenPoint(Enemy.transform.position + new Vector3(2, 0, 0));
        Defense.transform.position = Camera.main.WorldToScreenPoint(Enemy.transform.position + new Vector3(3, 0, 0));
        AThresh.transform.position = Camera.main.WorldToScreenPoint(Enemy.transform.position + new Vector3(4, 0, 0));
        DThresh.transform.position = Camera.main.WorldToScreenPoint(Enemy.transform.position + new Vector3(5, 0, 0));
        Attack.GetComponent<TextMeshProUGUI>().SetText(((int)ec.attack).ToString());
        Speed.GetComponent<TextMeshProUGUI>().SetText(((int)ec.speed).ToString());
        Defense.GetComponent<TextMeshProUGUI>().SetText(((int)ec.defense).ToString());
        AThresh.GetComponent<TextMeshProUGUI>().SetText(((int)ec.attackThreshold).ToString());
        DThresh.GetComponent<TextMeshProUGUI>().SetText(((int)ec.distanceThreshold).ToString());
    }
}
