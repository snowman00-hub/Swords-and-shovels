using UnityEngine;
using System.Collections.Generic;
using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] monsterPrefab;
    private int poolSize = 14;
    private int spawnInterval = 3;
    private List<GameObject> monsterList = new List<GameObject>();
    private Queue<GameObject> inActiveMonster = new Queue<GameObject>();
    private bool isSpawning = false;
    private bool allMonstersSpawned = false; 
    private int spawnedCount = 0;

    [SerializeField] private Vector3[] spawnPositions = new Vector3[14];
    private int currentSpawnIndex = 0;

    private void Start()
    {
        CreateMonsterPool();
        StartSpawning().Forget();   
    }

    private void CreateMonsterPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, monsterPrefab.Length);
            GameObject monster = Instantiate(monsterPrefab[randomIndex]);
            monster.SetActive(false);
            monsterList.Add(monster);
            inActiveMonster.Enqueue(monster);
        }
    }

    private async UniTaskVoid StartSpawning()
    {
        isSpawning = true;
        while (isSpawning && !allMonstersSpawned)
        {
            await UniTask.Delay(spawnInterval);
            if (inActiveMonster.Count > 0)
            {
                GameObject monster = inActiveMonster.Dequeue();
                ActivateMonster(monster);
                spawnedCount++;

                if (spawnedCount >= poolSize)
                {
                    allMonstersSpawned = true;
                    isSpawning = false;
                }
            }
        }
    }

    private void ActivateMonster(GameObject monster)
    {
        Vector3 spawnPos = spawnPositions[currentSpawnIndex];
        monster.transform.position = spawnPos;
        currentSpawnIndex = (currentSpawnIndex + 1) % spawnPositions.Length;
        monster.SetActive(true);

        MonsterHealth monsterHealth = monster.GetComponent<MonsterHealth>();
        if(monsterHealth != null)
        {
            WaitForMonsterDeath(monster, monsterHealth).Forget();
        }
    }

    private async UniTaskVoid WaitForMonsterDeath(GameObject monster, MonsterHealth monsterHealth)
    {
        await UniTask.WaitUntil(() => monsterHealth.isDead);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        ReturnToPool(monster);
    }

    public void ReturnToPool(GameObject monster)
    {
        monster.SetActive(false);
        //inActiveMonster.Enqueue(monster); 한번만 소환하므로 다시 넣지 않음
    }
}
