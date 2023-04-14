using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Node
{

    public float f, g, h;
    public Tile tile;
    public Node parent;

    public Vector3 TilePosition => tile.transform.position;

    public Node(Tile tile) {
        f = 0;
        g = 0;
        h = 0;
        this.tile = tile;
    }



}
