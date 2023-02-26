using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlueberryAttack : Attack
{
    // blueberry projectile
    public GameObject projPrefab;
    public GameObject projSpawnLocation;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            UpdateTarget();
        }

        if (target.GetComponent<EnemyStats>().GetHitPoints() <= 0)
        {
            target = null;
        }
    }

    public override IEnumerator AttackLoop() 
    {
        Shoot();

        yield return new WaitForSeconds(_stats.atkDelay);

        if (target == null)
        {
            yield break;
        }
    }

    public void Shoot()
    {
        Instantiate(projPrefab, projSpawnLocation.transform.position, transform.rotation, GetComponent<Transform>());
    }
}
