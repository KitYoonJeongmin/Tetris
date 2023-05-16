using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBlockLogic : MonoBehaviour
{
    public GameObject[] Tetris;
    public GameObject GameOverBg;
    public GameObject pnt_Tetris;
    
    // Start is called before the first frame update
    void Start()
    {
        Point.point = 0;
        NewTetirs();
    }
    public void NewTetirs()
    {
        Instantiate(Tetris[Random.Range(0, Tetris.Length)], this.transform.position, Quaternion.identity);
        
    }
    public void NewPntTetris(int i)
    {
        Instantiate(pnt_Tetris, new Vector3(i, 0, 0), Quaternion.identity);
        
    }

    public void GameOver()
    {
        Instantiate(GameOverBg, GameOverBg.transform.position, Quaternion.identity);
    }

}


