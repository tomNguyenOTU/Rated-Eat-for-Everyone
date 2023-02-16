using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerTags
{
    Blueberry,
    Watermelon
}

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

    [SerializeField] private List<TowerTags> tags;

    // used as reference
    private float maxHealth;
    private float normalCooldown;
    private float currentBuildTimer;

    // components of tower
    private Transform _trans;
    private UtilityDistComparison _dist;

    // other
    private GameObject _enemyTarget;

    // private float lastAttackTime = Time.realtimeSinceStartup;
    private int upgradeTier = 0;

    void Start()
    {
        _trans = GetComponent<Transform>();
        _dist = GetComponent<UtilityDistComparison>();
    }

    void Update()
    {
        
    }

    private void Attack()
    {

    }

    //pseudocode for now, uncomment when testing this out or something
    public void UpdateTowerTarget()
    {
        List<GameObject> enemies = new List<GameObject>(); // grab a list of enemies from a manager script somewhere; this line is a placeholder

        //selfnote for later: consider this solution for tags: https://answers.unity.com/questions/1470694/multiple-tags-for-one-gameobject.html
    
        enemies = _dist.CheckDistance(enemies, range); // remove all enemies not in range

        if (enemies.Count == 0)
        {
            _enemyTarget = null;
            return;
        }

        List<GameObject> temp = new List<GameObject>();

        // (this should be the last thing checked, but i need to pass this onto luc because i need enemy class stuff later)
        // find closest enemy to tower
        List<Transform> targets = _dist.ConvertObjToTrans(enemies);
        _enemyTarget = enemies[_dist.CheckDistance(targets)];
    }

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

    public List<TowerTags> GetTowerTag()
    {
        return tags;
    }

    public bool CheckIfTagsHas(TowerTags tag)
    {
        if (tags.Contains(tag))
            return true;

        return false;
    }
}
