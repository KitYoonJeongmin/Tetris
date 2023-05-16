using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Panelty_Logic1 : MonoBehaviour
{
    void Start()
    {

        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);
            if(string.Compare(SceneManager.GetActiveScene().name, "computer", true) == 0)
                BlockLogic2.grid[roundX, roundY] = children;
            else
                BlockLogic.grid[roundX, roundY] = children;
            
        }
        

    }
}
