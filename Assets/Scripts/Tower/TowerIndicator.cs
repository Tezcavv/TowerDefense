using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TowerIndicator : MonoBehaviour
{
    // Start is called before the first frame update

    private List<MeshRenderer> renderers;
    private Color validColor;
    private Color notValidColor;

    private Color currentColor;

    void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>().ToList();
        validColor = new Color(0, 1, 0, 0.5f);//green 
        notValidColor = new Color(1, 0, 0, 0.5f);//red
        currentColor = Color.black;
    }

    // Update is called once per frame
    void FixedUpdate() {


        // TODO
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, 100, 1 << 6)) 
            return;

        if (hit.collider.gameObject.GetComponent<Tile>().IsWalkable)
            ChangeColor(false);
        else
            ChangeColor(true);


        transform.position = hit.point;

    }


    void ChangeColor(bool isValid) {

        if (isValid && currentColor == validColor)
            return;

        if (!isValid && currentColor == notValidColor)
            return;

        currentColor = isValid ? validColor: notValidColor;
        renderers.ForEach(renderer => renderer.material.color = currentColor);

    }

}
