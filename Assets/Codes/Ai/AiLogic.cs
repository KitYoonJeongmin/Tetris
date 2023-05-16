using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiLogic : MonoBehaviour
{
    public GameObject[] Tetris;
    public GameObject[] AiTetris;
    public GameObject GameOverBg;
    public GameObject pnt_Tetris;
    
    public static float maxpoint;
    public static int i;
    public static int j;
    public static int x;
    public static int angle;
    private int random;
    // Start is called before the first frame update
    void Start()
    {
        Point.point = 0;
        NewTetirs();
    }
    public void  NewAiTetris(float point)
    {
        if (point > maxpoint)
        {
            x = j - 1;
            angle = i;
            maxpoint = point;
            Debug.Log(maxpoint);
        }
        if (i == 4)
        {
            Instantiate(Tetris[random], this.transform.position+new Vector3(4,0,0) , Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        else
        {
            if (j == 10)
            {
                j = 0;
                i++;
            }
            Instantiate(AiTetris[random], this.transform.position + new Vector3(j, 0, 0), Quaternion.Euler(new Vector3(0, 0, 90 * i)));
            j++;
        }
    }
    public void NewTetirs()
    {
        Debug.Log("New");
        i = 0; 
        j= 0;
        random = Random.Range(0, Tetris.Length);
        maxpoint = -10000;
        NewAiTetris(-10000);
    }
    public void NewPntTetris(int i)
    {
        Instantiate(pnt_Tetris, new Vector3(i, 0, 0), Quaternion.identity);
    }

    public void GameOver()
    {
        Instantiate(GameOverBg, GameOverBg.transform.position, Quaternion.identity);
        Time.timeScale = 0;
    }



}


