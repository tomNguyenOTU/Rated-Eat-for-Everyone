using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float hitPoints;
    public float atkDamage;
    public float atkDelay;
    public float moveSpeed;

    public float GetHitPoints()
    {
        return hitPoints;
    }

    public void SetHitPoints(float hitPoints)
    {
        this.hitPoints = hitPoints;
    }
}
