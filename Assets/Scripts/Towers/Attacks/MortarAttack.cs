using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarAttack : Attack
{
    public GameObject targetMarker;
    public GameObject attackProjectile;

    public float minimumRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IgnoreCloseEnemies();

        if (target != null)
        {
            UpdateTarget();
        }
    }

    public override IEnumerator AttackLoop()
    {
        while (target != null)
        {
            yield return new WaitForSeconds(_stats.atkDelay);
            Attack();

            if (target.GetComponent<EnemyStats>().hitPoints <= 0 || target == null)
            {
                RemoveResetTarget();
                yield break;
            }
        }
    }

    public void Attack()
    {
        GameObject mark = Instantiate(targetMarker, target.transform.position, Quaternion.identity, transform);
        GameObject attack = Instantiate(attackProjectile, transform.position, Quaternion.identity, transform);

        attack.Init(mark.transform.position, _stats.speed); // access projectile script
    }

    public void IgnoreCloseEnemies()
    {
        foreach (GameObject enemy in enemiesInRange)
        {
            if (_dist.CheckDistance(enemy.transform, minimumRange))
            {
                enemiesInRange.Remove(enemy);

                if (enemy == target)
                {
                    target = null;
                }
            }
        }
    }
}
