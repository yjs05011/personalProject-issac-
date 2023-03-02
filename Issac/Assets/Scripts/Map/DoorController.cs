using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoorController : MonoBehaviour
{

    public int roomType;
    public int roomNum;
    private RectTransform roomRect;
    private Rigidbody2D roomRigi;
    private SpriteRenderer[] Doors;
    //Sprite [monster, item, boss, moon, shop]
    public Sprite[] doorShapes;
    private int monsterNumber;
    public BoxCollider2D[] DoorExit;
    public int LeftDoorX;
    public int RightDoorX;
    public int UpDoorX;
    public int DownDoorX;
    public int LeftDoorY;
    public int RightDoorY;
    public int UpDoorY;
    public int DownDoorY;
    public GameObject[] Monster;

    public GameObject[,] map;
    public Collider2D roomChangeCollider;
    public Vector2 RoomPosition;
    public Vector2 direction;
    public float speed;
    public int DoorX;
    public int DoorY;
    public bool RoomChange;


    // Start is called before the first frame update
    // Door is Left, up, Right, bottom
    void OnEnable()
    {
        GameManager.instance.monsterClearChk = false;
    }
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


        if (GameManager.instance.monsterCount != 0 || GameManager.instance.bossHp != 0)
        {
            GameManager.instance.monsterClearChk = false;

        }
        else
        {
            GameManager.instance.monsterClearChk = true;
            OpenDoor();
        }
        if (RoomChange)
        {

            GameObject NextRoom = GameManager.instance.nowMapStat[DoorX, DoorY];
            GameManager.instance.NowMap = NextRoom;


            NextRoom.transform.Translate(direction * Time.deltaTime * speed);
            transform.Translate(direction * Time.deltaTime * speed);

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

                        UpDoorX = j;
                        UpDoorY = i + 1;
                        Doors[1].gameObject.SetActive(true);
                        switch (map[j, i + 1].GetComponent<DoorController>().roomType)
                        {
                            case 2:
                                Doors[1].sprite = doorShapes[1];
                                break;
                            case 3:
                                Doors[1].sprite = doorShapes[2];
                                break;
                            case 5:
                                Doors[1].sprite = doorShapes[3];
                                break;
                            case 6:
                                Doors[1].sprite = doorShapes[4];
                                break;
                            default:
                                break;
                        }
                    }
                    if (i - 1 >= 0 && map[j, i - 1] != null)
                    {

                        DownDoorX = j;
                        DownDoorY = i - 1;
                        Doors[3].gameObject.SetActive(true);
                        switch (map[j, i - 1].GetComponent<DoorController>().roomType)
                        {
                            case 2:
                                Doors[3].sprite = doorShapes[1];
                                break;
                            case 3:
                                Doors[3].sprite = doorShapes[2];
                                break;
                            case 5:
                                Doors[3].sprite = doorShapes[3];
                                break;
                            case 6:
                                Doors[3].sprite = doorShapes[4];
                                break;
                            default:
                                break;
                        }
                    }
                    if (j + 1 < map.GetLength(1) && map[j + 1, i] != null)
                    {

                        RightDoorX = j + 1;
                        RightDoorY = i;
                        Doors[2].gameObject.SetActive(true);
                        switch (map[j + 1, i].GetComponent<DoorController>().roomType)
                        {
                            case 2:
                                Doors[2].sprite = doorShapes[1];
                                break;
                            case 3:
                                Doors[2].sprite = doorShapes[2];
                                break;
                            case 5:
                                Doors[2].sprite = doorShapes[3];
                                break;
                            case 6:
                                Doors[2].sprite = doorShapes[4];
                                break;
                            default:
                                break;
                        }
                    }
                    if (j - 1 >= 0 && map[j - 1, i] != null)
                    {

                        LeftDoorX = j - 1;
                        LeftDoorY = i;
                        Doors[0].gameObject.SetActive(true);
                        switch (map[j - 1, i].GetComponent<DoorController>().roomType)
                        {
                            case 2:
                                Doors[0].sprite = doorShapes[1];
                                break;
                            case 3:
                                Doors[0].sprite = doorShapes[2];
                                break;
                            case 5:
                                Doors[0].sprite = doorShapes[3];
                                break;
                            case 6:
                                Doors[0].sprite = doorShapes[4];
                                break;
                            default:
                                break;
                        }
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
                    DoorExit[i + 4].isTrigger = true;
                }

            }
        }
    }

    // IEnumerator RoomChangeRoutine(Collider2D other, Vector2 RoomPosition, Vector2 Direction, float speed, int DoorX, int DoorY)
    // {

    //     DoorController nowRoom = other.transform.parent.parent.parent.gameObject.GetComponent<DoorController>();
    //     GameObject NextRoom = GameManager.instance.nowMapStat[DoorX, DoorY];

    //     Vector2 nowRoomPosition = nowRoom.transform.localPosition;
    //     NextRoom.SetActive(true);
    //     NextRoom.transform.localPosition = RoomPosition;
    //     NextRoom.transform.Translate(Direction * Time.deltaTime * speed);
    //     nowRoom.transform.Translate(Direction * Time.deltaTime * speed);

    //     yield return new WaitForSeconds(0.5f);




    // }

}
