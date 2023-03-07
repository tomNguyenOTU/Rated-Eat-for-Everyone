using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueberryAttack : Attack
{
    public GameObject projPrefab;
    public GameObject projSpawnLocation;

    void Update()
    {
        if (target == null)
        {
            UpdateTarget();
        }
    }

    public override IEnumerator AttackLoop()
    {
        while (target != null)
        {
            Spawn();

            yield return new WaitForSeconds(_stats.atkDelay);

            if (target.GetComponent<EnemyStats>().hitPoints <= 0 || target == null)
            {
                RemoveResetTarget();
                yield break;
            }
        }
    }

    public void Spawn()
    {
        TrackTarget(target);

        Instantiate(projPrefab, projSpawnLocation.transform.position, projPrefab.transform.rotation);
    }
}