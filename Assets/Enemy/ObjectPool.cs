using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0, 50)] int poolSize = 4;
    [SerializeField] float levelDuration = 10f;
    [SerializeField] [Range(0.1f, 30f)] float spawnTimer = 2f;

    GameObject[] pool;

    void Awake()
    {
        PopulatePool();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for(int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }

    }

    void EnableObjectInPool()
    {
       for (int i = 0; i < pool.Length; i++)
       {
           if(pool[i].activeInHierarchy == false)
           {
               pool[i].SetActive(true);
               return;
           }
       }
    }

   IEnumerator SpawnEnemies()
   {    
       float timePassed = 0f;
       while (timePassed < levelDuration)
       {
            EnableObjectInPool();
            timePassed++;
            yield return new WaitForSeconds(spawnTimer);
       }   
   }
}
