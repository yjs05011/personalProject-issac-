using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    // Start is called before the first frame update

    public int id;
    public string name;
    public float maxHp;
    public float normalHeart;
    public float soulHeart;
    public float str;
    public float shotSpeed;
    public float rateSpeed;
    public float speed;
    public float luck;
    public float range;
    

    // Update is called once per frame

}
