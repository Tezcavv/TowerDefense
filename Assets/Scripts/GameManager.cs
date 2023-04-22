using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Tower towerToSpawn;
    [SerializeField] private int playerGold = 120;

    public bool isPlacingTower;

    public UnityEvent<int> OnGoldUpdate;

    public int PlayerGold {
        get => playerGold;
        set {
            playerGold = value;
            OnGoldUpdate?.Invoke(playerGold);
        }
    }

    private void Awake() {
        SetInstance();
        isPlacingTower= false;
    }

    public void AddGold(int gold) {
        PlayerGold += gold;
    }

    private void Update() {

        if (isPlacingTower) {
            HandlePlacingTower();
        }

    }

    public void PlaceTower(Tower towerToBeSpawned) {
        if (GameManager.Instance.playerGold < towerToBeSpawned.BaseCost) {
            //NON HO ABBASTANZA GOLD
            Debug.Log("Not Enough Gold");
            return;
        }
        towerToSpawn = towerToBeSpawned;
        isPlacingTower = true;
    }

    private void HandlePlacingTower() {



        if (Input.GetMouseButtonDown(0)) {
            SpawnTower();
            isPlacingTower = false;
        }
    }

    public void SpawnTower() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, 100, 1 << 6))
            return;

        GameObject tile = hit.collider.gameObject;

        if (tile.GetComponent<Tile>().IsWalkable) 
            return;
        

        float offsetY= tile.GetComponent<MeshRenderer>().bounds.size.y/2;
        Instantiate(towerToSpawn, tile.transform.position + new Vector3(0, offsetY, 0),tile.transform.rotation);
        PlayerGold -= towerToSpawn.BaseCost;
    }

    public void GameOver() {
        ResetGame();
    }

    private void SetInstance() {

        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void ResetGame() {
        SceneManager.LoadScene(0);
    }
}
