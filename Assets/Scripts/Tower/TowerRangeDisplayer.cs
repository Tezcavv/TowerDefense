using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://forum.unity.com/threads/linerenderer-to-create-an-ellipse.74028/

[RequireComponent(typeof(Tower))]
[RequireComponent(typeof(LineRenderer))]
public class TowerRangeDisplayer : MonoBehaviour
{
   
    [SerializeField]
    [Tooltip("The number of lines that will be used to draw the circle. The more lines, the more the circle will be \"flexible\".")]
    [Range(0, 1000)]
    private int _segments = 60;

    private float radius;
 
    private LineRenderer _line;
 
    void Start()
    {

        Initialize();
  
    }
  
    public void Initialize() {

        _line = gameObject.GetComponent<LineRenderer>();

        radius = gameObject.GetComponent<Tower>().RangeRadius;

        _line.SetVertexCount(_segments + 1);
        _line.useWorldSpace = false;

        CreatePoints();
    }

    void CreatePoints()
    {

 
        float x;
        float y;
 
        float angle = 0f;
 
        for (int i = 0; i < (_segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            _line.SetPosition(i, new Vector3(y, 0, x));

            angle += (360f / _segments);
        }
    }
}
