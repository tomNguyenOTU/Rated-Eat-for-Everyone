using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // tower stats
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private float cooldown;
    [SerializeField] private float range;

    [SerializeField] private int cost;
    [SerializeField] private float[] buildTimer;

    [SerializeField] private int width;

    // used as reference
    private float maxHealth;
    private float normalCooldown;
    private float currentBuildTimer;

    // components of tower
    private Transform _trans;

    // other
    // private float lastAttackTime = Time.realtimeSinceStartup;
    private int upgradeTier = 0;

    void Start()
    {
        _trans = GetComponent<Transform>();
    }

    void Update()
    {
        
    }

    // pseudocode for now, uncomment when testing this out or something
    //public List<GameObject> findEnemiesInRange(bool canHitFlying)
    //{
    //    List<GameObject> enemies = new List<GameObject>();

    //    //selfnote for later: consider this solution for tags: https://answers.unity.com/questions/1470694/multiple-tags-for-one-gameobject.html

    //    enemies.Add(GameObject.FindGameObjectsWithTag("Enemy"));
    //    if (canHitFlying)
    //    {
    //        enemies.Add(GameObject.FindGameObjectsWithTag("Flying"));
    //    }

    //    for (int i = enemies.Count; i > 0; i--)
    //    {
    //        Transform enemyTransform = GetComponent<Transform>(enemies[i]);

    //        if (Vector3.Distance(_trans.position, enemyTransform.position) > range)
    //        {
    //            enemies.RemoveAt(i);
    //        }
    //    }

    //    return enemies;

    //    // some of this might just be easier if i have a universal enemy class tbh
    //}

    //whoops
    //checks if the tower can attack:
    // if the difference between realtime and last attack time is greater than cooldown, then the tower can attack
    //public bool CanAttack()
    //{
    //    if (Time.realtimeSinceStartup - lastAttackTime > cooldown)
    //    {
    //        return true;
    //    }

    //    return false;
    //}

    // helper functions go here
    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(float health)
    {
        this.health = health;
    }

    // use these cooldown functions for any relative changes (e.g. attack speed debuffs)
    public float GetCooldown()
    {
        return cooldown;
    }    

    public void SetCooldown(float cooldown) 
    {
        this.cooldown = cooldown;
    }
    
    // use these cooldown functions for permanent changes (e.g. upgrades)
    public float GetNormalCooldown()
    {
        return normalCooldown;
    }

    public void SetNormalCooldown(float normalCooldown)
    {
        this.normalCooldown = normalCooldown;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public float GetRange()
    {
        return range;
    }

    public void SetRange(float range)
    {
        this.range = range;
    }

    public void SetLastAttack(float time)
    {
        // lastAttackTime = time;
    }

    public void UpgradeTower()
    {
        if (upgradeTier != 4)
        {
            upgradeTier++;
        }
    }

    public int GetWidth()
    {
        return width;
    }
}
