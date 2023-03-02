using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceCharator : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] sound;
    public Charator[] character;
    public Sprite[] PlayerNameSprite;
    public float speed;
    public Vector2 direction;
    public Vector2 charactorDirectionUp;
    public Vector2 charactorDirectionDown;
    public float spinSpeed;
    bool keyDelay;
    public Image playerName;
    public Image[] characterImg;
    public Sprite[] stat;
    public Image[] statImg;
    public int characterNumber;
    public RectTransform[] imgRect;
    private float CharatorSmallSize = 65;
    private float CharatorSize = 65;
    private float time;
    public Image fadein;
    // Start is called before the first frame update
    void Awake()
    {
        keyDelay = false;
        characterNumber = 0;
        time = 0;
        imgRect = new RectTransform[2];
        DataSet();
        fadein.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<RectTransform>().localPosition.x < 0)
        {
            GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            direction = Vector2.zero;
        }
        InputKey();
        transform.Translate(direction * Time.deltaTime * speed);
        if (imgRect[0].localPosition.y <= 50)
        {
            imgRect[0].localPosition = new Vector3(0, 50, 0);
            imgRect[0].sizeDelta = new Vector2(100, 100);
            charactorDirectionDown = Vector2.zero;
            if (characterNumber == 1)
            {
                characterImg[characterNumber].color = new Color(1f, 1f, 1f, 1f);

                characterImg[0].color = new Color(1f, 1f, 1f, 0.7f);
            }
            else
            {
                characterImg[characterNumber].color = new Color(1f, 1f, 1f, 1f);

                characterImg[1].color = new Color(1f, 1f, 1f, 0.7f);
            }
        }
        else
        {
            imgRect[0].transform.Translate(charactorDirectionDown * Time.deltaTime * spinSpeed);

        }

        if (imgRect[1].localPosition.y >= 114)
        {
            imgRect[1].localPosition = new Vector3(0, 144, 0);
            imgRect[1].sizeDelta = new Vector2(65, 65);
            charactorDirectionUp = Vector2.zero;
            if (characterNumber == 1)
            {
                characterImg[characterNumber].color = new Color(1f, 1f, 1f, 1f);
                characterImg[0].color = new Color(1f, 1f, 1f, 0.7f);
            }
            else
            {
                characterImg[characterNumber].color = new Color(1f, 1f, 1f, 1f);

                characterImg[1].color = new Color(1f, 1f, 1f, 0.7f);
            }


        }
        else
        {
            imgRect[1].transform.Translate(charactorDirectionUp * Time.deltaTime * spinSpeed);

        }



    }
    void InputKey()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!keyDelay)
            {


                keyDelay = true;
                RightArrow();
                SoundManager.instance.SfxPlay("", sound[1], SoundManager.instance.sfx / 100f);
                StartCoroutine(Delaykey());
            }



        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!keyDelay)
            {

                keyDelay = true;
                LeftArrow();
                SoundManager.instance.SfxPlay("", sound[0], SoundManager.instance.sfx / 100f);
                StartCoroutine(Delaykey());
            }



        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!keyDelay)
            {
                keyDelay = true;
                transform.parent.GetComponent<MainMenuController>().isNewRun = false;
                transform.parent.GetComponent<MainMenuController>().isNewRunChange = true;
                StartCoroutine(Delaykey());
            }


        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!keyDelay)
            {
                keyDelay = true;
                Debug.Log(characterNumber);
                GFunc.PlayerStatReset(characterNumber);
                transform.parent.GetComponent<AudioSource>().Stop();
                SoundManager.instance.SfxPlay("", sound[2], SoundManager.instance.sfx / 100f);
                StartCoroutine(FadeIn(5f));
                StartCoroutine(Delaykey());
            }

        }


    }
    IEnumerator Delaykey()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        keyDelay = false;

    }
    public void DataSet()
    {
        playerName.sprite = PlayerNameSprite[characterNumber];
        statImg[0].sprite = stat[(int)character[characterNumber].maxHp / 2];
        statImg[1].sprite = stat[(int)character[characterNumber].speed];
        statImg[2].sprite = stat[(int)character[characterNumber].str];
        if (characterNumber == 1)
        {
            imgRect[0] = characterImg[characterNumber].GetComponent<RectTransform>();
            imgRect[1] = characterImg[0].GetComponent<RectTransform>();
        }
        else
        {
            imgRect[0] = characterImg[characterNumber].GetComponent<RectTransform>();
            imgRect[1] = characterImg[1].GetComponent<RectTransform>();
        }
        TrunCharactor();
        // Debug.Log($" imgRect :{imgRect[0]}");
        // Debug.Log($" imgRect :{imgRect[1]}");
        // StartCoroutine(SizeUp(imgRect[0]));
        // StartCoroutine(SizeDown(imgRect[1]));
    }

    public void RightArrow()
    {
        if (characterNumber == 1)
        {
            characterNumber = 0;
        }
        else
        {
            characterNumber++;
        }
        DataSet();
    }
    public void LeftArrow()
    {
        if (characterNumber == 0)
        {
            characterNumber = 1;
        }
        else
        {
            characterNumber--;
        }
        DataSet();
    }
    IEnumerator SizeUp(RectTransform imgRect)
    {
        while (imgRect.sizeDelta.x < 100f && imgRect.sizeDelta.y < 100f)
        {
            imgRect.sizeDelta = imgRect.sizeDelta * 0.5f * time;
            time += 0.1f;
            if (imgRect.sizeDelta.x >= 100f || imgRect.sizeDelta.y >= 100f)
            {
                imgRect.sizeDelta = new Vector2(CharatorSize, CharatorSize);
                time = 0;
                Debug.Log(imgRect.sizeDelta);
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
    public void TrunCharactor()
    {
        spinSpeed = 200f;
        StartCoroutine(DirectChangerUp(new Vector2(0, 50)));

        StartCoroutine(DirectChangerDown(new Vector2(0, 144)));


    }
    IEnumerator SizeDown(RectTransform imgRect)
    {
        while (imgRect.sizeDelta.x > 65f && imgRect.sizeDelta.y > 65f)
        {
            imgRect.sizeDelta = imgRect.sizeDelta * 0.5f * time;
            time += 0.1f;
            if (imgRect.sizeDelta.x <= 66f || imgRect.sizeDelta.y <= 66f)
            {
                imgRect.sizeDelta = new Vector2(CharatorSmallSize, CharatorSmallSize);
                time = 0;
                Debug.Log(imgRect.sizeDelta);
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator DirectChangerUp(Vector2 vector)
    {


        vector = (Vector2.up + Vector2.left);
        charactorDirectionUp = vector;
        yield return new WaitForSeconds(0.1f);
        vector = (Vector2.up + Vector2.right);
        charactorDirectionUp = vector;
    }
    IEnumerator DirectChangerDown(Vector2 vector)
    {
        vector = (Vector2.down + Vector2.left);
        charactorDirectionDown = vector;
        yield return new WaitForSeconds(0.1f);
        vector = (Vector2.down + Vector2.right);
        charactorDirectionDown = vector;
    }
    IEnumerator FadeIn(float totalTime)
    {
        fadein.gameObject.SetActive(true);
        float delay = totalTime / 30;

        // alpha go 0 to 0.9
        float alpha = 0;
        while (alpha <= 1)
        {
            alpha += 0.1f;
            fadein.color =
             new Color(fadein.color.r, fadein.color.g, fadein.color.b, alpha);
            yield return new WaitForSeconds(delay);
            // Debug.Log("FadeInroutineCheck");
        }
        GFunc.SceneChanger("Stage1");

    }

}
