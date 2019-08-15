using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IEnemy
{
    public GameObject target;               // Follows this gameObject
    public float distanceThreshold;         // Distance to follow player
    public float attackThreshold;           // Distance to attack player
    public CharacterStats Stats { get; set; }
    public float CurrentHealth { get; set; }


    public float attack;
    private float damage_dealt = 0;
    public float speed;
    public float defense;

    public float timeAlive;
    public float timeNearPlayer;
    public GameObject damageIndicator;
    private bool moving;
    private Vector2 move;
    private Vector2 lastMove;
    private Vector2 distanceNormal;
    private Rigidbody2D myBody;
    private AnimationController animationController;
    private int current_id;
    private float timeSincePlayer = 0.0f;
    // Use this for initialization
    void Start()
    {
        timeSincePlayer = Time.time;
        timeAlive = Time.time;
        timeNearPlayer = 0;
        current_id = 1;
        myBody = GetComponent<Rigidbody2D>();
        animationController = GetComponent<AnimationController>();
        Stats = new CharacterStats(3, 3, 3, 3, 3, 3, 100, 5);
        CurrentHealth = Stats.GetStat(Stat.StatType.MaxHealth).GetFinalStatValue();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist <= 12.0)
            timeNearPlayer += Time.deltaTime;
        moving = false;
        move = new Vector2(0, 0);
        if (dist < 20.0)
            timeSincePlayer = Time.time;

        if (Time.time - timeSincePlayer > 3) {
            Die();
        }

        Vector2 distance = new Vector2(target.transform.position.x - this.transform.position.x, target.transform.position.y - this.transform.position.y);

        if (distance.magnitude < attackThreshold)
        {
            myBody.velocity = Vector2.zero;
            if (current_id != 0) {
                current_id = 0;
                animationController.PlayAnimation(0, 15);
            }
            
            
        }
        else if (distance.magnitude < distanceThreshold)
        {
            distanceNormal = distance.normalized;
            myBody.velocity = new Vector2(distanceNormal.x * speed, distanceNormal.y * speed);
            moving = true;
            lastMove = new Vector2(distanceNormal.x, distanceNormal.y);
            move.x = distanceNormal.x;
            move.y = distanceNormal.y;
        }
        else
        {
            myBody.velocity = Vector2.zero;
        }

        
    }

    public void Die() {
        // This is slow if happens every frame. However, it is unlikely enemies will be dying every frame.
        //GameObject player = GameObject.Find("Player");
        GameObject ES = GameObject.Find("EnemyManager");
        //Probably shouldn't always give XP to the player if an enemy dies, as sometimes enemies will
        //Kill themselves, or get killed by other enemies
        //player.GetComponent<PlayerController>().getXP(Stats.GetStat(Stat.StatType.Experience).GetFinalStatValue());
        EnemySpawn spawner = ES.GetComponent<EnemySpawn>();
        spawner.Enemies.Remove(this.gameObject);
        if (timeNearPlayer > 0) {
            EnemySpawn.Child child = new EnemySpawn.Child();
            child.chromosome = new float[5];
            child.chromosome[0] = attack;
            child.chromosome[1] = defense;
            child.chromosome[2] = speed;
            child.chromosome[3] = attackThreshold;
            child.chromosome[4] = distanceThreshold;
            child.damageDealt = damage_dealt;
            child.percentInRange = timeNearPlayer / timeAlive;
            spawner.current_population.Add(child);
        }
        Destroy(this.gameObject);
    }

    public void TakeDamage(float damage) {
        damageIndicator.GetComponent<DamageController>().SpawnText((int)(damage / defense), transform.position);
        CurrentHealth -= damage / defense;
        if (CurrentHealth <= 0) {
            Die();
        }
    }

    public void DealDamage(float damage) {
        damage_dealt += attack;
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            DealDamage(attack);
        }
    }

}
