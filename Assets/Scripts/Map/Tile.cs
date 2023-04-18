using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{

    protected int row;
    protected int col;
    protected List<Tile> tiles = new();

    [SerializeField] protected bool isWalkable = false;
    [SerializeField] protected bool isGoal = false;

    public bool IsWalkable => isWalkable;

    public bool IsGoal => isGoal;

    public virtual int Row { get => row; set => row = value; }
    public virtual int Column { get => col; set => col = value; }
    public virtual List<Tile> AdjacentTiles { get => tiles; set => tiles = value; }

}
