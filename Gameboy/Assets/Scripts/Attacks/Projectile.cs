using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float baseDamage;
    public float speed;
    public Vector2 direction;
    public float duration;
    public int pierce;

    [HideInInspector] public float damage;
    private Rigidbody2D myBody;
    private float timeAlive;
    private int enemiesPierced;

    // Use this for initialization
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myBody.velocity = direction.normalized * speed;
        timeAlive = 0;
        enemiesPierced = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > duration) 
        {
            Die();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<IEnemy>().TakeDamage(damage);
            if (++enemiesPierced > pierce)
            {
                Die();
            }
        }
    }

    public void Die() 
    {
        Destroy(this.gameObject);
    }
}
