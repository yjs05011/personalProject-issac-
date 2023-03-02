using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horf : Monster_Active
{

    protected CircleCollider2D FindEnemy;
    protected Rigidbody2D EnemyRigd;
    protected SpriteRenderer Body;
    protected SpriteRenderer Head;
    protected RectTransform rotationBody;
    private bool slice;
    private bool die;
    private Animator bodyAni;
    // Start is called before the first frame update
    public override void OnEnable()
    {
        base.OnEnable();
        Debug.Log("?");
    }
    public override void Start()
    {
        FindEnemy = transform.GetChild(0).GetComponent<CircleCollider2D>();
        EnemyRigd = gameObject.GetComponent<Rigidbody2D>();
        rotationBody = transform.GetChild(3).GetComponent<RectTransform>();
        Head = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Body = transform.GetChild(2).GetComponent<SpriteRenderer>();
        Debug.Log($"statHp :{stat.maxHp}");
        Debug.Log($"StageNum :{GameManager.instance.stageNum}");
        maxHp = stat.maxHp + GameManager.instance.stageNum * 0.8f;
        bodyAni = transform.GetChild(2).GetComponent<Animator>();
        Debug.Log(maxHp);
        if (gameObject.activeSelf)
        {
            GameManager.instance.monsterCount++;
        }
        monsterType = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (die && maxHp < 0)
        {
            gameObject.SetActive(false);
            GameManager.instance.monsterCount--;
        }
        if (maxHp < 0)
        {
            die = true;
        }
        if (die)
        {
            if (Random.Range(0, 3) == 0)
            {
                Head.transform.gameObject.SetActive(false);
                slice = true;
                maxHp = (10.5f + GameManager.instance.stageNum * 0.8f) / 2;
                die = false;

            }
        }
        if (slice)
        {
        }
        else
        {

        }



    }

    public virtual void OnTriggerStay2D(Collider2D other)
    {
        if (!slice)
        {
            if (other.transform.tag == "Player")
            {
                EnemyRigd.velocity = (other.transform.position - transform.position).normalized * stat.speed * 3f;
                if (Mathf.Abs(EnemyRigd.velocity.x) > Mathf.Abs(EnemyRigd.velocity.y))
                {
                    bodyAni.SetBool("SideMove", true);
                    bodyAni.SetBool("FrontMove", false);
                }
                else
                {
                    bodyAni.SetBool("SideMove", false);
                    bodyAni.SetBool("FrontMove", true);
                }

            }
        }
        else
        {
            if (other.transform.tag == "Player")
            {
                EnemyRigd.velocity = (other.transform.position - transform.position).normalized * stat.speed;

            }


        }
        if (other.transform.tag == "PlayerTears")
        {
            //if()
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {

    }
}
