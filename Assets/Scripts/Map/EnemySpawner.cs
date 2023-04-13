using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Enemy[] pool;

    void SpawnEnemy() {
        //pool[0].Initialize();
        pool[0].OnDeath.AddListener(OnEnemyDeath);
    }

    void DeactivateEnemy() {
        //pool[0].Deactivate();
        pool[0].OnDeath.RemoveListener(OnEnemyDeath);

    }
    private void OnEnemyDeath(float arg0) {
        
    }

    //spawna i nemici prendendoli dalla pool

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
