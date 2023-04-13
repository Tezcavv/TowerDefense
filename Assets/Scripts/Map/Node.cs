using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

    public float f, g, h;
    public Tile tile;

    public Node(Tile tile) {
        f = 0;
        g = 0;
        h = 0;
        this.tile = tile;
    }

}
