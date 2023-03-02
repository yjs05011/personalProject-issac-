using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSoundController : MonoBehaviour
{
    AudioSource introSound = default;
    public Canvas mainmenu;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.introSkip)
        {
            gameObject.SetActive(false);
            mainmenu.gameObject.SetActive(true);
        }
        else
        {
            introSound = gameObject.GetComponent<AudioSource>();
            StartCoroutine(SoundStop());
        }

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.anyKeyDown)
        {
            gameObject.SetActive(false);
            mainmenu.gameObject.SetActive(true);
        }

    }

    private IEnumerator SoundStop()
    {
        //yield return new WaitForSeconds(4.6f);
        gameObject.SetActive(true);
        introSound.Play();
        yield return new WaitForSeconds(23f);
        introSound.Stop();
        gameObject.SetActive(false);
        mainmenu.gameObject.SetActive(true);
    }
}
