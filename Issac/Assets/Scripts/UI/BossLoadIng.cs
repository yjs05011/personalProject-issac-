using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossLoadIng : MonoBehaviour
{
    public Image vsImg;
    public Image bossImg;
    public Image playerImg;
    public Image playerNameImg;
    public Image bossNameImg;
    public Sprite[] player;
    public Sprite[] playerName;
    public Sprite[] boss;
    public Sprite[] bossName;
    bool isClick;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        isClick = false;
        playerImg.sprite = player[GameManager.instance.player_Stat.ID - 1];
        playerNameImg.sprite = playerName[GameManager.instance.player_Stat.ID - 1];
        bossImg.sprite = boss[GameManager.instance.bossType - 1];
        bossNameImg.sprite = bossName[GameManager.instance.bossType - 1];
        StartCoroutine(VsImageMoving(0.005f));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isClick = true;

        }
    }
    IEnumerator VsImageMoving(float Second)
    {
        float TotalTime = Second / 720;
        float rotation = 0f;
        float alpha = 0f;
        Vector2 size = new Vector2(500, 500);
        while (rotation <= 360f)
        {
            if (isClick)
            {
                break;
            }
            rotation += 4;
            if (alpha < 1)
            {
                alpha += 0.01f;
            }
            if (size.x > 200 && size.x > 200)
            {
                size -= new Vector2(2, 2);
            }
            vsImg.color = new Color(vsImg.color.r, vsImg.color.g, vsImg.color.b, alpha);
            vsImg.GetComponent<RectTransform>().sizeDelta = size;
            // vsImg.GetComponent<RectTransform>().rotation = new Quaternion.eulerAngle = rotation
            GFunc.SetRotation(vsImg.GetComponent<RectTransform>(), new Vector3(0, 0, rotation));


            yield return new WaitForSecondsRealtime(TotalTime);
        }
        if (isClick)
        {


        }
        else
        {
            yield return new WaitForSecondsRealtime(1f);
        }
        Time.timeScale = 1f;
        gameObject.SetActive(false);

    }
}
