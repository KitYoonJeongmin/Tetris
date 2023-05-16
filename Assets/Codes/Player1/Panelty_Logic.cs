using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panelty_Logic : MonoBehaviour
{
    void Start()
    {

        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);
            BlockLogic1.grid[roundX, roundY] = children;
            
        }
        

    }
}
