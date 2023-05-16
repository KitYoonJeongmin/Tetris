# Tetris

![image](https://github.com/KitYoonJeongmin/Tetris/assets/102850131/7746f63a-e909-470e-a80d-07dac95a7a86)

Unity를 사용한 인공지능 테트리스
- 제작기간 2021.11.12~ 2021.11.20
- 사용엔진: Unity
- 플랫폼: Windows

## 1. 서론
### 1.1. 테트리스
    - 테트로미노 : 4개의 정사각형으로 이루어진 폴리오미노
      
    - 테트로미노는 자동으로 낙하된다.
    - 테트로미노는 방향키 입력에 따라 좌우로 움직인다.
    - 테트로미노는 윗 방향키 입력에 90도 회전한다.
    - 낙하된 테트로미노는 쌓인다.
    - 쌓인 테트로미노가 x축으로 한줄이 모두 차면 삭제되고 위의 테트로미노들이 빈 아래로 내려온다.
    - 테트로미노가 보드 범위를 벗어나게 쌓이면 게임이 종료된다.
    - 게임보드
    
    - 높이 : 20, 넓이 : 10
### 1.2. 개발 목표
    - 1인용 테트리스 개발
    - 대전가능 테트리스 개발
    - 테트리스 인공지능 개발

## 2. 실행 흐름도
![image](https://github.com/KitYoonJeongmin/Tetris/assets/102850131/73260e2d-f305-45b1-8ce4-c67075a7b499)


## 3. 시스템 구성
 - 씬에따라 사용된 c#파일
![image](https://github.com/KitYoonJeongmin/Tetris/assets/102850131/044b73e5-0e2e-4ef8-9bb0-6eab1da6bf37)

## 4. 자료구조 설계
- 내부 데이터 : Transform[,] grid = new Transform[width, height + 10]
- 보드판을 grid로 구현하여 떨어진 테트로미노의 좌표값 자리에 테트로미노의 각각의 블록의 Transform을 저장한다.
- 블록의 y좌표값이 보드판을 넘어가게 된다면 게임은 종료된다.
```
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
```

## 5. 모듈설계
![image](https://github.com/KitYoonJeongmin/Tetris/assets/102850131/2fde8674-a132-4d7d-a04f-12ee4d5da0ea)

## 6. 주요 알고리즘
- AI Paly의 블록생성 알고리즘
![image](https://github.com/KitYoonJeongmin/Tetris/assets/102850131/9130851a-f069-48f3-80a0-4294796b6ec8)
  - AI Logic
  ```
  public void  NewAiTetris(int point)
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
  ```
  - AIBlockLogic
    ```
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
  ```
## 7. 개발내용 및 실행화면
### 7.1. 메뉴화면
 - 버튼을 누르면 게임 화면으로 실행된다.
 
![image](https://github.com/KitYoonJeongmin/Tetris/assets/102850131/5f42c4e7-4907-4af9-95fb-ae6deb9dfd37)

### 7.2. Player1
 - 혼자 플레이가 가능하다.
 - 블록을 쌓게 되면 score점수가 +1된다.
 - 메뉴 버튼을 누르면 메뉴 화면으로 돌아간다.
 - 방향키는 A:좌 D:우 W:회전
 - 게임이 종료되면 회색화면이 올라간다.(밑에서 확인가능)
 
![image](https://github.com/KitYoonJeongmin/Tetris/assets/102850131/8a830ae0-2139-4bdd-8a0f-d8f6ad1332bf)

### 7.3. Player1vs2
- 두명이서 플레이가 가능하다.
- 한줄을 완성하게 되면 상대방에게 회색블럭의 패널티가 적용된다.
- 방향키는 A:좌 D:우 W:회전 | 왼쪽 방향키:좌, 오른쪽 방향키:우 위쪽 방향키:회전 

![image](https://github.com/KitYoonJeongmin/Tetris/assets/102850131/95340d62-8369-4ca1-a454-a18d5e0c8620)

### 7.4. Ai
- ai혼자서 플레이한다.

![image](https://github.com/KitYoonJeongmin/Tetris/assets/102850131/5bc56907-8ae0-4977-aa02-76f1991d2a1e)

### 7.5. Vs Computer
- 컴퓨터와 테트리스 대전을 할 수 있다.
- 방향키는 왼쪽 방향키:좌, 오른쪽 방향키:우 위쪽 방향키:회전 

![image](https://github.com/KitYoonJeongmin/Tetris/assets/102850131/bf8a7f4d-1ce3-480f-8cd6-ab628253f383)

## 8. 느낀점
> 이번 개발을 하면서 처음으로 복잡한 GUI를 구현하였고 처음으로 Unity를 사용하였다. 문제 분석 과정에서 winForm을 이용하려 하였다. 하지만 다른분이 발표를 하면서 c#을 사용하고 유니티로 개발을 한다 하여 unity가 무엇인지 궁금해져 인터넷에 검색을 해보았다. 다양한 유니티로 만든 결과물들을 보니 유니티라는 게임엔진을 이용해 게임을 만들고 싶어졌다. 그래서 유니티로 구현을 하게 되었는데 C#도 익숙하지 않은 상태에서 unity도 처음 다뤄 하나 구현 할때마다 검색만 몇 번씩 하였다. 하지만 단순한 테트리스를 완성하고 나니 지금까지 만든 게임중 가장 게임다워서 만족스러웠다.
그리고 ai를 구현하게 되었는데 초반에는 고려사항을 3개정도 생각했었다. 하지만 그렇게 구현하니 ai가 나보다 테트리스를 못하였다. 그래서 고려 사항을 2개 더 늘렸더니 제법ai 같아졌다. 하지만 교수님께서 말씀하신 다음 테트로미노까지 고려하는 방법은 구현하지 못해 정말 아쉽다. 그리고 이번학기에 탐색과 ai에 대한 문제를 풀면서 조금 공부해보았는데 ‘어렵고 재미없는 분야’라는 생각이 많이 바뀐 것 같다. 특히 이 문제와 같은 완전탐색의 경우는 최적의 경우를 빠르게 (사람기준)찾아낸다는 점이 매력적이다. 겨울방학 동안 이와 관련된 공부를 해보고 싶다.

