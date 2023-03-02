using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTears : MonoBehaviour
{
    public BoxCollider2D tearHitBox;
    public RectTransform tearSize;
    public RectTransform tearImgSize;
    public Rigidbody2D tearRigid;
    public Rigidbody2D tearImgRigid;
    public Vector2 TearsstartPos;
    public Vector2 ShadowstartPos;

    public Player_Stat player_Stat;
    public Player_Active player_Active;
    public Rigidbody2D playerRigid;
    public RectTransform playerRect;
    public Vector2 startPosition;
    public float range = 0;
    public bool tearsTrigger;
    private bool isWall;
    private Animator tearsPop;
    private bool playerTouch;
    private bool isTrigger;
    private float damage;

    public void OnEnable()
    {
        playerTouch = false;
        tearImgRigid = transform.GetChild(0).gameObject.GetRigid();
        tearImgSize = transform.GetChild(0).gameObject.GetRect();

        tearHitBox = gameObject.GetBoxCollider();
        tearSize = gameObject.GetRect();
        tearRigid = gameObject.GetRigid();
        tearsPop = gameObject.transform.GetChild(0).GetComponent<Animator>();
        TearsstartPos = tearImgRigid.position;
        ShadowstartPos = tearSize.position;




    }
    // Start is called before the first frame update
    void Start()
    {

        Debug.Log($" Damage :{damage}");

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.roomChange)
        {
            tearImgRigid.velocity = Vector2.zero;
            tearRigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
            gameObject.transform.position = ShadowstartPos;
            tearImgSize.position = TearsstartPos;
            Pooling.instance.playerTears.Push(gameObject);
            tearsTrigger = false;
            isWall = false;
        }
        if (isWall) { }
        else
        {
            if (Vector2.Distance(TearsstartPos, tearSize.position) > GameManager.instance.player_Stat.Range / 2)
            {

                if (tearSize.position.y > tearImgSize.position.y || Vector2.Distance(TearsstartPos, tearSize.position) > (GameManager.instance.player_Stat.Range + 1) / 2)
                {
                    if (!tearsTrigger)
                    {
                        tearsTrigger = true;
                        StartCoroutine(TearsPop());
                    }

                }
                else
                {
                    tearImgRigid.velocity += new Vector2(0, -0.1f);
                }

            }

        }
        if (!playerTouch)
        {
            tearRigid.velocity = transform.up * 6f;
            tearImgRigid.velocity = transform.up * 6f;
        }

    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (!isTrigger)
        {


            if (other.transform.tag == "Player")
            {
                if (other.transform.GetComponent<Player_Active>().isHit == true)
                {
                    Debug.Log("isTrue");
                }
                else
                {
                    Debug.Log($"Time");
                    other.transform.GetComponent<Player_Active>().isHitChk = true;
                    if (GameManager.instance.player_Stat.SoulHeart >= 1)
                    {
                        GameManager.instance.player_Stat.SoulHeart -= 1;
                    }
                    else
                    {
                        GameManager.instance.player_Stat.NormalHeart -= 1;
                    }
                    GameManager.instance.StartCoroutineDeligate(GFunc.PlayerHit(other, 1, "", other.transform.GetComponent<Player_Active>().clip[0]));

                }
            }











        }
        if (other.transform.tag == "Rock" || other.transform.tag == "wall" || other.transform.tag == "LeftDoor" || other.transform.tag == "RightDoor" || other.transform.tag == "DownDoor" || other.transform.tag == "UpDoor")
        {
            if (!isWall)
            {
                isWall = true;
                tearImgRigid.velocity = Vector2.zero;
                tearRigid.velocity = Vector2.zero;
                StartCoroutine(TearsPop());
            }


        }

    }

    IEnumerator TearsPop()
    {
        tearsPop.SetBool("isTrigger", true);

        tearImgRigid.velocity = Vector2.zero;
        tearRigid.velocity = Vector2.zero;
        isTrigger = true;
        yield return new WaitForSeconds(0.2f);
        tearImgRigid.velocity = Vector2.zero;
        tearRigid.velocity = Vector2.zero;
        gameObject.SetActive(false);
        isTrigger = false;
        gameObject.transform.position = ShadowstartPos;
        tearImgSize.position = TearsstartPos;
        Pooling.instance.enemyTears.Push(gameObject);
        tearsTrigger = false;
        isWall = false;
    }

}

