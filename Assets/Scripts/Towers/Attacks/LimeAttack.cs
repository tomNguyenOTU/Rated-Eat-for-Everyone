using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimeAttack : Attack
{
    bool exploding;

    // Update is called once per frame
    void Update()
    {
        if (target != null && !exploding)
        {
            exploding = true;

            StartCoroutine(AttackLoop());
        }
    }

    public override IEnumerator AttackLoop()
    {
        yield return new WaitForSeconds(_stats.atkDelay);

        Explode();
    }

    public void Explode()
    {
        foreach(GameObject enemy in enemiesInRange)
        {
            EnemyStats temp = enemy.GetComponent<EnemyStats>();

            temp.hitPoints -= _stats.atkDamage;
        }

        Destroy(gameObject);
    }
}
