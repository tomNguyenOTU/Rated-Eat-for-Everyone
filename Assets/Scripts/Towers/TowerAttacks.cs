using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttacks : MonoBehaviour
{
    // this class has been merged into Tower; any error lines in this script have been commented out so unity doesn't freak out


    public GameObject enemyToAttack;

    public bool canAttack;

    public int attackPower;

    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;

        attackPower = 33;
    }

    //Update is called once per frame
    void Update()
    {
        if (enemyToAttack != null && canAttack == true)
        {
            StartCoroutine(Attack());
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "enemy")
        {
            if (enemyToAttack == null)
            {
                enemyToAttack = col.gameObject;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (enemyToAttack == col.gameObject)
        {
            enemyToAttack = null;
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        // enemyToAttack.GetComponent<EnemyStats>().GetHitPoints() -= attackPower;
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
    }
}