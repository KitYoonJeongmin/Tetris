using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBlockLogic : MonoBehaviour
{
    public Vector3 RotationPoint;
    static int s_width = 0;
    static int height = 20;
    static int width = 10 + s_width;
    
    int point = -1000;
    // Start is called before the first frame update
    void Start()
    {
        
        if(!XY())
        {
            FindObjectOfType<AiLogic>().NewAiTetris(point);
            Destroy(gameObject);
            this.enabled = false;
        }
        else
        {
            point = 0;
            while (Valid())
                transform.position += new Vector3(0, -1, 0); // 이동
            foreach (Transform children in transform)
            {
                int roundX = Mathf.RoundToInt(children.transform.position.x);
                int roundY = Mathf.RoundToInt(children.transform.position.y);

                BlockLogic2.grid[roundX, roundY] = children;
            }
            SidePoint();
            BlankPoint();
            NearPoint();
            LinePoint();
            Height();
            
            foreach (Transform children in transform)
            {
                int roundX = Mathf.RoundToInt(children.transform.position.x);
                int roundY = Mathf.RoundToInt(children.transform.position.y);
                BlockLogic2.grid[roundX, roundY] = null;
            }
            FindObjectOfType<AiLogic>().NewAiTetris(point);
            Destroy(this.gameObject);
            this.enabled = false;
        }    
    }

    //가장자리에 가까이 있으면 있으면(가중치 1)
    void SidePoint()
    {
        int roundX;
        foreach (Transform children in transform)
        {
            roundX = Mathf.RoundToInt(children.transform.position.x);
            if (0 > (roundX - 4))
                point += (-1) * (roundX - 4);
            else
                point += roundX - 4;
        }

    }
    //빈칸이 있으면(4x4)(가중치 -6)
    void BlankPoint()
    {
        int s_roundX = 10;
        int e_roundX = 0;
        int s_roundY = 0;
        int e_roundY = 20;
        /*
        foreach (Transform children in transform)
        {
            if(s_roundY< Mathf.RoundToInt(children.transform.position.y))
                s_roundY = Mathf.RoundToInt(children.transform.position.y);
            else if (e_roundY > Mathf.RoundToInt(children.transform.position.y))
                e_roundY = Mathf.RoundToInt(children.transform.position.y);
            if (e_roundY > 0)
                e_roundY--;
            if (e_roundX < Mathf.RoundToInt(children.transform.position.x))
                e_roundX = Mathf.RoundToInt(children.transform.position.x);
            else if (s_roundX > Mathf.RoundToInt(children.transform.position.x))
                s_roundX = Mathf.RoundToInt(children.transform.position.x);
        }
        

        for (int j=s_roundY;  j>e_roundY ;j--)
        {
            for(int k=s_roundX;  k<e_roundX ;k++)
            {
                if (BlockLogic2.grid[k, j] == null)
                    point -= 6;
            }
        }
        */
        for (int j = 0; j < width; j++)
        {
            int a = 0;
            for (int i = height - 1; i >= 0; i--)
            {
                if (BlockLogic2.grid[j, i] == null)
                {
                    if (a > 0)
                        point -= 6;
                }
                else
                    a++;
            }
        }
    }
    //주변에 블럭이 있을시(가중치 +3)
    void NearPoint()
    {
        int roundX = 0;
        int roundY = 0;
        foreach (Transform children in transform)
        {
            roundY = Mathf.RoundToInt(children.transform.position.y);
            roundX = Mathf.RoundToInt(children.transform.position.x);
            if(roundX>0)
            {
                if (BlockLogic2.grid[roundX - 1, roundY] != null)
                    point += 3;
            }
            if (roundX < width-1)
            {
                if (BlockLogic2.grid[roundX + 1, roundY] != null)
                    point += 3;
            }
            if (roundY > 0)
            {
                if (BlockLogic2.grid[roundX, roundY - 1] != null)
                    point += 3;
            }
            else
                point += 3;
        }

    }
    //한 줄을 채운다면(가중치20)
    void LinePoint()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                point += 100;
            }
        }
    }
    //높이가 높을 수록(가중치 -2)
    void Height()
    {
        int roundY = 0;
        foreach (Transform children in transform)
        {
            roundY = Mathf.RoundToInt(children.transform.position.y);
            point -= roundY *2;
        }
    }
    public bool HasLine(int i)
    {
        for (int j = s_width; j < width; j++)
        {

            if (BlockLogic2.grid[j, i] == null)
            {
                return false;
            }

        }

        return true;
    }
    public bool XY()
    {
        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);

            //벽밖으로 가려한다면
            if (roundX < s_width || roundX >= width)
            {
                return false;
            }
        }
        return true;
    }
    public bool Valid()
    {
        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);

            if (roundY < 0 )
            {
                transform.position -= new Vector3(0, -1, 0);
                return false;
            }
            
            //쌓인 블록과 만난다면
            if (BlockLogic2.grid[roundX, roundY] != null)
            {
                transform.position -= new Vector3(0, -1, 0);
                return false;
            }
        }
        return true;
    }
    // Update is called once per frame
    void Update()
    {


    }
}
