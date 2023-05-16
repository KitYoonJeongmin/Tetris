using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLogic : MonoBehaviour
{
    public Vector3 RotationPoint;
    private float timer;
    public float falltime = 1.0f;
    public static int height = 20;
    public static int width = 10;
    public int s_width = 0;
    public static Transform[,] grid = new Transform[width + 40, height + 10];
    bool gameover = false;

    //private static Transform[,] grid = new Transform[width+40, height+10];

    void Start()
    {

    }
    //줄내리기
    void RowUp()
    {
        for (int y = height; y >=0 ; y--)
        {
            for (int j = s_width; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y + 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y + 1].transform.position += new Vector3(0, 1, 0);
                }
            }
        }
    }
    //줄 채우기
    public void Insert()
    {
        int ran;
        ran = Random.Range(s_width, width);
        for (int i = s_width; i < width; i++)
        {
            if (i != ran)
                FindObjectOfType<NewBlockLogic>().NewPntTetris(i);
        }
    }

    public void Penalty()
    {
        RowUp();
        Insert(); //채우기
    }


    //라인의 채워짐 유무
    bool HasLine(int i)
    {
        for (int j = s_width; j < width; j++)
        {

            if (grid[j, i] == null)
            {

                return false;
            }

        }

        return true;
    }
    //채워진 라인 삭제
    void DeleteLine(int i)
    {
        for (int j = s_width; j < width && grid[j, i] != null; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }
    //줄내리기
    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = s_width; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    //라인이 채워졌다면 삭제 및 줄내리기
    public void CheckForLine()
    {
        int delline = 0;
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                delline++;
                DeleteLine(i);
                RowDown(i);
            }
        }

        if (delline >= 1)
        {
            FindObjectOfType<BlockLogic1>().Penalty();

        }


    }
    //블록쌓기
    public void AddToGrid()
    {

        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);
            grid[roundX, roundY] = children;
            if (roundY >= height)
            {
                gameover = true;
            }

        }
    }
    // Update is called once per frame


    //블록의 움직임 범위
    public bool Valid()
    {
        {

            foreach (Transform children in transform)
            {

                int roundX = Mathf.RoundToInt(children.transform.position.x);
                int roundY = Mathf.RoundToInt(children.transform.position.y);

                //벽밖으로 가려한다면
                if (roundX < s_width || roundX >= width || roundY < 0)
                {
                    return false;
                }

                //쌓인 블록과 만난다면
                if (grid[roundX, roundY] != null)
                {
                    
                    return false;
                }
            }


            return true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //왼쪽 이동
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!Valid())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        //오른쪽 이동
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!Valid())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        //회전
        else if (Input.GetKeyDown(KeyCode.W))
        {

            transform.RotateAround(transform.TransformPoint(RotationPoint), new Vector3(0, 0, 1), 90);
            if (!Valid())
            {
                transform.RotateAround(transform.TransformPoint(RotationPoint), new Vector3(0, 0, 1), -90);
            }



        }
        //내려가기
        if (Time.time - timer > (Input.GetKey(KeyCode.S) ? falltime / 15 : falltime))
        {
            transform.position += new Vector3(0, -1, 0);

            if (!Valid())
            {
                transform.position -= new Vector3(0, -1, 0); // 이동
                Point.point++; //점수 +1
                AddToGrid(); // 채우기
                CheckForLine(); // 라인 채워졌는지 확인
                this.enabled = false;
                if (gameover != true) //게임오버가 아니라면
                {
                    FindObjectOfType<NewBlockLogic>().NewTetirs(); //블럭 다시 생성
                }
                else
                {
                    FindObjectOfType<NewBlockLogic>().GameOver(); //게임오버화면
                }
            }

            timer = Time.time;
        }

    }

}
