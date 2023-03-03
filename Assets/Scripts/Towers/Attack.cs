using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject target;

    public Rigidbody2D _rb;
    public Tower _tower;
    public TowerStats _stats;
    public UtilityDistComparison _dist;

    public List<GameObject> enemiesInRange = new List<GameObject>();

    private void Start() // why the fuck am i not allowed to use a contructor here :(
    {
        TryGetComponent<Tower>(out _tower);

        if (_tower == null)
        {
            Destroy(gameObject.GetComponent<Attack>());
            return;
        }

        _rb = GetComponent<Rigidbody2D>();
        _stats = GetComponent<TowerStats>();
        _tower = GetComponent<Tower>();
        _dist = GameObject.Find("Utility").GetComponent<UtilityDistComparison>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemiesInRange.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemiesInRange.Remove(collision.gameObject);
        }
    }
    public virtual IEnumerator AttackLoop() // remember to only call this in a single frame kthanks
    {
        // copy this when using an override
        yield return new WaitForSeconds(_stats.atkDelay);

        if (target == null)
        {
            yield break;
        }
    }

    public virtual void UpdateTarget()
    {
        // default: targets closest, then starts to attack
        // (i should actually tweak this later)

        if (enemiesInRange.Count > 0)
        {
            target = enemiesInRange[_dist.CheckDistance(enemiesInRange)]; // need to rewrite
            StartCoroutine(AttackLoop());
        }
    }

    public void TrackTarget(GameObject target)
    {
        Vector2 lookDirection = target.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.forward);

        _rb.SetRotation(rotation);
    }
}
