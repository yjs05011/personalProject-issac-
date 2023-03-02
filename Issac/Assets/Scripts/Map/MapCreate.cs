using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreate : MonoBehaviour
{
    private int roomCount;
    public GameObject[] rooms;
    private List<GameObject> MapList;
    private int[,] map;
    public GameObject[,] nowMapStatus;
    public MapCreate instance = null;
    private int makeRoomNumber = 0;
    public GameObject newRoom;
    private DoorController door;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {

        nowMapStatus = new GameObject[GameManager.instance.map.GetLength(0), GameManager.instance.map.GetLength(1)];
        map = GameManager.instance.map;
        MapList = new List<GameObject>();

        CreateMap();
        GameManager.instance.roomChange = true;
        StartCoroutine(DelayTime());


    }


    // Update is called once per frame
    void Update()
    {

    }
    void CreateMap()
    {


        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[j, i] == 0) { }
                else
                {
                    makeRoomNumber++;
                    switch (map[j, i])
                    {
                        case 1:

                            newRoom = Instantiate(rooms[0]);
                            newRoom.name = "StartRoom";
                            newRoom.SetActive(true);
                            newRoom.transform.SetParent(transform, false);
                            newRoom.transform.SetPositionAndRotation(new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
                            newRoom.GetComponent<DoorController>().roomType = 1;
                            newRoom.GetComponent<DoorController>().roomNum = makeRoomNumber;
                            nowMapStatus[j, i] = newRoom;
                            GameManager.instance.NowMap = newRoom;
                            MapList.Add(newRoom);
                            break;
                        case 2:
                            newRoom = Instantiate(rooms[1]);
                            newRoom.name = "ItemRoom";
                            newRoom.SetActive(false);
                            newRoom.transform.SetParent(transform, false);
                            newRoom.GetComponent<DoorController>().roomType = 2;
                            newRoom.GetComponent<DoorController>().roomNum = makeRoomNumber;
                            nowMapStatus[j, i] = newRoom;
                            MapList.Add(newRoom);
                            break;
                        case 3:
                            newRoom = Instantiate(rooms[2]);
                            newRoom.name = "BossRoom";
                            newRoom.SetActive(false);
                            newRoom.transform.SetParent(transform, false);
                            newRoom.GetComponent<DoorController>().roomType = 3;
                            newRoom.GetComponent<DoorController>().roomNum = makeRoomNumber;
                            nowMapStatus[j, i] = newRoom;
                            MapList.Add(newRoom);
                            break;
                        case 4:
                            newRoom = Instantiate(rooms[Random.Range(3, 8)]);
                            newRoom.name = "MosterRoom";
                            newRoom.SetActive(false);
                            newRoom.transform.SetParent(transform, false);
                            newRoom.GetComponent<DoorController>().roomType = 4;
                            newRoom.GetComponent<DoorController>().roomNum = makeRoomNumber;
                            nowMapStatus[j, i] = newRoom;
                            MapList.Add(newRoom);
                            break;
                        case 5:
                            newRoom = Instantiate(rooms[8]);
                            newRoom.name = "MoonRoom";
                            newRoom.SetActive(false);
                            newRoom.transform.SetParent(transform, false);
                            newRoom.GetComponent<DoorController>().roomType = 5;
                            newRoom.GetComponent<DoorController>().roomNum = makeRoomNumber;
                            MapList.Add(newRoom);
                            break;


                    }
                }
            }
        }

        GameManager.instance.nowMapStat = nowMapStatus;

    }
    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.roomChange = false;

    }
}
