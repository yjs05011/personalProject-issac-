using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChoiceMenu : MonoBehaviour
{
    public Toggle[] PlayCheck;
    public float speed;
    public Vector2 direction;
    public bool isExport;
    bool keyDelay;
    public AudioClip[] sound;
    // Start is called before the first frame update
    void Start()
    {
        keyDelay = false;
        isExport = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (isExport)
        {
            if (GetComponent<RectTransform>().localPosition.y < 0 || GetComponent<RectTransform>().localPosition.x > 0)
            {
                GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                direction = Vector2.zero;
                isExport = false;
            }
        }
        else
        {
            if (GetComponent<RectTransform>().localPosition.y > 0 || GetComponent<RectTransform>().localPosition.x < 0)
            {
                GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                direction = Vector2.zero;
            }
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
                StartCoroutine(Delaykey());
                SoundManager.instance.SfxPlay("", sound[0], SoundManager.instance.sfx / 100f);
            }

        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!keyDelay)
            {
                if (PlayCheck[0].interactable)
                {
                    transform.parent.GetComponent<MainMenuController>().isNewRun = true;
                    transform.parent.GetComponent<MainMenuController>().isNewRunChange = true;
                }
                else
                {
                    transform.parent.GetComponent<MainMenuController>().isOption = true;
                    transform.parent.GetComponent<MainMenuController>().isOptionChange = true;
                }
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
