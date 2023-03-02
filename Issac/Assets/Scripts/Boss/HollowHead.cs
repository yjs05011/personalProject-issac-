using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowHead : Monster_Active
{
    public float MaxHp = default;
    public int totalLength = 6;
    public GameObject hollowBody;
    public GameObject separateBoss;
    public List<HollowBody> body;
    public bool[] isDie;
    public bool cutting;
    public int cuttingBodyNum;
    public int bodyDieCount;
    public bool newCreate;
    public bool bodyDieCountChk;
    public bool separate;
    public bool copyCheck;
    private Rigidbody2D bossRigid;
    public Vector2 lastSpeed;
    public Vector2 saveVector;
    public Vector2 HeadPosition;
    private RectTransform bossRotation;
    private bool routinChk;
    public bool isDownCheck = false;

    // Start is called before the first frame update

    void OnEnable()
    {
        transform.SetAsFirstSibling();
    }
    void Awake()
    {

    }
    public override void Start()
    {
        bossRotation = GetComponent<RectTransform>();
        base.Start();
        monsterRenderer = GetComponent<SpriteRenderer>();
        bossRigid = GetComponent<Rigidbody2D>();

        GameManager.instance.bossCheck = true;

    }


    // Update is called once per frame
    void Update()
    {
        Direction(gameObject, saveVector);


        if (!newCreate && transform.parent.GetChild(0).GetSiblingIndex() == 0)
        {

            totalLength -= bodyDieCount;
            newCreate = true;
            body = new List<HollowBody>();
            for (int i = 0; i < totalLength; i++)
            {
                GameObject bodyObj = Instantiate(hollowBody);
                bodyObj.transform.SetParent(transform.parent, false);
                bodyObj.GetComponent<HollowBody>().number = bodyObj.transform.GetSiblingIndex();
                body.Add(bodyObj.GetComponent<HollowBody>());
            }
            saveVector = new Vector2(-3f, 3f);
        }
        else if (newCreate && transform.parent.parent.GetChild(1).GetSiblingIndex() == 1)
        {

            if (!bodyDieCountChk)
            {

            }
            else
            {
                Debug.Log("Doing");
                bodyDieCountChk = false;
                totalLength -= bodyDieCount;

                transform.SetParent(transform.parent.parent.GetChild(1));
                for (int i = 0; i < totalLength; i++)
                {
                    GameObject bodyObj = Instantiate(hollowBody);
                    bodyObj.transform.SetParent(transform.parent, false);
                    bodyObj.GetComponent<HollowBody>().number = bodyObj.transform.GetSiblingIndex();
                    body.Add(bodyObj.GetComponent<HollowBody>());
                }

            }
        }
        lastSpeed = bossRigid.velocity;
        bossRigid.velocity = saveVector;
        if (!routinChk)
        {
            if (!copyCheck)
            {
                StartCoroutine(Move(0));
            }
            else
            {
                StartCoroutine(Move(1));
            }

        }

        if (cutting)
        {

            cutting = false;
            Debug.Log($"cuttingBodyNum :{cuttingBodyNum}, totalLength : {totalLength}");
            if (totalLength >= 3 && totalLength - 1 > cuttingBodyNum && cuttingBodyNum >= 3)
            {
                BodySeparate();
            }
            else
            {
                Cutting();
            }
            int dieBodyCount = 0;
            if (!copyCheck)
            {
                for (int i = 1; i < transform.parent.parent.GetChild(0).childCount; i++)
                {
                    if (!transform.parent.parent.GetChild(0).GetChild(i).gameObject.activeSelf)
                    {
                        dieBodyCount++;
                    }
                }
                Debug.Log($"Main Body Count :{dieBodyCount}, totalLength : {transform.parent.parent.GetChild(0).childCount}");
                if (dieBodyCount == transform.parent.parent.GetChild(0).childCount - 1)
                {
                    gameObject.SetActive(false);
                    if (transform.parent.parent.GetChild(1).childCount != 0)
                    {
                        Debug.Log(transform.parent.childCount);
                        if (!transform.parent.parent.GetChild(0).GetChild(0).gameObject.activeSelf && !transform.parent.parent.GetChild(1).GetChild(0).gameObject.activeSelf)
                        {

                            GameManager.instance.bossHp = 0;
                            GameManager.instance.bossCheck = false;
                        }
                        else
                        {
                            GameManager.instance.bossHp = 60;
                        }



                    }
                    else
                    {
                        gameObject.SetActive(false);
                        GameManager.instance.bossCheck = false;
                    }

                }
            }
            else
            {
                for (int i = 1; i < transform.parent.parent.GetChild(1).childCount; i++)
                {
                    if (!transform.parent.parent.GetChild(1).GetChild(i).gameObject.activeSelf)
                    {
                        dieBodyCount++;
                    }
                }
                Debug.Log($"Main Body Count :{dieBodyCount}, totalLength : {transform.parent.parent.GetChild(0).childCount}");
                if (dieBodyCount == transform.parent.parent.GetChild(1).childCount - 1)
                {
                    gameObject.SetActive(false);
                    if (transform.parent.parent.GetChild(1).childCount != 0)
                    {
                        Debug.Log(transform.parent.parent.GetChild(0).GetChild(0).gameObject.activeSelf);
                        Debug.Log(transform.parent.parent.GetChild(1).GetChild(0).gameObject.activeSelf);
                        if (!transform.parent.parent.GetChild(0).GetChild(0).gameObject.activeSelf && !transform.parent.parent.GetChild(1).GetChild(0).gameObject.activeSelf)
                        {

                            GameManager.instance.bossHp = 0;
                            GameManager.instance.bossCheck = false;
                        }
                        else
                        {
                            GameManager.instance.bossHp = 60;
                        }



                    }

                }
            }



        }

    }
    void Cutting()
    {

        bodyDieCount = 0;
        if (!copyCheck)
        {
            for (int i = 1; i < transform.parent.childCount; i++)
            {
                if (!transform.parent.GetChild(i).GetComponent<HollowBody>().die)
                {
                    bodyDieCount++;

                }

            }
            totalLength = bodyDieCount;

        }
        else
        {
            for (int i = 1; i < transform.parent.childCount; i++)
            {
                if (!transform.parent.GetChild(i).GetComponent<HollowBody>().die)
                {
                    bodyDieCount++;

                }

            }
            totalLength = bodyDieCount;

        }

    }
    void BodySeparate()
    {

        if (!separate)
        {

            for (int i = cuttingBodyNum; i < totalLength + 1; i++)
            {
                transform.parent.parent.GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            separate = true;
            bodyDieCount = cuttingBodyNum + 1;
            GameObject NewBoss = Instantiate(separateBoss);
            NewBoss.transform.SetParent(transform.parent.parent.GetChild(1), false);
            NewBoss.GetComponent<HollowHead>().bodyDieCount = bodyDieCount;
            NewBoss.GetComponent<HollowHead>().separate = separate;
            NewBoss.GetComponent<HollowHead>().bodyDieCountChk = true;
            NewBoss.GetComponent<HollowHead>().copyCheck = true;
            NewBoss.GetComponent<HollowHead>().saveVector = -saveVector;



        }

    }

    public override void HitThisMonster(float damage)
    {
        int dieBodyCount = 0;
        if (!copyCheck)
        {
            for (int i = 1; i < transform.parent.GetChild(0).childCount; i++)
            {
                if (!transform.parent.GetChild(0).GetChild(i).gameObject.activeSelf)
                {
                    dieBodyCount++;
                }
            }
            if (dieBodyCount == transform.parent.GetChild(0).childCount - 1)
            {
                gameObject.SetActive(false);
            }
            base.HitThisMonster(damage);
            Cutting();
            transform.parent.GetChild(0).GetChild(totalLength - 1).GetComponent<HollowBody>().maxHp -= damage;
        }
        else
        {
            for (int i = 1; i < transform.parent.GetChild(1).childCount; i++)
            {
                if (!transform.parent.GetChild(1).GetChild(i).gameObject.activeSelf)
                {
                    dieBodyCount++;
                }
            }
            if (dieBodyCount == transform.parent.GetChild(1).childCount - 1)
            {
                gameObject.SetActive(false);
            }
            base.HitThisMonster(damage);
            Cutting();
            transform.parent.GetChild(1).GetChild(totalLength - 1).GetComponent<HollowBody>().maxHp -= damage;
        }


    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        switch (other.transform.tag)
        {

            case "Boom":
                other.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                break;

            default:
                var speed = lastSpeed.magnitude;
                var dir = Vector2.Reflect(lastSpeed.normalized, other.contacts[0].normal);
                saveVector = dir * Mathf.Max(speed, 0f);
                bossRigid.velocity = saveVector;
                if (Direction(gameObject, saveVector).eulerAngles.z >= 180f)
                {
                    isDownCheck = true;
                    gameObject.GetComponent<Animator>().SetBool("isDown", true);
                }
                else
                {

                    isDownCheck = false;
                    gameObject.GetComponent<Animator>().SetBool("isDown", false);
                }
                break;



        }

    }
    public Quaternion Direction(GameObject Player, Vector2 vector)
    {
        Vector2 direction = new Vector2(Player.transform.position.x, Player.transform.position.y) - vector;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 180f, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(Player.transform.rotation, angleAxis, 1);
        return angleAxis;
    }
    IEnumerator Move(int index)
    {
        routinChk = true;
        HeadPosition = bossRotation.position;

        for (int i = 1; i < totalLength + 1; i++)
        {

            if (transform.parent.parent.GetChild(index).GetChild(i).gameObject.activeSelf)
            {
                transform.parent.parent.GetChild(index).GetChild(i).GetComponent<RectTransform>().position = bossRotation.position;


                yield return new WaitForSeconds(0.15f);
            }
            else
            {
                yield return null;
            }

        }
        routinChk = false;
    }

}
