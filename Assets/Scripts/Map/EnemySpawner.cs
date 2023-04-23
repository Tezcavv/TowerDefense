using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Tile> spawnTiles;
    [SerializeField] private List<Enemy> enemyPrefabs;

    [SerializeField] private float spawnIntervalSeconds = 2f;
    [SerializeField] private float increaseDifficultyTimer = 10f;
    public float difficultyLevel = 1;
    private bool keepSpawning;




    // Start is called before the first frame update
    void Start() {
        keepSpawning = true;

        StartCoroutine(IncreaseDifficulty());
        StartCoroutine(SpawnEnemy());

    }

    private IEnumerator IncreaseDifficulty() {

        while (keepSpawning && spawnIntervalSeconds > 0.4f) {
            yield return new WaitForSeconds(increaseDifficultyTimer);
            difficultyLevel += 0.1f;
            spawnIntervalSeconds -= 0.2f;
            
        }

        StopSpawn();

    }

    public IEnumerator SpawnEnemy() {

        while (keepSpawning) {

            Enemy enemy = Instantiate(enemyPrefabs[Random.Range(0,enemyPrefabs.Count)]);
            enemy.StartTile = spawnTiles[Random.Range(0, spawnTiles.Count)];
            enemy.speed *= difficultyLevel;
            enemy.initialHp *= difficultyLevel ;
            enemy.transform.localScale *= difficultyLevel;
            enemy.Initialize();
            enemy.OnDeath.AddListener(OnEnemyDeath);
            enemy.OnGoalReached.AddListener(OnGoalReached);

            yield return new WaitForSeconds(spawnIntervalSeconds);
        }
    }

    public void StopSpawn() {
        keepSpawning = false;
    }


    public void OnEnemyDeath(int enemyGold) {
        GameManager.Instance.AddGold(enemyGold);
    }

    public void OnGoalReached() {
        GameManager.Instance.GameOver();
    }

}
