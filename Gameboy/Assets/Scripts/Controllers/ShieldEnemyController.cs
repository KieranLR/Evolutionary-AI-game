using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemyController : MonoBehaviour, IEnemy
{
    public Animator anim;
    public float moveSpeed;
    public GameObject target;               // Follows this gameObject
    public float distanceThreshold;         // Distance to follow player
    public float attackThreshold;           // Distance to attack player
    public CharacterStats Stats { get; set; }
    public float CurrentHealth { get; set; }


    public GameObject damageIndicator;
    private bool moving;
    private Vector2 move;
    private Vector2 lastMove;
    private Vector2 distanceNormal;
    private Rigidbody2D myBody;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        Stats = new CharacterStats(3, 3, 3, 3, 3, 3, 100);
        CurrentHealth = Stats.GetStat(Stat.StatType.MaxHealth).GetFinalStatValue();
    }

    // Update is called once per frame
    void Update()
    {
        moving = false;
        move = new Vector2(0, 0);

        Vector2 distance = new Vector2(target.transform.position.x - this.transform.position.x, target.transform.position.y - this.transform.position.y);

        if (distance.magnitude < attackThreshold)
        {
            anim.Play("Attack");
            myBody.velocity = Vector2.zero;
        }
        else if (distance.magnitude < distanceThreshold)
        {
            distanceNormal = distance.normalized;
            myBody.velocity = new Vector2(distanceNormal.x * moveSpeed, distanceNormal.y * moveSpeed);
            moving = true;
            lastMove = new Vector2(distanceNormal.x, distanceNormal.y);
            move.x = distanceNormal.x;
            move.y = distanceNormal.y;
        }
        else
        {
            myBody.velocity = Vector2.zero;
        }

        anim.SetFloat("MoveX", move.x);
        anim.SetFloat("MoveY", move.y);
        anim.SetFloat("LastX", lastMove.x);
        anim.SetFloat("LastY", lastMove.y);
        anim.SetBool("Moving", moving);
    }

    public void Die() {
        Debug.Log("isded");
        Destroy(this.gameObject);
    }

    public void TakeDamage(float damage) {
        damageIndicator.GetComponent<DamageController>().SpawnText((int)damage, transform.position);
        CurrentHealth -= damage;
        if (CurrentHealth <= 0) {
            Die();
        }
    }


}
