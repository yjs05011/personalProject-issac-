using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCreate : MonoBehaviour
{
    public Canvas ending;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.enddingVideo = ending;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
