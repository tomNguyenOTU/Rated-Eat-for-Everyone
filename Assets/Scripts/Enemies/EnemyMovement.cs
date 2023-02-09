using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject currentTargetObject;
    public GameObject defaultTarget;
    public string defaultTargetName;
    public float moveSpeed = 1f;
    public bool moveObj = false;
    Rigidbody2D objrigidbody;

    // Start is called before the first frame update
    void Start()
    {
        objrigidbody = GetComponent<Rigidbody2D>();
        defaultTarget = GameObject.Find(defaultTargetName);
    }

    private void Update()
    {
        if (currentTargetObject == null || !currentTargetObject.activeInHierarchy)
        {
            currentTargetObject = defaultTarget;
        }
    }

    void FixedUpdate()
    {
        if (moveObj)
        {
            Movement(currentTargetObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (moveObj && collision.gameObject.tag != "Enemy")
        {
            moveObj= false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        moveObj = true;
    }

    void Movement(GameObject target)
    {
        Vector2 lookDirection = target.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(lookDirection, -Vector3.forward);

        objrigidbody.SetRotation(rotation);

        objrigidbody.MovePosition(objrigidbody.position + ((lookDirection.normalized) * moveSpeed * Time.fixedDeltaTime));
    }
}
