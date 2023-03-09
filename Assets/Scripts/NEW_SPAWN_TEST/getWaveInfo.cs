using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getWaveInfo : MonoBehaviour
{
    [SerializeField] GameObject[] enObj;
    [SerializeField] int[] quantity;

    public Dictionary<int, Enemy> keyValuePairs;

    private void Start()
    {
        for (int enemyIndex = 0; enemyIndex < enObj.Length; enemyIndex++)
        {
            Enemy newEnemy = new Enemy(quantity[enemyIndex], enObj[enemyIndex]);
            keyValuePairs.Add(enemyIndex, newEnemy);
        }
    }

    public struct Enemy
    {
        // Number of objects of this type
        public int quantity;

        // Enemy object prefab for this type
        public GameObject type;


        // Constructor:
        public Enemy(int quantity, GameObject type)
        {
            this.quantity = quantity;
            this.type = type;
        }
    }
}
