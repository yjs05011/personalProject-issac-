using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clotty : Monster_Active

{
    protected Rigidbody2D EnemyRigd;
    protected MonsterTears tears;
    protected RectTransform rotationBody;
    private bool isMove;
    private bool die;
    private Animator bodyAni;
    // Start is called before the first frame update
    public override void Start()
    {

        base.Start();
        EnemyRigd = gameObject.GetComponent<Rigidbody2D>();
        Debug.Log($"statHp :{stat.maxHp}");
        Debug.Log($"StageNum :{GameManager.instance.stageNum}");
        rotationBody = transform.GetChild(1).GetComponent<RectTransform>();
        bodyAni = transform.GetChild(0).GetComponent<Animator>();
        Debug.Log(maxHp);
        if (gameObject.activeSelf)
        {
            GameManager.instance.monsterCount++;
        }
        maxHp = stat.maxHp + GameManager.instance.stageNum * 0.8f;
        monsterType = 2;

    }

    // Update is called once per frame
    void Update()
    {

        if (maxHp < 0)
        {
            die = true;
        }

        if (!die)
        {
            if (!isMove)
            {
                isMove = true;
                StartCoroutine(RandomAct());
            }
        }
        else
        {
            transform.gameObject.SetActive(false);
            GameManager.instance.monsterCount--;
        }



    }
    IEnumerator RandomAct()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                EnemyRigd.velocity = new Vector2(stat.speed, stat.speed);
                break;
            case 1:
                EnemyRigd.velocity = new Vector2(-stat.speed, stat.speed);
                break;
            case 2:
                EnemyRigd.velocity = new Vector2(stat.speed, -stat.speed);
                break;
            case 3:
                EnemyRigd.velocity = new Vector2(-stat.speed, -stat.speed);
                break;

        }
        yield return new WaitForSeconds(1f);
        EnemyRigd.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.3f);
        ShotTears();

        isMove = false;
    }
    void ShotTears()
    {
        for (int i = 0; i < 4; i++)
        {
            rotationBody.SetRotation(0, 0, 90f * i);

            tears = Pooling.instance.enemyTears.Pop().GetComponent<MonsterTears>();

            tears.tearSize.rotation = rotationBody.rotation;
            if (i == 1)
            {

                tears.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.3f, gameObject.transform.position.z);
                tears.tearImgSize.position =
                new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z);
                // Debug.Log($"Right :{tears.transform.GetChild(0).gameObject.transform.position}");
                // Debug.Log($"shadow :{tears.transform.position.y}");
                // Debug.Log($"Tears : {tears.transform.position.y}");
                // Debug.Log($"shadow rotation :{tears.tearSize.rotation.eulerAngles}");
                // Debug.Log($"Tears rotation : {tears.tearImgSize.rotation.eulerAngles}");
            }
            if (i == 3)
            {

                tears.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.3f, gameObject.transform.position.z);
                tears.transform.GetChild(0).gameObject.transform.position =
                new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z);
                // Debug.Log($"left :{tears.transform.GetChild(0).gameObject.transform.position}");
            }
            if (i == 0)
            {

                tears.transform.position = gameObject.transform.position;
                tears.transform.GetChild(0).gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.1f, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            if (i == 2)
            {

                tears.transform.position = gameObject.transform.position;
                tears.transform.GetChild(0).gameObject.transform.position = new Vector3(gameObject.transform.position.x - 0.1f, gameObject.transform.position.y, gameObject.transform.position.z);
            }

            tears.gameObject.SetActive(true);
        }
    }


}

