using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool inRange = false;
    float atkTime = 0f;
    float atkDamage = 0f;
    float atkDelay = 0f;

    private void Start()
    {
        atkDamage = GetComponent<EnemyStats>().atkDamage;
        atkDelay = GetComponent<EnemyStats>().atkDelay;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tower")
        {
            Attack(collision, atkDamage, atkDelay);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tower")
        {
            inRange = false;
            atkTime = 0f;
        }
    }

    void Attack(Collision2D collision, float damage, float cooldown)
    {
        TowerStats tower = collision.gameObject.GetComponent<TowerStats>();

        if (inRange && (Time.time > atkTime))
        {
            atkTime = Time.time + cooldown;
            tower.hitPoints -= damage;
            if (tower.hitPoints <= 0)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
