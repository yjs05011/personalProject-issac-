using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Charator", menuName = "Charator/new Charator", order = 0)]
public class Charator : ScriptableObject
{
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
    public bool die;
    public int keyCount;
    public int boomCount;
    public int coinCount;
    public Stack<int> item;
}
