using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMaker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GFunc.SceneChanger("MainMenu");
        ItemDuplication(4);
        GFunc.PlayerStatReset(GameManager.instance.choiceCharator);

    }

    // Update is called once per frame
    void Update()
    {

    }


    void ItemDuplication(int totalItem)
    {
        for (int i = 0; i < totalItem; i++)
            GameManager.instance.totalItem.Add(i);
    }
}
