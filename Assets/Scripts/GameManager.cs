using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    private int playerGold = 0;

    private void Awake() {
        SetInstance();
    }

    public void AddGold(Enemy enemy) {
        playerGold += enemy.goldOnDeath;
        Debug.Log(playerGold);
    }


    private void SetInstance() {

        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;
    }
}
