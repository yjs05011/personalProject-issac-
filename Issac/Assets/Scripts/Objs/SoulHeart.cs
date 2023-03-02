using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulHeart : MonoBehaviour
{
    BoxCollider2D Heart;
    Rigidbody2D rigid;
    public AudioClip[] sfx;
    // Start is called before the first frame update
    void Start()
    {
        Heart = transform.GetChild(0).GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        //SoundManager.instance.SfxPlay("", sfx[0], SoundManager.instance.sfx / 100f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Heart.isTrigger)
        {
            StartCoroutine(ImpossiblePickUp());
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            if (GameManager.instance.player_Stat.MaxHp == 24f || GameManager.instance.player_Stat.MaxHp + GameManager.instance.player_Stat.SoulHeart == 24f)
            {
                Heart.isTrigger = false;
            }
            else
            {
                if (GameManager.instance.player_Stat.MaxHp + GameManager.instance.player_Stat.SoulHeart == 23f)
                {
                    GameManager.instance.player_Stat.SoulHeart++;
                    gameObject.SetActive(false);
                }
                else
                {
                    GameManager.instance.player_Stat.SoulHeart += 2;
                    gameObject.SetActive(false);
                }

            }

        }
        if (other.transform.tag == "Wall")
        {
            rigid.velocity = Vector2.zero;
        }
    }
    IEnumerator ImpossiblePickUp()
    {
        yield return new WaitForSeconds(0.5f);
        rigid.velocity = Vector2.zero;
        Heart.isTrigger = true;
    }
}
