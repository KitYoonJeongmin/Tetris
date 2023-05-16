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
    //�ٳ�����
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
    //�� ä���
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
        Insert(); //ä���
    }


    //������ ä���� ����
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
    //ä���� ���� ����
    void DeleteLine(int i)
    {
        for (int j = s_width; j < width && grid[j, i] != null; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }
    //�ٳ�����
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

    //������ ä�����ٸ� ���� �� �ٳ�����
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
    //��Ͻױ�
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


    //����� ������ ����
    public bool Valid()
    {
        {

            foreach (Transform children in transform)
            {

                int roundX = Mathf.RoundToInt(children.transform.position.x);
                int roundY = Mathf.RoundToInt(children.transform.position.y);

                //�������� �����Ѵٸ�
                if (roundX < s_width || roundX >= width || roundY < 0)
                {
                    return false;
                }

                //���� ��ϰ� �����ٸ�
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
        //���� �̵�
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!Valid())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        //������ �̵�
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!Valid())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        //ȸ��
        else if (Input.GetKeyDown(KeyCode.W))
        {

            transform.RotateAround(transform.TransformPoint(RotationPoint), new Vector3(0, 0, 1), 90);
            if (!Valid())
            {
                transform.RotateAround(transform.TransformPoint(RotationPoint), new Vector3(0, 0, 1), -90);
            }



        }
        //��������
        if (Time.time - timer > (Input.GetKey(KeyCode.S) ? falltime / 15 : falltime))
        {
            transform.position += new Vector3(0, -1, 0);

            if (!Valid())
            {
                transform.position -= new Vector3(0, -1, 0); // �̵�
                Point.point++; //���� +1
                AddToGrid(); // ä���
                CheckForLine(); // ���� ä�������� Ȯ��
                this.enabled = false;
                if (gameover != true) //���ӿ����� �ƴ϶��
                {
                    FindObjectOfType<NewBlockLogic>().NewTetirs(); //�� �ٽ� ����
                }
                else
                {
                    FindObjectOfType<NewBlockLogic>().GameOver(); //���ӿ���ȭ��
                }
            }

            timer = Time.time;
        }

    }

}
