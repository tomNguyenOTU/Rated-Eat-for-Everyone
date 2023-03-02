using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // used as reference
    private float health;
    private float cooldown;
    private float damage;
    private float range;

    // components of tower
    private Transform _trans;
    private UtilityDistComparison _dist;
    private CircleCollider2D _rangeTrigger;
    private TowerStats _stats;

    // private float lastAttackTime = Time.realtimeSinceStartup;
    private int upgradeTier = 0;

    void Start()
    {
        _trans = GetComponent<Transform>();
        _dist = GetComponent<UtilityDistComparison>();
        _stats = GetComponent<TowerStats>();

        health = _stats.hitPoints;
        damage = _stats.atkDamage;
        cooldown = _stats.atkDelay;
        range = _stats.range;

        // _rangeTrigger.radius = _stats.range; // uhhh this one doesn't work fuck i'll fix it later
    }

    void Update()
    {
        
    }

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
        // return normalCooldown;
        return 0f;
    }

    public void SetNormalCooldown(float normalCooldown)
    {
        // this.normalCooldown = normalCooldown;
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

    public void UpgradeTower()
    {
        if (upgradeTier != 4)
        {
            upgradeTier++;
        }
    }

    // other

    public int GetWidth()
    {
        return _stats.width;
    }

    public List<TowerTags> GetTowerTag()
    {
        return _stats.tags;
    }

    public bool CheckIfTagsHas(TowerTags tag)
    {
        if (_stats.tags.Contains(tag))
            return true;

        return false;
    }
}
