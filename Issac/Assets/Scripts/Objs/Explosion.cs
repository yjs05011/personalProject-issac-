using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    CircleCollider2D boomCollider;

    void OnEnable()
    {
        boomCollider = GetComponent<CircleCollider2D>();

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            if (other.transform.tag == "Player")
            {
                if (other.transform.GetComponent<Player_Active>().isHit == true)
                {
                    // Debug.Log("isTrue");
                }
                else
                {
                    // Debug.Log($"Time");
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

        if (other.transform.tag == "Monster")
        {
            other.transform.parent.GetComponent<Monster_Active>().maxHp -= 40;

        }
        if (other.transform.tag == "Rock")
        {
            other.transform.gameObject.SetActive(false);
        }
        if (other.transform.tag == "Boss")
        {
            other.GetComponent<Monster_Active>().HitThisMonster(40);
        }
        Debug.Log($"the rock Test :{other.tag}");
    }
}
