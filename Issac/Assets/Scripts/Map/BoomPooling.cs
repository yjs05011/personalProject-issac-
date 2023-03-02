using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomPooling : MonoBehaviour
{

    public static BoomPooling instance = null;
    // Start is called before the first frame update
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
