using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    // Start is called before the first frame update
    public Toggle[] PlayCheck;
    public float speed;
    public Vector2 direction;
    bool keyDelay;
    public Sprite[] volume;
    public Image sfxImage;
    public Image MusicImage;
    public AudioClip[] sound;
    // Start is called before the first frame update
    void Start()
    {
        keyDelay = false;
        MusicImage = PlayCheck[0].transform.GetChild(2).GetComponent<Image>();
        MusicImage.sprite = volume[SoundManager.instance.music - 1];
        sfxImage = PlayCheck[1].transform.GetChild(2).GetComponent<Image>();
        sfxImage.sprite = volume[SoundManager.instance.sfx - 1];

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<RectTransform>().localPosition.x > 0)
        {
            GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            direction = Vector2.zero;
        }
        InputKey();
        transform.Translate(direction * Time.deltaTime * speed);
    }
    void InputKey()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            if (!keyDelay)
            {
                keyDelay = true;
                if (PlayCheck[0].interactable)
                {
                    PlayCheck[0].interactable = false;
                    PlayCheck[1].interactable = true;
                }
                else
                {
                    PlayCheck[0].interactable = true;
                    PlayCheck[1].interactable = false;
                }
                SoundManager.instance.SfxPlay("", sound[0], SoundManager.instance.sfx / 100f);
                StartCoroutine(Delaykey());
            }

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (PlayCheck[0].interactable)
            {
                if (!keyDelay)
                {
                    keyDelay = true;
                    if (SoundManager.instance.music == 11)
                    {

                    }
                    else
                    {
                        SoundManager.instance.music++;
                        MusicImage.sprite = volume[SoundManager.instance.music - 1];

                    }
                    SoundManager.instance.SfxPlay("", sound[1], SoundManager.instance.sfx / 100f);
                    StartCoroutine(Delaykey());


                }
            }
            else
            {
                if (!keyDelay)
                {
                    keyDelay = true;
                    if (SoundManager.instance.sfx == 10)
                    {

                    }
                    else
                    {
                        SoundManager.instance.sfx++;
                        sfxImage.sprite = volume[SoundManager.instance.sfx];
                    }
                    SoundManager.instance.SfxPlay("", sound[1], SoundManager.instance.sfx / 100f);
                    StartCoroutine(Delaykey());
                }


            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (PlayCheck[0].interactable)
            {
                if (!keyDelay)
                {
                    keyDelay = true;
                    if (SoundManager.instance.music == 0)
                    {

                    }
                    else
                    {
                        SoundManager.instance.music--;
                        MusicImage.sprite = volume[SoundManager.instance.music];

                    }
                    SoundManager.instance.SfxPlay("", sound[0], SoundManager.instance.sfx / 100f);
                    StartCoroutine(Delaykey());


                }
            }
            else
            {
                if (!keyDelay)
                {
                    keyDelay = true;
                    if (SoundManager.instance.sfx == 1)
                    {

                    }
                    else
                    {
                        SoundManager.instance.sfx--;
                        sfxImage.sprite = volume[SoundManager.instance.sfx - 1];
                    }
                    SoundManager.instance.SfxPlay("", sound[0], SoundManager.instance.sfx / 100f);
                    StartCoroutine(Delaykey());
                }


            }



        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!keyDelay)
            {
                keyDelay = true;
                transform.parent.GetComponent<MainMenuController>().isOption = false;
                transform.parent.GetComponent<MainMenuController>().isOptionChange = true;
                StartCoroutine(Delaykey());
            }


        }


    }
    IEnumerator Delaykey()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        keyDelay = false;
        Debug.Log(keyDelay);
    }
}
