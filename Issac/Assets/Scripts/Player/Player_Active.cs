using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Active : MonoBehaviour
{

    public RuntimeAnimatorController[] character;
    public Sprite[] PlayerActSprite;
    public AudioClip[] clip;
    public Charator[] stat;
    // Start is called before the first frame update
    public Rigidbody2D playerRigid;
    private BoxCollider2D playerBox;
    public PointEffector2D playerPoint;
    public float maxVelocityX = 5;
    public float maxVelocityY = 5;
    private Tears tears;
    public RectTransform playerHead;
    public float speed;
    private bool keyDown;
    private bool isAttack = false;
    private bool[] isAttackKey;
    private Animator aniHead;
    private Animator aniBody;
    public int[,] miniMap = new int[7, 7];
    private bool roomChangeChk;
    private bool isBoom;
    private bool isDie;
    public bool isHit;
    public bool isHitChk;
    public SpriteRenderer playerAct;
    public Canvas roomChanger;
    public bool isSceneChange;
    public float startTime;
    public Canvas BossLoad;
    public void Awake()
    {
        playerAct = transform.GetChild(1).GetComponent<SpriteRenderer>();
        playerBox = gameObject.GetBoxCollider();
        isAttackKey = new bool[4];
        playerRigid = gameObject.GetComponent<Rigidbody2D>();
        playerHead = transform.GetChild(2).GetComponent<RectTransform>();
        aniHead = transform.GetChild(3).GetComponent<Animator>();
        aniBody = transform.GetChild(4).GetComponent<Animator>();
        playerPoint = GetComponent<PointEffector2D>();
        aniHead.runtimeAnimatorController = character[GameManager.instance.player_Stat.ID - 1];
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
        transform.GetChild(4).gameObject.SetActive(true);
    }
    public void Start()
    {

    }

    void Update()
    {
        if (!GameManager.instance.MainUiActive)
        {
            if (!isSceneChange)
            {
                if (!isDie)
                {
                    move();
                    if (transform.GetChild(0).gameObject.activeSelf)
                    {

                    }
                    else
                    {
                        Shooting();
                    }
                    DropBoom();
                    if (isHitChk)
                    {
                        isHitChk = false;
                        StartCoroutine("HitDelay");
                    }
                    Restart();
                }

                if (GameManager.instance.player_Stat.NormalHeart <= 0 && GameManager.instance.player_Stat.SoulHeart <= 0)
                {
                    GameManager.instance.player_Stat.Die = true;

                    if (!isDie)
                    {
                        isDie = true;
                        StartCoroutine(DieAnime());
                    }

                }

            }
        }

    }

    public void DropBoom()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (GameManager.instance.player_Stat.BoomCount > 0)
            {
                if (!isBoom)
                {
                    isBoom = true;
                    StartCoroutine(DropBoomDelay());

                }
                if (GameManager.instance.roomChange)
                {
                    isBoom = false;
                }
            }


        }
    }
    public void Restart()
    {
        if (Input.GetKey(KeyCode.R))
        {
            float restartTime = Time.time - startTime;
            if (restartTime >= 3f)
            {

                ResetTime();
                GFunc.PlayerStatReset(GameManager.instance.player_Stat.ID - 1);
                GFunc.SceneChanger("Stage1");
            }
        }
    }
    public void ResetTime()
    {
        startTime = Time.time;
    }
    public void move()
    {


        if (Input.GetKey(KeyCode.W))
        {
            aniBody.SetBool("FrontMove", true);
            playerRigid.AddForce(new Vector2(0, 4f));
            LimitSpeed();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {

            playerRigid.velocity = new Vector2(0, 0);
            playerRigid.AddForce(new Vector2(0, 5f));
            keyDown = false;
            aniBody.SetBool("FrontMove", false);

        }
        if (Input.GetKey(KeyCode.S))
        {
            aniBody.SetBool("FrontMove", true);
            playerRigid.AddForce(new Vector2(0, -4f));
            LimitSpeed();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {

            playerRigid.velocity = new Vector2(0, 0);
            playerRigid.AddForce(new Vector2(0, -5f));
            keyDown = false;
            aniBody.SetBool("FrontMove", false);

        }
        if (Input.GetKey(KeyCode.A))
        {
            aniBody.SetBool("SideMove", true);
            playerRigid.AddForce(new Vector2(-4f, 0));
            LimitSpeed();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {

            playerRigid.velocity = new Vector2(0, 0);
            playerRigid.AddForce(new Vector2(-5f, 0));
            keyDown = false;
            aniBody.SetBool("SideMove", false);

        }

        if (Input.GetKey(KeyCode.D))
        {
            aniBody.SetBool("SideMove", true);
            playerRigid.AddForce(new Vector2(4f, 0));
            LimitSpeed();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            playerRigid.velocity = new Vector2(0, 0);
            playerRigid.AddForce(new Vector2(5f, 0));
            keyDown = false;
            aniBody.SetBool("SideMove", false);

        }
        if (Input.anyKey)
        {
            keyDown = true;
        }
        if (!keyDown)
        {
            StartCoroutine(Moving(new Vector2(0, 0)));
        }


    }
    void LimitSpeed()
    {
        if (playerRigid.velocity.x > maxVelocityX)
        {
            playerRigid.velocity = new Vector2(maxVelocityX + GameManager.instance.player_Stat.Speed, playerRigid.velocity.y);
        }
        if (playerRigid.velocity.x < (maxVelocityX * -1))
        {
            playerRigid.velocity = new Vector2(maxVelocityX * -1 - GameManager.instance.player_Stat.Speed, playerRigid.velocity.y);
        }
        if (playerRigid.velocity.y > maxVelocityY)
        {
            playerRigid.velocity = new Vector2(playerRigid.velocity.x, maxVelocityY + GameManager.instance.player_Stat.Speed);
        }
        if (playerRigid.velocity.y < (maxVelocityY * -1))
        {
            playerRigid.velocity = new Vector2(playerRigid.velocity.x, (maxVelocityY * -1) - GameManager.instance.player_Stat.Speed);
        }
    }

    public void Attack()
    {
        tears = Pooling.instance.playerTears.Pop().GetComponent<Tears>();
        tears.tearSize.rotation = playerHead.rotation;
        // Debug.Log($"2번 플로우 눈물이 팝된다.{tears.tearSize.rotation.eulerAngles},  {playerHead.rotation.eulerAngles}");
        if (isAttackKey[0])
        {

            tears.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.3f, gameObject.transform.position.z);
            tears.tearImgSize.position =
            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f + GameManager.instance.player_Stat.Range * 0.05f, gameObject.transform.position.z);
            // Debug.Log($"Right :{tears.transform.GetChild(0).gameObject.transform.position}");
            // Debug.Log($"shadow :{tears.transform.position.y}");
            // Debug.Log($"Tears : {tears.transform.position.y}");
            // Debug.Log($"shadow rotation :{tears.tearSize.rotation.eulerAngles}");
            // Debug.Log($"Tears rotation : {tears.tearImgSize.rotation.eulerAngles}");
        }
        if (isAttackKey[1])
        {

            tears.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.3f, gameObject.transform.position.z);
            tears.transform.GetChild(0).gameObject.transform.position =
            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f + GameManager.instance.player_Stat.Range * 0.05f, gameObject.transform.position.z);
            // Debug.Log($"left :{tears.transform.GetChild(0).gameObject.transform.position}");
        }
        if (isAttackKey[2])
        {

            tears.transform.position = gameObject.transform.position;
            tears.transform.GetChild(0).gameObject.transform.position = new Vector3(gameObject.transform.position.x + +GameManager.instance.player_Stat.Range * 0.05f, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        if (isAttackKey[3])
        {

            tears.transform.position = gameObject.transform.position;
            tears.transform.GetChild(0).gameObject.transform.position = new Vector3(gameObject.transform.position.x + GameManager.instance.player_Stat.Range * 0.05f, gameObject.transform.position.y, gameObject.transform.position.z);
        }

        tears.gameObject.SetActive(true);
    }
    public void Shooting()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {

            aniHead.SetBool("RightAttack", false);
            isAttackKey[0] = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {

            aniHead.SetBool("LeftAttack", false);
            isAttackKey[1] = false;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {

            aniHead.SetBool("BackAttack", false);
            isAttackKey[2] = false;

        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {

            aniHead.SetBool("frontAttack", false);
            isAttackKey[3] = false;

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isAttackKey[0] = true;
            // Debug.Log("Shot");
            aniHead.SetBool("RightAttack", true);
            playerHead.SetRotation(0, 0, 270f);
            // Debug.Log($"1번 플로우 해드가 돌아간다.{playerHead.rotation.eulerAngles}");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isAttackKey[1] = true;
            aniHead.SetBool("LeftAttack", true);
            playerHead.SetRotation(0, 0, 90f);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isAttackKey[2] = true;
            aniHead.SetBool("BackAttack", true);
            playerHead.SetRotation(0, 0, 0);

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isAttackKey[3] = true;
            aniHead.SetBool("frontAttack", true);
            playerHead.SetRotation(0, 0, 180f);

        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
        {



            if (!isAttack)
            {


                isAttack = true;
                StartCoroutine(Attacking(tears));
            }

        }


    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!GameManager.instance.player_Stat.Die)
        {
            if (other.transform.tag == "Monster")
            {
                if (isHit == true)
                {

                }
                else
                {
                    if (other.transform.parent.GetComponent<Monster_Active>().monsterType == 1)
                    {
                        isHitChk = true;
                        isHit = true;
                        if (GameManager.instance.player_Stat.SoulHeart >= 2)
                        {
                            GameManager.instance.player_Stat.SoulHeart -= 2;
                        }
                        else if (GameManager.instance.player_Stat.SoulHeart == 1)
                        {
                            GameManager.instance.player_Stat.SoulHeart -= 1;
                        }
                        else
                        {
                            GameManager.instance.player_Stat.NormalHeart -= 2;
                        }
                    }
                    else
                    {
                        isHitChk = true;
                        isHit = true;
                        if (GameManager.instance.player_Stat.SoulHeart >= 1)
                        {
                            GameManager.instance.player_Stat.SoulHeart -= 1;
                        }
                        else
                        {
                            GameManager.instance.player_Stat.NormalHeart -= 1;
                        }
                    }
                    StartCoroutine(GFunc.PlayerHit(playerBox, 1, "", clip[0]));
                }
            }
            if (other.transform.tag == "Thron")
            {
                if (isHit == true)
                {

                }
                else
                {
                    isHitChk = true;
                    isHit = true;
                    if (GameManager.instance.player_Stat.SoulHeart >= 1)
                    {
                        GameManager.instance.player_Stat.SoulHeart -= 1;
                    }
                    else
                    {
                        GameManager.instance.player_Stat.NormalHeart -= 1;
                    }
                }
                StartCoroutine(GFunc.PlayerHit(playerBox, 1, "", clip[0]));

            }
            if (other.transform.tag == "Boss")
            {
                if (isHit == true)
                {

                }
                else
                {

                    isHitChk = true;
                    isHit = true;
                    if (GameManager.instance.player_Stat.SoulHeart >= 2)
                    {
                        GameManager.instance.player_Stat.SoulHeart -= 2;
                    }
                    else if (GameManager.instance.player_Stat.SoulHeart == 1)
                    {
                        GameManager.instance.player_Stat.SoulHeart -= 1;
                    }
                    else
                    {
                        GameManager.instance.player_Stat.NormalHeart -= 2;
                    }


                    StartCoroutine(GFunc.PlayerHit(playerBox, 1, "", clip[0]));

                }
            }
        }



    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        switch (other.tag)
        {

            case "LeftDoor":
                if (!roomChangeChk)
                {
                    DoorController inspector = other.transform.parent.parent.parent.gameObject.GetComponent<DoorController>();
                    roomChangeChk = true;
                    transform.position = new Vector2(-other.transform.position.x - 2, other.transform.position.y);
                    if (GameManager.instance.nowMapStat[inspector.LeftDoorX, inspector.LeftDoorY].GetComponent<DoorController>().roomType == 3)
                    {
                        // GameObject NextRoom = GameManager.instance.nowMapStat[inspector.LeftDoorX, inspector.LeftDoorY];
                        // GameManager.instance.NowMap = NextRoom;
                        // NextRoom.transform.position = new Vector2(0, 0);
                        // NextRoom.SetActive(true);

                        // inspector.gameObject.SetActive(false);
                        // inspector.RoomChange = false;
                        // roomChangeChk = false;
                        // playerBox.isTrigger = false;
                        // GameManager.instance.roomChange = false;
                        if (GameManager.instance.bossEnter)
                        {
                            StartCoroutine(BossRoomChange(other, new Vector2(-216.5f, -100), Vector2.right, 80, inspector.LeftDoorX, inspector.LeftDoorY));
                        }
                        StartCoroutine(BossRoomChange(other, new Vector2(-216.5f, -100), Vector2.right, 80, inspector.LeftDoorX, inspector.LeftDoorY));
                    }
                    else
                    {
                        // roomChanger.gameObject.SetActive(true);
                        // roomChanger.GetComponent<LoadingUi>().BlackFadeInAndOut();

                        StartCoroutine(RoomChange(other, new Vector2(-216.5f, -100), Vector2.right, 80, inspector.LeftDoorX, inspector.LeftDoorY));
                    }
                    // StartCoroutine(RoomChange(other, new Vector2(-216.5f, -100), Vector2.right, 80, inspector.LeftDoorX, inspector.LeftDoorY));
                    GameManager.instance.roomChange = true;
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(4).gameObject.SetActive(true);
                    transform.GetChild(3).gameObject.SetActive(true);

                }

                break;
            case "RightDoor":
                if (!roomChangeChk)
                {
                    DoorController inspector = other.transform.parent.parent.parent.gameObject.GetComponent<DoorController>();
                    roomChangeChk = true;
                    transform.position = new Vector2(-other.transform.position.x + 2, other.transform.position.y);
                    if (GameManager.instance.nowMapStat[inspector.RightDoorX, inspector.RightDoorY].GetComponent<DoorController>().roomType == 3)
                    {
                        // GameObject NextRoom = GameManager.instance.nowMapStat[inspector.RightDoorX, inspector.RightDoorY];
                        // GameManager.instance.NowMap = NextRoom;
                        // NextRoom.transform.position = new Vector2(0, 0);
                        // NextRoom.SetActive(true);
                        // inspector.gameObject.SetActive(false);
                        // inspector.RoomChange = false;
                        // roomChangeChk = false;
                        // playerBox.isTrigger = false;
                        // GameManager.instance.roomChange = false;
                        if (GameManager.instance.bossEnter)
                        {
                            StartCoroutine(RoomChange(other, new Vector2(-183.5f, -100), Vector2.left, 80, inspector.RightDoorX, inspector.RightDoorY));
                        }
                        StartCoroutine(BossRoomChange(other, new Vector2(-183.5f, -100), Vector2.left, 80, inspector.RightDoorX, inspector.RightDoorY));
                    }
                    else
                    {
                        // roomChanger.gameObject.SetActive(true);
                        // roomChanger.GetComponent<LoadingUi>().BlackFadeInAndOut();

                        StartCoroutine(RoomChange(other, new Vector2(-183.5f, -100), Vector2.left, 80, inspector.RightDoorX, inspector.RightDoorY));
                    }
                    //StartCoroutine(RoomChange(other, new Vector2(-183.5f, -100), Vector2.left, 80, inspector.RightDoorX, inspector.RightDoorY));
                    GameManager.instance.roomChange = true;
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(4).gameObject.SetActive(true);
                    transform.GetChild(3).gameObject.SetActive(true);
                }
                break;
            case "UpDoor":
                if (!roomChangeChk)
                {
                    DoorController inspector = other.transform.parent.parent.parent.gameObject.GetComponent<DoorController>();
                    roomChangeChk = true;
                    transform.position = new Vector2(other.transform.position.x, 1.5f - other.transform.position.y);
                    if (GameManager.instance.nowMapStat[inspector.UpDoorX, inspector.UpDoorY].GetComponent<DoorController>().roomType == 3)
                    {
                        // GameObject NextRoom = GameManager.instance.nowMapStat[inspector.UpDoorX, inspector.UpDoorY];
                        // GameManager.instance.NowMap = NextRoom;
                        // NextRoom.transform.position = new Vector2(0, 0);
                        // NextRoom.SetActive(true);
                        // inspector.gameObject.SetActive(false);
                        // inspector.RoomChange = false;
                        // roomChangeChk = false;
                        // playerBox.isTrigger = false;
                        // GameManager.instance.roomChange = false;
                        if (GameManager.instance.bossEnter)
                        {
                            StartCoroutine(RoomChange(other, new Vector2(-200, -91), Vector2.down, 40, inspector.UpDoorX, inspector.UpDoorY));
                        }
                        StartCoroutine(BossRoomChange(other, new Vector2(-200, -91), Vector2.down, 40, inspector.UpDoorX, inspector.UpDoorY));
                    }
                    else
                    {


                        StartCoroutine(RoomChange(other, new Vector2(-200, -91), Vector2.down, 40, inspector.UpDoorX, inspector.UpDoorY));
                    }
                    //StartCoroutine(RoomChange(other, new Vector2(-200, -91), Vector2.down, 40, inspector.UpDoorX, inspector.UpDoorY));
                    GameManager.instance.roomChange = true;
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(4).gameObject.SetActive(true);
                    transform.GetChild(3).gameObject.SetActive(true);
                }
                break;
            case "DownDoor":
                if (!roomChangeChk)
                {
                    DoorController inspector = other.transform.parent.parent.parent.gameObject.GetComponent<DoorController>();
                    roomChangeChk = true;
                    transform.position = new Vector2(other.transform.position.x, -other.transform.position.y - 1.5f);
                    if (GameManager.instance.nowMapStat[inspector.DownDoorX, inspector.DownDoorY].GetComponent<DoorController>().roomType == 3)
                    {
                        // GameObject NextRoom = GameManager.instance.nowMapStat[inspector.DownDoorX, inspector.DownDoorY];
                        // GameManager.instance.NowMap = NextRoom;
                        // NextRoom.transform.position = new Vector2(0, 0);
                        // NextRoom.SetActive(true);
                        // inspector.gameObject.SetActive(false);
                        // inspector.RoomChange = false;
                        // roomChangeChk = false;
                        // playerBox.isTrigger = false;
                        // GameManager.instance.roomChange = false;
                        if (GameManager.instance.bossEnter)
                        {
                            StartCoroutine(RoomChange(other, new Vector2(-200, -109), Vector2.up, 40, inspector.DownDoorX, inspector.DownDoorY));
                        }
                        StartCoroutine(BossRoomChange(other, new Vector2(-200, -109), Vector2.up, 40, inspector.DownDoorX, inspector.DownDoorY));
                    }
                    else
                    {

                        // roomChanger.gameObject.SetActive(true);
                        // roomChanger.GetComponent<LoadingUi>().BlackFadeInAndOut();
                        StartCoroutine(RoomChange(other, new Vector2(-200, -109), Vector2.up, 40, inspector.DownDoorX, inspector.DownDoorY));

                    }
                    // StartCoroutine(RoomChange(other, new Vector2(-200, -109), Vector2.up, 40, inspector.DownDoorX, inspector.DownDoorY));
                    GameManager.instance.roomChange = true;
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(4).gameObject.SetActive(true);
                    transform.GetChild(3).gameObject.SetActive(true);
                }
                break;
            case "Item":

                if (other.transform.GetChild(0).gameObject.activeSelf)
                {
                    ItemMaker item = other.transform.GetComponent<ItemMaker>();
                    item.GetItem();
                    item.transform.GetChild(0).gameObject.SetActive(false);

                    if (!item.player_Get_Item_Chk)
                    {
                        item.player_Get_Item_Chk = true;
                        GameManager.instance.itemgetChk = true;
                        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = item.itemImg[item.randomNum];
                        StartCoroutine(item.ItemAnima(gameObject));
                    }


                }
                break;
        }
    }
    IEnumerator Moving(Vector2 vector)
    {
        playerRigid.velocity = vector;
        yield return new WaitForSeconds(0.05f);
        playerRigid.velocity = Vector2.zero;
    }

    IEnumerator Attacking(Tears tears)
    {
        yield return null;
        Attack();
        // Debug.Log("Shoting");
        yield return new WaitForSeconds(0.5f - (GameManager.instance.player_Stat.RateSpeed / 20f));
        isAttack = false;

    }
    IEnumerator RoomChange(Collider2D other, Vector2 RoomPosition, Vector2 Direction, float speed, int DoorX, int DoorY)
    {
        playerBox.isTrigger = false;
        transform.tag = "Untagged";
        GameObject NextRoom = GameManager.instance.nowMapStat[DoorX, DoorY];
        GameManager.instance.NowMap = NextRoom;
        DoorController nowRoom = other.transform.parent.parent.parent.gameObject.GetComponent<DoorController>();
        nowRoom.roomChangeCollider = other;
        nowRoom.RoomPosition = RoomPosition;
        nowRoom.direction = Direction;
        nowRoom.speed = speed;
        nowRoom.DoorX = DoorX;
        nowRoom.DoorY = DoorY;
        nowRoom.RoomChange = true;
        NextRoom.transform.localPosition = RoomPosition;
        NextRoom.SetActive(true);
        roomChanger.gameObject.SetActive(true);
        roomChanger.GetComponent<LoadingUi>().BlackFadeInAndOut();
        yield return new WaitForSeconds(0.15f);
        // roomChanger.gameObject.SetActive(false);
        NextRoom.transform.localPosition = new Vector2(-200, -100);
        nowRoom.gameObject.SetActive(false);
        transform.tag = "Player";
        nowRoom.RoomChange = false;
        roomChangeChk = false;
        playerBox.isTrigger = false;
        GameManager.instance.roomChange = false;
    }
    IEnumerator BossRoomChange(Collider2D other, Vector2 RoomPosition, Vector2 Direction, float speed, int DoorX, int DoorY)
    {
        playerBox.isTrigger = false;
        transform.tag = "Untagged";
        GameObject NextRoom = GameManager.instance.nowMapStat[DoorX, DoorY];
        GameManager.instance.NowMap = NextRoom;
        DoorController nowRoom = other.transform.parent.parent.parent.gameObject.GetComponent<DoorController>();
        NextRoom.SetActive(true);
        if (GameManager.instance.bossEnter)
        {

        }
        else
        {
            BossLoad.gameObject.SetActive(true);
            GameManager.instance.bossEnter = true;
        }

        yield return new WaitForSeconds(0.1f);
        // roomChanger.gameObject.SetActive(false);
        NextRoom.transform.localPosition = new Vector2(-200, -100);
        nowRoom.gameObject.SetActive(false);
        transform.tag = "Player";
        nowRoom.RoomChange = false;
        roomChangeChk = false;
        playerBox.isTrigger = false;
        GameManager.instance.roomChange = false;
    }
    IEnumerator DropBoomDelay()
    {
        GameManager.instance.player_Stat.BoomCount--;
        GameObject boom = Pooling.instance.booms.Pop();
        boom.transform.SetParent(GameManager.instance.NowMap.transform, false);
        boom.SetActive(true);
        boom.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        yield return new WaitForSeconds(2.5f);
        isBoom = false;

    }
    IEnumerator HitDelay()
    {
        isHit = true;
        yield return new WaitForSeconds(1.5f);
        isHit = false;
    }
    IEnumerator DieAnime()
    {
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = PlayerActSprite[4];
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(0.2f);
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = PlayerActSprite[5];
        yield return new WaitForSecondsRealtime(0.2f);
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = PlayerActSprite[6];
        yield return new WaitForSecondsRealtime(1.6f);



    }



}
