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
    private float health;
    private float damage;
    private float cooldown;
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
    private CircleCollider2D _rangeTrigger;

    // other
    [SerializeField] private GameObject _enemyTarget;
    List<GameObject> _enemiesInRange = new List<GameObject>();
    private bool canAttack = true;

    // private float lastAttackTime = Time.realtimeSinceStartup;
    private int upgradeTier = 0;



    void Start()
    {
        _trans = GetComponent<Transform>();
        _dist = GetComponent<UtilityDistComparison>();

        health = GetComponent<TowerStats>().hitPoints;
        damage = GetComponent<TowerStats>().atkDamage;
        cooldown = GetComponent<TowerStats>().atkDelay;

        _rangeTrigger.radius = range; // uhhh this one doesn't work fuck i'll fix it later
    }

    void Update()
    {
        if (_enemyTarget != null && canAttack == true)
        {
            StartCoroutine(Attack());
        }
        else if (_enemyTarget == null)
        {
            UpdateTowerTarget();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            _enemiesInRange.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            _enemiesInRange.Remove(collision.gameObject);
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;

        EnemyStats targetStats = _enemyTarget.GetComponent<EnemyStats>();
        targetStats.SetHitPoints(targetStats.GetHitPoints() - damage);

        if (targetStats.GetHitPoints() <= 0) 
        {
            // _enemyTarget.Die();
            Destroy(_enemyTarget);
            _enemyTarget = null;
        }

        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

    //pseudocode for now, uncomment when testing this out or something
    public void UpdateTowerTarget()
    {
        if (_enemiesInRange.Count == 0)
        {
            _enemyTarget = null;
            return;
        }

        List<GameObject> temp = new List<GameObject>();

        // (this should be the last thing checked, but i need to pass this onto luc because i need enemy class stuff later)
        // find closest enemy to tower
        List<Transform> targets = _dist.ConvertObjToTrans(_enemiesInRange);
        _enemyTarget = _enemiesInRange[_dist.CheckDistance(targets)];
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
