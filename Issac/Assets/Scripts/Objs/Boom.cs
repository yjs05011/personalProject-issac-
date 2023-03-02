using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private BoxCollider2D boomSize;
    private CircleCollider2D boomBlow;
    private bool isPlayer;
    private SpriteRenderer smallboom;
    private RectTransform boomRect;
    private Animator bigBoom;
    public AudioClip sfx;
    // Start is called before the first frame update
    private void OnEnable()
    {
        smallboom = GetComponent<SpriteRenderer>();
        bigBoom = transform.GetChild(0).GetComponent<Animator>();
        boomSize = GetComponent<BoxCollider2D>();
        boomBlow = transform.GetChild(0).GetComponent<CircleCollider2D>();
        boomRect = GetComponent<RectTransform>();
        StartCoroutine(Explosion());
        StartCoroutine(BoomColorChage());
        bigBoom.transform.gameObject.SetActive(false);
        boomSize.isTrigger = true;
        //SoundManager.instance.SfxPlay("", sfx, SoundManager.instance.sfx / 100f);





    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.roomChange)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            boomSize.isTrigger = true;
            isPlayer = true;
        }
        else { boomSize.isTrigger = false; }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            boomSize.isTrigger = false;
            isPlayer = true;
        }
    }

    IEnumerator Explosion()
    {

        yield return new WaitForSeconds(2.5f);
        boomBlow.transform.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        boomSize.isTrigger = true;
        boomBlow.transform.gameObject.SetActive(false);
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Pooling.instance.booms.Push(gameObject);
    }
    IEnumerator BoomColorChage()
    {
        for (int i = 0; i < 17; i++)
        {
            smallboom.color = new Color(1, 0, 0, 1);
            boomRect.localScale = new Vector3(3, 3, 1);
            yield return new WaitForSeconds(0.05f);
            boomRect.localScale = new Vector3(3, 2.5f, 1);
            smallboom.color = new Color(1, 1, 0, 1);
            yield return new WaitForSeconds(0.05f);
            boomRect.localScale = new Vector3(3, 2f, 1);
            smallboom.color = new Color(0, 0, 0, 1);
            yield return new WaitForSeconds(0.05f);
        }


    }
}

