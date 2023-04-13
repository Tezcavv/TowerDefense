using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public static Grid Instance { get; private set; }

    private  List<Tile> allTiles= new List<Tile>();
    private  List<Tile> walkableTiles= new List<Tile>();
    private  List<Tile> buildableTiles = new List<Tile>();

    private List<List<Tile>> tileMatrix;

    #region Properties / Getters
    public  List<Tile> BuildableTiles => buildableTiles;
    public  List<Tile> WalkableTiles=> walkableTiles;
    #endregion

    private void Awake() {

        SetInstance();

        allTiles = FindObjectsOfType<Tile>().ToList();
        walkableTiles = allTiles.FindAll(tile => tile.IsWalkable);
        buildableTiles = allTiles.FindAll(tile => !tile.IsWalkable);

        InitMatrix();

    }

    #region Matrix Init
    private void InitMatrix() {
        tileMatrix = new List<List<Tile>>();

        int rowCount = 0;
        foreach (Tile node in allTiles) {
            if (tileMatrix[rowCount].Count == 0) {
                //primo elemento della riga
                tileMatrix[rowCount].Add(node);
            } else if (node.transform.position.x == tileMatrix[rowCount][0].transform.position.x) {
                tileMatrix[rowCount].Add(node);
            } else {
                //creo nuova riga
                rowCount++;
                tileMatrix.Add(new List<Tile> { node });
            }

        }
        foreach (List<Tile> row in tileMatrix) {
            row.Sort((node2, node1) => node1.transform.position.z.CompareTo(node2.transform.position.z));
        }

        SetAdjacentTiles();
    }
    private void SetAdjacentTiles() {


        for (int iRow = 0; iRow < tileMatrix.Count; iRow++) {
            for (int iCol = 0; iCol < tileMatrix[iRow].Count; iCol++) {


                tileMatrix[iRow][iCol].Row = iRow;
                tileMatrix[iRow][iCol].Column = iCol;

                //assegnazione con null-check
                if (!tileMatrix[iRow][iCol].IsWalkable) {
                    continue;
                }

                if (iRow > 0 && tileMatrix[iRow - 1][iCol].IsWalkable)
                    tileMatrix[iRow][iCol].AdjacentTiles.Add(tileMatrix[iRow - 1][iCol]);

                if (iRow < tileMatrix.Count - 1 && tileMatrix[iRow + 1][iCol].IsWalkable)
                    tileMatrix[iRow][iCol].AdjacentTiles.Add(tileMatrix[iRow + 1][iCol]);

                if (iCol > 0 && tileMatrix[iRow][iCol - 1].IsWalkable)
                    tileMatrix[iRow][iCol].AdjacentTiles.Add(tileMatrix[iRow][iCol - 1]);

                if (iCol < tileMatrix[iRow].Count - 1 && tileMatrix[iRow][iCol + 1].IsWalkable)
                    tileMatrix[iRow][iCol].AdjacentTiles.Add(tileMatrix[iRow][iCol + 1]);


            }
        }
    }
    #endregion

    private void SetInstance() {

        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;
    }



}
