using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCreater : MonoBehaviour
{
    public GameObject[] bossList;
    private void OnEnable()
    {


    }

    // Start is called before the first frame update
    void Start()
    {
        CreateBoss();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void CreateBoss()
    {
        GameObject newBoss = Instantiate(bossList[0]);
        newBoss.transform.SetParent(transform.GetChild(0), false);
        newBoss.SetActive(true);

    }
}
