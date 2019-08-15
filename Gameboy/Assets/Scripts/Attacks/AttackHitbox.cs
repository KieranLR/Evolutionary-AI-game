using UnityEngine;
using System.Collections;

public class AttackHitbox : MonoBehaviour
{
    public float CurrentDamage { get; set; }

    private void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("I just dealt: " + CurrentDamage + " Damage!");
            other.GetComponent<IEnemy>().TakeDamage(CurrentDamage);
        }
    }
}
