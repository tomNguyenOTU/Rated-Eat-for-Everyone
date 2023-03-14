using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getWaveInfo : MonoBehaviour
{
    [SerializeField] GameObject[] enObj;
    [SerializeField] int[] quantity;

    public Dictionary<int, Enemy> keyValuePairs;

    /* Get information from all "Waves" objects.
         * 
         * Information is saved in a Dictionary with the format: [Wave Number, Wave Dictionary],
         * Where [Wave Dictionary] has information on each individual enemy type:
         * 
         *      [Wave Dictionary] = [Enemy Index, Enemy Info]
         *      
         *      Such that:
         *      
         *          [Enemy Info] = 
         *          {
         *              GameObject type;
         *              int quantity;
         *          }
         *          
         *      Enemy Info is a struct with variables for an enemies type - which prefab will be used to spawn,
         *      And quantity - how many enemies of this type are spawned.
         *      
         *      Enemies are spawned in the order they appear.
         *      Once all enemies of one type are spawned, 
         *      The next group is automatically spawned until all types have been spawned as many times as indicated in [quantity]
         *      
         *      EX: Type A, Quantity 10,
         *          Type B, Quantity 5
         *          
         *          First, 10 objects of Type A are spawned,
         *          Then, 5 objects of Type B are spawned.
         */

    private void Awake()
    {
        Waves waves = GameObject.Find("FLOW_CONTROLLER").GetComponent<Waves>();

        keyValuePairs = new Dictionary<int, Enemy>();

        for (int enemyIndex = 0; enemyIndex < enObj.Length; enemyIndex++)
        {
            Enemy newEnemy = new(quantity[enemyIndex], enObj[enemyIndex]);
            keyValuePairs.Add(enemyIndex, newEnemy);
        }

        waves.waveInfo = new Dictionary<int, Dictionary<int, Enemy>>();

        GameObject[] wave_objs = GameObject.FindGameObjectsWithTag("Waves");
        for (int waveIndex = 0; waveIndex < wave_objs.Length; waveIndex++)
        {
            waves.waveInfo.Add(waveIndex, keyValuePairs);
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
