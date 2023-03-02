using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tears : MonoBehaviour
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
    public PointEffector2D pointEffector;
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
        damage = GameManager.instance.player_Stat.Str;
        tearImgSize.localScale = new Vector3(1f + GameManager.instance.player_Stat.Str * 0.1f, 1 + GameManager.instance.player_Stat.Str * 0.1f, 1f);
        tearSize.localScale = new Vector3(1f + GameManager.instance.player_Stat.Str * 0.1f, 1 + GameManager.instance.player_Stat.Str * 0.1f, 1f);



    }
    // Start is called before the first frame update
    void Start()
    {
        pointEffector = GetComponent<PointEffector2D>();
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


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTrigger)
        {
            if (other.transform.tag == "Player")
            {
                if (!playerTouch)
                {
                    playerTouch = true;

                    player_Active = other.gameObject.GetComponent<Player_Active>();
                    playerRigid = other.gameObject.GetRigid();
                    tearRigid.velocity = transform.up * 6f * GameManager.instance.player_Stat.ShotSpeed;
                    tearImgRigid.velocity = transform.up * 6f * GameManager.instance.player_Stat.ShotSpeed;
                    tearImgRigid.velocity = new Vector2(tearImgRigid.velocity.x, tearImgRigid.velocity.y - 0.2f);
                    if ((tearRigid.velocity.x - playerRigid.velocity.x + 2f) > 3f || (tearRigid.velocity.y - playerRigid.velocity.y + 2f) > 3f)
                    {

                    }
                    else if ((tearRigid.velocity.x - playerRigid.velocity.x + 2f) < -3f || (tearRigid.velocity.y - playerRigid.velocity.y + 2f) < -3f)
                    {

                    }
                    else
                    {
                        tearRigid.velocity += playerRigid.velocity;
                        tearImgRigid.velocity += playerRigid.velocity;
                    }

                    playerRect = other.gameObject.GetComponent<RectTransform>();
                    tearSize.transform.rotation = player_Active.playerHead.rotation;
                    startPosition = playerRect.transform.position;
                }


            }
            if (other.transform.tag == "wall" || other.transform.tag == "LeftDoor" || other.transform.tag == "RightDoor" || other.transform.tag == "DownDoor" || other.transform.tag == "UpDoor")
            {
                if (!isWall)
                {
                    isWall = true;
                    tearImgRigid.velocity = Vector2.zero;
                    tearRigid.velocity = Vector2.zero;
                    StartCoroutine(TearsPop());
                }


            }


            if (other.transform.tag == "Monster")
            {

                pointEffector.enabled = true;
                StartCoroutine(MonsterHit(other));
                other.transform.parent.GetComponent<Monster_Active>().maxHp -= damage;
                StartCoroutine(TearsPop());
                Debug.Log($" Damage :{damage}");

                Debug.Log($" horf Hp :{other.transform.parent.GetComponent<Monster_Active>().maxHp}");
            }
            if (other.transform.tag == "Boom")
            {
                Debug.Log("Why");
                pointEffector.enabled = true;

                StartCoroutine(TearsPop());

            }
            if (other.transform.tag == "Boss")
            {
                StartCoroutine(MonsterHit(other));
                GameManager.instance.monsterHit = true;
                other.GetComponent<Monster_Active>().HitThisMonster(damage);
                StartCoroutine(TearsPop());
            }


        }

    }

    IEnumerator TearsPop()
    {
        tearsPop.SetBool("isTrigger", true);
        isTrigger = true;
        tearImgRigid.velocity = Vector2.zero;
        tearRigid.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.2f);
        tearImgRigid.velocity = Vector2.zero;
        tearRigid.velocity = Vector2.zero;
        gameObject.SetActive(false);
        isTrigger = false;
        gameObject.transform.position = ShadowstartPos;
        tearImgSize.position = TearsstartPos;
        pointEffector.enabled = false;
        Pooling.instance.playerTears.Push(gameObject);
        tearsTrigger = false;
        isWall = false;
    }

    public IEnumerator MonsterHit(Collider2D other)
    {
        SpriteRenderer monsterSprite = other.GetComponent<SpriteRenderer>();
        monsterSprite.color = new Color(1, 0, 0, 1f);
        yield return new WaitForSeconds(0.1f);
        monsterSprite.color = new Color(1, 1, 1, 1f);
    }
}
