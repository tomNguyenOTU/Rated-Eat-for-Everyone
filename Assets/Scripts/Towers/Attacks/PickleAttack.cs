using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PickleAttack : Attack
{
    public GameObject picklePrefab;
    public int maxPickles;

    private int activePickles;
    private bool loop;

    // Update is called once per frame
    void Update()
    {
        activePickles = transform.childCount;

        if (activePickles < maxPickles && !loop)
        {
            loop = true;

            StartCoroutine(AttackLoop());
        }
    }

    public void Spawn()
    {
        Instantiate(picklePrefab, transform.position, Quaternion.identity, transform);
    }

    public override IEnumerator AttackLoop()
    {
        while (activePickles < maxPickles)
        {
            Spawn();

            yield return new WaitForSeconds(_stats.atkDelay);
        }

        loop = false;
    }
}
