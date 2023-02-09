using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTarget : MonoBehaviour
{
    public GameObject target;
    GameObject parent;
    EnemyMovement movement;

    private void Start()
    {
        parent = transform.parent.gameObject;
        movement = parent.GetComponent<EnemyMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tower")
        {
            movement.currentTargetObject = collision.gameObject;
        }
    }
}
