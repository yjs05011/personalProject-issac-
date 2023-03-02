using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowBody : Monster_Active
{

    public bool die;
    public float hp;
    public int number;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        maxHp = stat.maxHp;
        hp = maxHp;
        GameManager.instance.bossHp += 20;
        die = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (hp > maxHp)
        {
            if (maxHp <= 0)
            {
                maxHp = 0;
                GameManager.instance.bossHp -= hp - maxHp;
                hp = maxHp;
            }
            else
            {
                GameManager.instance.bossHp -= hp - maxHp;
                hp = maxHp;
            }


        }
        if (maxHp <= 0)
        {
            die = true;

            transform.SetAsLastSibling();
            transform.parent.GetChild(0).GetComponent<HollowHead>().cutting = true;
            transform.parent.GetChild(0).GetComponent<HollowHead>().cuttingBodyNum = number;
            gameObject.SetActive(false);



        }
        if (transform.parent.GetChild(0).GetComponent<HollowHead>().isDownCheck)
        {
            gameObject.GetComponent<Animator>().SetBool("isDown", true);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("isDown", false);
        }


    }
    public override void HitThisMonster(float damage)
    {
        base.HitThisMonster(damage);

        maxHp -= damage;
    }

}


