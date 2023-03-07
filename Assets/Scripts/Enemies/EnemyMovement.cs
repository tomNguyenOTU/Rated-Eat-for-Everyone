using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject currentTargetObject; // the game object that the enemy is currently moving towards
    [SerializeField] private GameObject defaultTarget; // the default game object that the enemy moves towards if there is no current target
    [SerializeField] private string defaultTargetName; // the name of the default target game object
    [SerializeField] private float moveSpeed; // the speed at which the enemy moves

    private Rigidbody2D objrigidbody; // the Rigidbody2D component attached to the game object
    private bool moveObj; // a boolean that determines whether the enemy should move or not

    private void Start()
    {
        objrigidbody = GetComponent<Rigidbody2D>(); // get the Rigidbody2D component attached to the game object
        defaultTarget = GameObject.Find(defaultTargetName); // find the default target game object using its name
        moveObj = true; // set moveObj to true initially
    }

    private void Update()
    {
        // if the currentTargetObject is null or inactive, set it to the default target game object
        if (currentTargetObject == null || !currentTargetObject.activeInHierarchy)
        {
            currentTargetObject = defaultTarget;
        }
    }

    private void FixedUpdate()
    {
        // if moveObj is true, call the Movement() function with the currentTargetObject as an argument
        if (moveObj)
        {
            Movement(currentTargetObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if moveObj is true and the colliding game object is not tagged as "Enemy", set moveObj to false
        if (moveObj && !collision.gameObject.CompareTag("Enemy"))
        {
            moveObj = false;
        }

        // if the colliding game object is tagged as "Projectile", destroy both the projectile and the enemy game object
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        moveObj = true; // set moveObj to true when the game object stops colliding with another game object
    }

    private void Movement(GameObject target)
    {
        Vector2 lookDirection = target.transform.position - transform.position; // calculate the direction to look towards

        Quaternion rotation = Quaternion.LookRotation(lookDirection, -Vector3.forward); // calculate the rotation to face the target

        objrigidbody.MoveRotation(rotation.eulerAngles.z); // rotate the game object towards the target

        objrigidbody.MovePosition(objrigidbody.position + (lookDirection.normalized * moveSpeed * Time.fixedDeltaTime)); // move the game object towards the target
    }
}
