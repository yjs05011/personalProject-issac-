using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public int roomType;
    public int roomNum;
    public RectTransform roomRect;
    public Rigidbody2D roomRigi;
    private SpriteRenderer[] Doors;
    public int monsterNumber;
    public BoxCollider2D[] DoorExit;
    public int LeftDoorX;
    public int RightDoorX;
    public int UpDoorX;
    public int DownDoorX;
    public int LeftDoorY;
    public int RightDoorY;
    public int UpDoorY;
    public int DownDoorY;

    public GameObject[,] map;

    // Start is called before the first frame update

    void Start()
    {
        map = GameManager.instance.nowMapStat;

        Doors = new SpriteRenderer[4];
        roomRect = gameObject.GetComponent<RectTransform>();
        roomRigi = gameObject.GetComponent<Rigidbody2D>();
        Doors[0] = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Doors[1] = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Doors[2] = transform.GetChild(2).GetComponent<SpriteRenderer>();
        Doors[3] = transform.GetChild(3).GetComponent<SpriteRenderer>();
        DoorChk(map, roomNum);


    }

    // Update is called once per frame
    void Update()
    {
        GameManager.instance.monsterClearChk = false;
        if (monsterNumber == 0)
        {
            GameManager.instance.monsterClearChk = true;
            OpenDoor();
        }
        /*  if(GameManager.Instance.ClearChk){

          }*/
    }
    void DoorChk(GameObject[,] map, int roomNum)
    {
        for (int i = 0; i < Doors.Length; i++)
        {
            Doors[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {


                if (map[j, i] != null && map[j, i].GetComponent<DoorController>().roomNum == roomNum)
                {


                    if (i + 1 < map.GetLength(0) && map[j, i + 1] != null)
                    {
                        Debug.Log($"map [{j} , {i + 1}] =  {map[j, i + 1].GetComponent<DoorController>().roomNum}");
                        UpDoorX = j;
                        UpDoorY = i + 1;

                        Doors[1].gameObject.SetActive(true);
                    }
                    if (i - 1 >= 0 && map[j, i - 1] != null)
                    {
                        Debug.Log($"map [{j} , {i - 1}] =  {map[j, i - 1].GetComponent<DoorController>().roomNum}");
                        DownDoorX = j;
                        DownDoorY = i - 1;
                        Doors[3].gameObject.SetActive(true);
                    }
                    if (j + 1 < map.GetLength(1) && map[j + 1, i] != null)
                    {
                        Debug.Log($"map [{j + 1} , {i}] =  {map[j + 1, i].GetComponent<DoorController>().roomNum}");
                        RightDoorX = j + 1;
                        RightDoorY = i;
                        Doors[0].gameObject.SetActive(true);
                    }
                    if (j - 1 >= 0 && map[j - 1, i] != null)
                    {
                        Debug.Log($"map [{j - 1} , {i}] =  {map[j - 1, i].GetComponent<DoorController>().roomNum}");
                        LeftDoorX = j - 1;
                        LeftDoorY = i;
                        Doors[2].gameObject.SetActive(true);
                    }

                }
            }
        }

    }


    void OpenDoor()
    {
        if (GameManager.instance.monsterClearChk)
        {

            for (int i = 0; i < Doors.Length; i++)
            {
                if (Doors[i].gameObject.activeSelf)
                {
                    Doors[i].transform.GetChild(0).gameObject.SetActive(true);
                    Doors[i].transform.GetChild(1).gameObject.SetActive(false);
                    Doors[i].transform.GetChild(2).gameObject.SetActive(false);
                    DoorExit[i].isTrigger = true;
                }

            }
        }
    }


}
