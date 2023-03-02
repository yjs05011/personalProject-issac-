using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UiController : MonoBehaviour
{
    public Button[] pauseButton;

    int ToggleCounter;
    Color defaultColor;
    Color changeColor;
    bool keyDelay;
    bool isDie;
    // Start is called before the first frame update
    void Start()
    {
        defaultColor = new Color(1f, 1f, 1f, 1f);
        changeColor = new Color(0.2f, 0.2f, 0.2f, 1f);
        pauseButton[0].transform.GetChild(0).GetComponent<TMP_Text>().color = defaultColor;
        pauseButton[1].transform.GetChild(0).GetComponent<TMP_Text>().color = changeColor;
        pauseButton[2].transform.GetChild(0).GetComponent<TMP_Text>().color = changeColor;
        isDie = false;
        GetComponent<AudioSource>().volume = SoundManager.instance.music / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
        Die();
    }
    public void Die()
    {
        if (GameManager.instance.player_Stat.Die)
        {
            if (!isDie)
            {
                isDie = true;
                StartCoroutine(DieLoading());

            }
            if (transform.GetChild(2).gameObject.activeSelf)
            {
                if (Input.anyKeyDown)
                {
                    Time.timeScale = 1f;
                    GFunc.SceneChanger("MainMenu");
                }
            }
        }
    }
    public void Pause()
    {
        if (!GameManager.instance.roomChange)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (transform.GetChild(0).gameObject.activeSelf)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                    Time.timeScale = 1f;
                    GameManager.instance.MainUiActive = false;
                    ToggleCheck();
                    ToggleCounter = 0;
                    Debug.Log(ToggleCounter);
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    Time.timeScale = 0f;
                    GameManager.instance.MainUiActive = true;
                    ToggleCheck();

                    ToggleCounter = 0;
                    Debug.Log(ToggleCounter);
                }

            }
        }
        if (GameManager.instance.MainUiActive)
        {
            PressEnter();
            InputKey();
        }



    }
    public void ToggleCheck()
    {

        if (ToggleCounter % 3 == 0)
        {
            pauseButton[0].transform.GetChild(0).GetComponent<TMP_Text>().color = defaultColor;
            pauseButton[1].transform.GetChild(0).GetComponent<TMP_Text>().color = changeColor;
            pauseButton[2].transform.GetChild(0).GetComponent<TMP_Text>().color = changeColor;
            ToggleCounter = 0;
        }
        else if (ToggleCounter % 3 == 1)
        {
            pauseButton[0].transform.GetChild(0).GetComponent<TMP_Text>().color = changeColor;
            pauseButton[1].transform.GetChild(0).GetComponent<TMP_Text>().color = defaultColor;
            pauseButton[2].transform.GetChild(0).GetComponent<TMP_Text>().color = changeColor;
        }
        if (ToggleCounter % 3 == 2)
        {
            pauseButton[0].transform.GetChild(0).GetComponent<TMP_Text>().color = changeColor;
            pauseButton[1].transform.GetChild(0).GetComponent<TMP_Text>().color = changeColor;
            pauseButton[2].transform.GetChild(0).GetComponent<TMP_Text>().color = defaultColor;
        }


    }
    public void PressEnter()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log($"Press Enter:{ToggleCounter}");
            switch (ToggleCounter)
            {
                case 0:
                    Restart();
                    break;
                case 1:
                    ResumeGame();
                    break;
                case 2:
                    ExitGame();
                    break;
            }
        }
    }
    public void InputKey()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (!keyDelay)
            {
                keyDelay = true;
                if (ToggleCounter <= 0)
                {
                    ToggleCounter = 2;
                }
                else
                {
                    ToggleCounter--;
                }

                Debug.Log(ToggleCounter);
                StartCoroutine(Delaykey());
            }

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (!keyDelay)
            {
                keyDelay = true;
                ToggleCounter++;
                Debug.Log(ToggleCounter);
                StartCoroutine(Delaykey());

            }
        }

    }


    public void Restart()
    {
        ToggleCounter = 0;
        GFunc.PlayerStatReset(GameManager.instance.player_Stat.ID - 1);
        Time.timeScale = 1f;
        GameManager.instance.MainUiActive = false;
        GFunc.SceneChanger("Stage1");
    }

    public void ResumeGame()
    {
        ToggleCounter = 0;
        transform.GetChild(0).gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameManager.instance.MainUiActive = false;
    }
    public void ExitGame()
    {

        ToggleCounter = 0;
        transform.GetChild(0).gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameManager.instance.MainUiActive = false;
        GFunc.SceneChanger("MainMenu");
    }
    IEnumerator Delaykey()
    {
        ToggleCheck();
        yield return new WaitForSecondsRealtime(0.1f);
        keyDelay = false;
        Debug.Log(keyDelay);
    }
    IEnumerator DieLoading()
    {

        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 0f;
        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        transform.GetChild(0).GetComponent<LoadingUi>().SceneChanger.color = new Color(0f, 0f, 0f, 1f);
        transform.GetChild(2).gameObject.SetActive(true);
    }
}
