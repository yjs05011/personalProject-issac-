using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneKey : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigid;
    public AudioClip[] sfx;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        SoundManager.instance.SfxPlay("", sfx[0], SoundManager.instance.sfx / 100f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            GameManager.instance.player_Stat.KeyCount++;
            gameObject.SetActive(false);
        }
        if (other.transform.tag == "Wall")
        {
            rigid.velocity = Vector2.zero;
        }
    }
}
