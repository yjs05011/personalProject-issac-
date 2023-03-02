using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMaker : MonoBehaviour
{
    // public Canvas canvas;
    // public RectTransform imgrect;
    // public Image[] rooms;
    // public Image makeImg;
    public int[,] bfsMap;
    public int[,] doubleCheck;
    public int[,] CheckMap;
    public int[,] Map;

    private List<int> bestWay;
    private int MapX;
    private int MapY;
    int randomX = 0;
    int randomY = 0;
    int randomNum = 0;
    int roomNum = 0;
    public bool[,] checkRoad;
    public bool[,] dupleMap;
    public int[,] direction;
    List<int> EndRoomX;
    List<int> EndRoomY;
    Stack<int> SpecialRoom;
    int bestWaySort;
    int bestWaynumber;
    bool FindMapCheck;



    void Awake()
    {
        MakeMap();


    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartBFS(int y, int x, int targetY, int targetX)
    {

        ClearCheckRoad();
        BFSNode bestNode = null;
        Queue<BFSNode> queue = new Queue<BFSNode>();
        queue.Enqueue(new BFSNode(y, x, null));
        checkRoad[y, x] = true;
        while (queue.Count > 0)
        {

            BFSNode node = queue.Dequeue();

            if (node.Y == targetY && node.X == targetX)
            {

                if (bestNode == null || (bestNode.PrevConut > node.PrevConut))
                {
                    bestNode = node;



                }
            }
            for (int i = 0; i < direction.GetLength(0); i++)
            {
                int dy = node.Y + direction[i, 0];
                int dx = node.X + direction[i, 1];

                if (CheckMapRange(dy, dx) && CheckMapWay(dy, dx) && !checkRoad[dy, dx])
                {
                    BFSNode searchNode = new BFSNode(dy, dx, node);
                    queue.Enqueue(searchNode);
                    checkRoad[dy, dx] = true;
                }
            }



        }
        bestWay.Add(bestNode.PrevConut);
        while (bestNode.PrevConut > 0)
        {
            CheckMap[bestNode.Y, bestNode.X] = 0;


            bestNode = bestNode.PrevNode;
        }




    }
    public void CheckBFS(int y, int x, int targetY, int targetX)
    {

        ClearCheckRoad();
        BFSNode bestNode = null;
        Queue<BFSNode> queue = new Queue<BFSNode>();
        queue.Enqueue(new BFSNode(y, x, null));
        checkRoad[y, x] = true;
        while (queue.Count > 0)
        {

            BFSNode node = queue.Dequeue();

            if (node.Y == targetY && node.X == targetX)
            {

                if (bestNode == null || (bestNode.PrevConut > node.PrevConut))
                {
                    bestNode = node;



                }
            }
            for (int i = 0; i < direction.GetLength(0); i++)
            {
                int dy = node.Y + direction[i, 0];
                int dx = node.X + direction[i, 1];
                if (CheckMapRange(dy, dx) && dupleMap[dy, dx] && !checkRoad[dy, dx])
                {

                    BFSNode searchNode = new BFSNode(dy, dx, node);
                    queue.Enqueue(searchNode);

                    checkRoad[dy, dx] = true;

                }





            }
            for (int i = 0; i < direction.GetLength(0); i++)
            {
                int dy = node.Y + direction[i, 0];
                int dx = node.X + direction[i, 1];
                if (CheckMapRange(dy, dx) && CheckMapDuple(dy, dx) && !checkRoad[dy, dx])
                {

                    BFSNode searchNode = new BFSNode(dy, dx, node);
                    queue.Enqueue(searchNode);

                    checkRoad[dy, dx] = true;

                }





            }



        }
        while (bestNode.PrevConut > 0)
        {


            if (Map[bestNode.Y, bestNode.X] == 3)
            {


                // Legacy:
                dupleMap[bestNode.Y, bestNode.X] = true;
            }
            else if (Map[bestNode.Y, bestNode.X] == 1)
            {
                dupleMap[bestNode.Y, bestNode.X] = true;
            }
            else
            {

                Map[bestNode.Y, bestNode.X] = 4;
                dupleMap[bestNode.Y, bestNode.X] = true;
            }





            bestNode = bestNode.PrevNode;
        }




    }
    private bool CheckMapRange(int y, int x)
    {
        return (y >= 0 && y < bfsMap.GetLength(0) && (x >= 0 && x < bfsMap.GetLength(1)));
    }

    private bool CheckMapWay(int y, int x)
    {
        return bfsMap[y, x] == 0;
    }
    private bool CheckMapDuple(int y, int x)
    {

        return CheckMap[y, x] == 0;
    }

    private void ClearCheckRoad()
    {
        checkRoad = new bool[bfsMap.GetLength(0), bfsMap.GetLength(1)];
        for (int i = 0; i < checkRoad.GetLength(0); i++)
        {
            for (int j = 0; j < checkRoad.GetLength(1); j++)
            {
                checkRoad[i, j] = false;
            }
        }
    }
    public class BFSNode
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public BFSNode PrevNode { get; private set; }

        public int PrevConut { get; private set; }
        public BFSNode()
        {
            Y = 0;
            X = 0;
            PrevNode = null;

            PrevConut = 0;


        }
        public BFSNode(int y, int x, BFSNode prevNode)
        {
            Y = y;
            X = x;
            PrevNode = prevNode;

            if (prevNode == null)
            {
                PrevConut = 0;

            }
            else
            {
                PrevConut = PrevNode.PrevConut + 1;
            }
        }


    }
    public void MakeMap()
    {
        MapX = GameManager.instance.stageNum + 6;
        MapY = GameManager.instance.stageNum + 6;
        SpecialRoom = new Stack<int>();
        SpecialRoom.Push(2);
        SpecialRoom.Push(5);
        SpecialRoom.Push(4);
        EndRoomX = new List<int>();
        EndRoomY = new List<int>();
        dupleMap = new bool[MapY, MapX];
        // ******************************************************
        //                  BestWay = down to up
        // ******************************************************
        bestWay = new List<int>();
        direction = new int[,] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };
        bfsMap = new int[MapY, MapX];
        Map = new int[MapY, MapX];
        CheckMap = new int[MapY, MapX];
        Map[MapY / 2, MapX / 2] = 1;
        randomX = 0;
        randomY = Random.Range(1, MapY - 1);
        Map[randomY, randomX] = 3;

        randomX = Random.Range(1, MapX - 1);
        randomY = 0;
        Map[randomY, randomX] = 3;

        randomX = MapY - 1;
        randomY = Random.Range(1, MapY - 1);
        Map[randomY, randomX] = 3;



        randomX = Random.Range(1, MapY - 1);
        randomY = MapY - 1;
        Map[randomY, randomX] = 3;
        for (int j = 0; j < Map.GetLength(0); j++)
        {
            for (int k = 0; k < Map.GetLength(1); k++)
            {

                CheckMap[k, j] = 1;

            }
        }
        CheckMap[MapX / 2, MapY / 2] = 0;
        for (int i = 0; i < MapX; i++)
        {
            for (int j = 0; j < MapY; j++)
            {
                if (Map[j, i] == 3)
                {

                    EndRoomX.Add(i);
                    EndRoomY.Add(j);
                }

            }
        }
        for (int i = 0; i < 4; i++)
        {

            StartBFS(MapY / 2, MapX / 2, EndRoomY[i], EndRoomX[i]);


        }
        for (int i = 0; i < 4; i++)
        {
            CheckBFS(EndRoomY[i], EndRoomX[i], MapY / 2, MapX / 2);
            // CheckBFS(MapY / 2, MapX / 2, EndRoomY[i], EndRoomX[i]);


        }



        for (int i = 0; i < 3; i++)
        {
            if (bestWaySort < bestWay[i])
            {
                bestWaySort = bestWay[i];
                bestWaynumber = i;
            }
        }

        switch (bestWaynumber)
        {
            case 0:
                for (int i = 0; i < 4; i++)
                {
                    if (i == 0)
                    {
                    }
                    else
                    {
                        Map[EndRoomY[i], EndRoomX[i]] = SpecialRoom.Pop();
                    }
                }
                break;
            case 1:
                for (int i = 0; i < 4; i++)
                {
                    if (i == 1)
                    {
                    }
                    else
                    {
                        Map[EndRoomY[i], EndRoomX[i]] = SpecialRoom.Pop();
                    }
                }
                break;
            case 2:
                for (int i = 0; i < 4; i++)
                {
                    if (i == 2)
                    {
                    }
                    else
                    {
                        Map[EndRoomY[i], EndRoomX[i]] = SpecialRoom.Pop();
                    }
                }
                break;

            case 3:
                for (int i = 0; i < 4; i++)
                {
                    if (i == 3)
                    {
                    }
                    else
                    {
                        Map[EndRoomY[i], EndRoomX[i]] = SpecialRoom.Pop();
                    }
                }
                break;
        }
        GameManager.instance.map = Map;
        // for Map Create Test
        /*
        for (int i = 0; i < MapX; i++)
        {
            for (int j = 0; j < MapY; j++)
            {
                switch (Map[j, i])
                {
                    case 0:
                        Image makeImg = Instantiate(rooms[0]);
                        makeImg.transform.SetParent(canvas.transform, false);
                        imgrect = makeImg.GetComponent<RectTransform>();
                        imgrect.transform.SetPositionAndRotation(new Vector2(j * 10, i * 10), new Quaternion(0, 0, 0, 0));
                        break;
                    case 1:
                        makeImg = Instantiate(rooms[1]);
                        makeImg.transform.SetParent(canvas.transform, false);
                        imgrect = makeImg.GetComponent<RectTransform>();
                        imgrect.transform.SetPositionAndRotation(new Vector2(j * 10, i * 10), new Quaternion(0, 0, 0, 0));
                        break;
                    case 2:
                        makeImg = Instantiate(rooms[2]);
                        makeImg.transform.SetParent(canvas.transform, false);
                        imgrect = makeImg.GetComponent<RectTransform>();
                        imgrect.transform.SetPositionAndRotation(new Vector2(j * 10, i * 10), new Quaternion(0, 0, 0, 0));
                        break;
                    case 3:
                        makeImg = Instantiate(rooms[3]);
                        makeImg.transform.SetParent(canvas.transform, false);
                        imgrect = makeImg.GetComponent<RectTransform>();
                        imgrect.transform.SetPositionAndRotation(new Vector2(j * 10, i * 10), new Quaternion(0, 0, 0, 0));
                        break;
                    case 4:
                        makeImg = Instantiate(rooms[4]);
                        makeImg.transform.SetParent(canvas.transform, false);
                        imgrect = makeImg.GetComponent<RectTransform>();
                        imgrect.transform.SetPositionAndRotation(new Vector2(j * 10, i * 10), new Quaternion(0, 0, 0, 0));
                        break;
                    case 5:
                        makeImg = Instantiate(rooms[5]);
                        makeImg.transform.SetParent(canvas.transform, false);
                        imgrect = makeImg.GetComponent<RectTransform>();
                        imgrect.transform.SetPositionAndRotation(new Vector2(j * 10, i * 10), new Quaternion(0, 0, 0, 0));
                        break;
                    case 6:
                        makeImg = Instantiate(rooms[6]);
                        makeImg.transform.SetParent(canvas.transform, false);
                        imgrect = makeImg.GetComponent<RectTransform>();
                        imgrect.transform.SetPositionAndRotation(new Vector2(j * 10, i * 10), new Quaternion(0, 0, 0, 0));
                        break;

                }
            }
        }
        for (int i = 0; i < MapX; i++)
        {
            for (int j = 0; j < MapY; j++)
            {
                switch (CheckMap[j, i])
                {
                    case 0:
                        Image makeImg = Instantiate(rooms[0]);
                        makeImg.transform.SetParent(canvas.transform, false);
                        imgrect = makeImg.GetComponent<RectTransform>();
                        imgrect.transform.SetPositionAndRotation(new Vector2(j * 10 + 10 * MapX, i * 10), new Quaternion(0, 0, 0, 0));
                        break;
                    case 1:
                        makeImg = Instantiate(rooms[1]);
                        makeImg.transform.SetParent(canvas.transform, false);
                        imgrect = makeImg.GetComponent<RectTransform>();
                        imgrect.transform.SetPositionAndRotation(new Vector2(j * 10 + 10 * MapX, i * 10), new Quaternion(0, 0, 0, 0));
                        break;
                    case 2:
                        makeImg = Instantiate(rooms[2]);
                        makeImg.transform.SetParent(canvas.transform, false);
                        imgrect = makeImg.GetComponent<RectTransform>();
                        imgrect.transform.SetPositionAndRotation(new Vector2(j * 10 + 10 * MapX, i * 10), new Quaternion(0, 0, 0, 0));
                        break;
                    case 3:
                        makeImg = Instantiate(rooms[3]);
                        makeImg.transform.SetParent(canvas.transform, false);
                        imgrect = makeImg.GetComponent<RectTransform>();
                        imgrect.transform.SetPositionAndRotation(new Vector2(j * 10 + 10 * MapX, i * 10), new Quaternion(0, 0, 0, 0));
                        break;
                    case 4:
                        makeImg = Instantiate(rooms[4]);
                        makeImg.transform.SetParent(canvas.transform, false);
                        imgrect = makeImg.GetComponent<RectTransform>();
                        imgrect.transform.SetPositionAndRotation(new Vector2(j * 10 + 10 * MapX, i * 10), new Quaternion(0, 0, 0, 0));
                        break;
                    case 5:
                        makeImg = Instantiate(rooms[5]);
                        makeImg.transform.SetParent(canvas.transform, false);
                        imgrect = makeImg.GetComponent<RectTransform>();
                        imgrect.transform.SetPositionAndRotation(new Vector2(j * 10 + 100, i * 10), new Quaternion(0, 0, 0, 0));
                        break;
                    case 6:
                        makeImg = Instantiate(rooms[6]);
                        makeImg.transform.SetParent(canvas.transform, false);
                        imgrect = makeImg.GetComponent<RectTransform>();
                        imgrect.transform.SetPositionAndRotation(new Vector2(j * 10 + 100, i * 10), new Quaternion(0, 0, 0, 0));
                        break;

                }
            }
        }
        */

    }

}