using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid : MonoBehaviour
{
    
    private static List<Tile> allTiles= new List<Tile>();
    private static List<Tile> walkableTiles= new List<Tile>();

    private void Awake() {
        allTiles = FindObjectsOfType<Tile>().ToList();
        walkableTiles = allTiles.FindAll(tile => tile.IsWalkable);
    }





}
