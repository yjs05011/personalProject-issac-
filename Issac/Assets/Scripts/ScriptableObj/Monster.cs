using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Monster", menuName = "Monster/new Monster", order = 0)]
public class Monster : ScriptableObject
{
    public int id;
    public string name;
    public float maxHp;
    public float shotSpeed;
    public float rateSpeed;
    public float speed;

    public bool die;


}
