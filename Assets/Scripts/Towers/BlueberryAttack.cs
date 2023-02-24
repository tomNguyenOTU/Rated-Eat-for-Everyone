using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlueberryAttack : MonoBehaviour
{
    public GameObject target;
    public GameObject projPrefab;

    Rigidbody2D _rb;

    private bool canAttack = true;
    private bool? attacking = null;
    Tower _tower;
    TowerStats _stats;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _stats = GetComponent<TowerStats>();
        _tower = GetComponent<Tower>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TrackTarget(target);

        if (target == null)
        {
            _tower.UpdateTowerTarget();
            attacking = false;
        }
        else
        {
            TrackTarget(target);
        }

        if (attacking == true)
        {
            StartCoroutine(Attack());
            attacking = null;
        }
        else if (attacking == false) 
        {
            StopCoroutine(Attack());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            _tower._enemiesInRange.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            _tower._enemiesInRange.Remove(collision.gameObject);
        }
    }
    public IEnumerator Attack()
    {
        canAttack = false;
        yield return new WaitForSeconds(_stats.atkDelay);

        Instantiate(projPrefab, transform.position, transform.rotation, transform);

        canAttack = true;
    }

    void TrackTarget(GameObject target)
    {
        Vector2 lookDirection = target.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.forward);

        _rb.SetRotation(rotation);
    }

    public void Spawn(GameObject template, Vector3 spawnPosition)
    {
        Instantiate(template, spawnPosition, transform.rotation, GetComponent<Transform>());
    }
}
