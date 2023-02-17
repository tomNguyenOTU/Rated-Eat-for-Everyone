using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    public GameObject target;
    public Collider2D trig;
    public GameObject projPrefab;
    public GameObject projLoc;
    public bool inRange = false;
    float atkTime = 0f;
    float atkDamage = 0f;
    float atkDelay = 0f;
    Rigidbody2D objrigidbody;
    GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        objrigidbody = parent.GetComponent<Rigidbody2D>();
        atkDamage = parent.GetComponent<TowerStats>().atkDamage;
        atkDelay = parent.GetComponent<TowerStats>().atkDelay;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TrackTarget(target);
        if(inRange)
        {
            Attack(target, atkDamage, atkDelay);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && (target == null))
        {
            target = collision.gameObject;
            inRange = true;
            atkTime = 0f;
        }
    }

    void TrackTarget(GameObject target)
    {
        Vector2 lookDirection = target.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.forward);

        objrigidbody.SetRotation(rotation);
    }

    public void Spawn(GameObject template, Vector3 spawnPosition)
    {
        Instantiate(template, spawnPosition, transform.rotation);
    }

    void Attack(GameObject @object, float damage, float cooldown)
    {
        EnemyStats enemy = @object.GetComponent<EnemyStats>();

        if (inRange && (Time.time > atkTime))
        {
            atkTime = Time.time + cooldown;
            Spawn(projPrefab, projLoc.transform.position);
        }
    }
}
