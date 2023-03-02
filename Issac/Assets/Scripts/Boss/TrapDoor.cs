using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TrapDoor : MonoBehaviour
{
    bool isEnter;
    public GameObject trapDoor;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.bossCheck && GameManager.instance.monsterClearChk && !GameManager.instance.roomChange)
        {
            trapDoor.SetActive(true);
            gameObject.GetComponent<BoxCollider2D>().enabled = true;

        }
        else
        {
            trapDoor.SetActive(false);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            if (!isEnter)
            {
                isEnter = true;
                Debug.Log("why");

                StartCoroutine(Delay(other.gameObject));
            }




        }
    }
    IEnumerator Delay(GameObject objs)
    {

        objs.transform.GetChild(1).gameObject.SetActive(true);
        objs.transform.GetChild(4).gameObject.SetActive(false);
        objs.transform.GetChild(3).gameObject.SetActive(false);
        objs.transform.GetComponent<Player_Active>().isSceneChange = true;
        objs.transform.GetComponent<Player_Active>().transform.position = new Vector2(transform.position.x, transform.position.y + 0.1f);
        objs.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = objs.transform.GetComponent<Player_Active>().PlayerActSprite[2];
        objs.transform.GetComponent<Rigidbody2D>().velocity = Vector2.up * 8f;
        yield return new WaitForSeconds(0.2f);
        objs.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = objs.transform.GetComponent<Player_Active>().PlayerActSprite[3];
        objs.transform.GetComponent<Rigidbody2D>().velocity = Vector2.down * 16f;
        yield return new WaitForSeconds(0.1f);
        objs.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        objs.transform.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        GameManager.instance.stageNum++;
        isEnter = false;
        GameManager.instance.enddingVideo.gameObject.SetActive(true);


    }
}
