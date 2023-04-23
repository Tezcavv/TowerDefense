using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareTile : Tile 
    {

    private MeshRenderer m_Renderer;
    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
       
        List<Material> materials = new List<Material>();

        if (isWalkable) {
            materials = GridMap.Instance.WalkableTilesMaterial;  
        }
        else {
            materials = GridMap.Instance.BuildableTilesMaterial;
        }

        m_Renderer.material = materials[Random.Range(0, materials.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
