using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Tile> spawnTiles;
    [SerializeField] private List<Enemy> enemyPrefabs;
    [SerializeField] private int amountToPool = 0;

    private List<Enemy> enemyPool;
    private List<Enemy> activeEnemies;




    // Start is called before the first frame update
    void Start() {
        enemyPool = new List<Enemy>();
       // per adesso voglio X nemici di ogni tipo disponibili
        for (int i = 0; i < amountToPool; i++) {
            foreach (Enemy enemy in enemyPrefabs) {
                enemyPool.Add(Instantiate(enemy));
            }
        }
        enemyPool.ForEach(enemy => {
            enemy.OnDeath.AddListener(OnEnemyDeath);
            enemy.gameObject.SetActive(false);
        });
    }

    public void SpawnEnemy() {

        Enemy toSpawn = enemyPool[Random.Range(0, enemyPool.Count)];

        enemyPool.Remove(toSpawn);
        activeEnemies.Add(toSpawn);

        toSpawn.gameObject.SetActive(true);
        toSpawn.StartTile = spawnTiles[Random.Range(0, spawnTiles.Count)];
        toSpawn.Initialize();

    }


    public void OnEnemyDeath(Enemy enemy) {

        GameManager.Instance.AddGold(enemy);

        enemy.gameObject.SetActive(false);

        activeEnemies.Remove(enemy);
        enemyPool.Add(enemy);


    }



    // Spawn Manuale per testing
    void Update() {


        if (Input.GetKeyDown(KeyCode.P)) {
            SpawnEnemy();
        }
    }
}
