using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : Monster_Active
{
    // Start is called before the first frame update

    protected Rigidbody2D EnemyRigd;
    private Animator bodyAni;
    private bool die;
    private bool routinChk;
    public override void Start()
    {

        base.Start();
        EnemyRigd = gameObject.GetComponent<Rigidbody2D>();
        bodyAni = GetComponent<Animator>();
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
            bodyAni.SetBool("isDie", true);
            if (!routinChk)
            {
                routinChk = true;
                StartCoroutine(DidDelay());
            }



        }





    }


    public virtual void OnTriggerStay2D(Collider2D other)
    {

        if (other.transform.tag == "Player")
        {
            EnemyRigd.velocity = (other.transform.position - transform.position).normalized * stat.speed * 3f;

        }
    }


    IEnumerator DidDelay()
    {
        yield return new WaitForSeconds(0.2f);
        transform.gameObject.SetActive(false);
        GameManager.instance.monsterCount--;
    }
}
