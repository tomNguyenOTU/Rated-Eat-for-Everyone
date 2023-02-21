using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerTags
{
    Blueberry,
    Watermelon
}

public class TowerStats : MonoBehaviour
{
    public float hitPoints;
    public float atkDamage;
    public float atkDelay;
    public float range;
    public int cost;
    public float[] buildTimer;
    public int width;
    public List<TowerTags> tags;
}
