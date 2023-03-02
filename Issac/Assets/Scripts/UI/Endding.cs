using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class Endding : MonoBehaviour
{
    // Start is called before the first frame update
    public VideoPlayer video;
    void Start()
    {
        video = transform.GetComponent<VideoPlayer>();
        video.url = Application.streamingAssetsPath + "/the_void_ending.ogv";
        transform.GetComponent<AudioSource>().volume = SoundManager.instance.music / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            transform.GetComponent<VideoPlayer>().Stop();
            GFunc.SceneChanger("MainMenu");
            Time.timeScale = 1f;

        }
        if (transform.GetComponent<VideoPlayer>().time >= transform.GetComponent<VideoPlayer>().length)
        {
            GFunc.SceneChanger("MainMenu");
            Time.timeScale = 1f;
        }

    }
}
