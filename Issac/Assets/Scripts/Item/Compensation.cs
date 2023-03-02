using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compensation : MonoBehaviour
{

    //Item List [coin, nikkel, oneboom, twoboom, oneKey, twoKey, nomalHeart, soulHeart]
    private PickUpItem[] ItemList;
    private int randomDropItemNum;
    private bool isDrop;
    // Start is called before the first frame update
    void Start()
    {
        ItemList = new PickUpItem[8];
        for (int i = 0; i < 8; i++)
        {
            ItemList[i] = gameObject.transform.GetChild(i).GetComponent<PickUpItem>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.monsterClearChk && !isDrop)
        {
            isDrop = true;
            DropPickUp();
        }
    }
    void DropPickUp()
    {
        int randomDropItemNum = Random.Range(0, 31);
        if (randomDropItemNum < 10)
        {
            ItemList[0].transform.gameObject.SetActive(true);
        }
        else if (randomDropItemNum >= 10 && randomDropItemNum < 12)
        {
            ItemList[1].transform.gameObject.SetActive(true);
        }
        else if (randomDropItemNum >= 12 && randomDropItemNum < 16)
        {
            ItemList[2].transform.gameObject.SetActive(true);
        }
        else if (randomDropItemNum == 16)
        {
            ItemList[3].transform.gameObject.SetActive(true);
        }
        else if (randomDropItemNum >= 17 && randomDropItemNum < 21)
        {
            ItemList[4].transform.gameObject.SetActive(true);
        }
        else if (randomDropItemNum == 21)
        {
            ItemList[5].transform.gameObject.SetActive(true);
        }
        else if (randomDropItemNum >= 22 && randomDropItemNum < 30)
        {
            ItemList[6].transform.gameObject.SetActive(true);
        }
        else if (randomDropItemNum == 30)
        {
            ItemList[7].transform.gameObject.SetActive(true);
        }



    }
}
