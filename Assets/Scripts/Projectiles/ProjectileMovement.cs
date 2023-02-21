using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileMovement : MonoBehaviour
{
    public GameObject parentTower;
    public Camera cam;
    public float moveSpeed = 1.0f;
    float damage;

    void Start()
    {
        damage = parentTower.GetComponent<TowerStats>().atkDamage;
    }

    void Update()
    {
        // Starts object movement
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        // Find current object position, converted to screen space coordinates
        Vector3 projectilePosition = cam.WorldToViewportPoint(transform.position);

        {
            // Deletes an object if it crosses outside of camera bounds
            if (projectilePosition.x < 0 || projectilePosition.x > 1)
            {
                Destroy(gameObject);
            }
            else if (projectilePosition.y < 0 || projectilePosition.y > 1)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            GameObject enemy = collision.gameObject;
            float enemyHP = enemy.GetComponent<EnemyStats>().hitPoints;
            enemyHP -= damage;

            Destroy(gameObject);
        }
    }
}
