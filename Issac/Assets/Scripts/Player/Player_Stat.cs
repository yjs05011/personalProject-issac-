using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stat : MonoBehaviour
{
    private int id;
    private string name;
    private float maxHp;
    private float normalHeart;
    private float soulHeart;
    private float str;
    private float shotSpeed;
    private float rateSpeed;
    private float speed;
    private float luck;
    private float range;
    private bool die;
    private int keyCount;
    private int boomCount;
    private int coinCount;
    public Stack<int> item = new Stack<int>();


    public float MaxHp
    {
        get; set;
    }
    public int ID
    {
        get; set;
    }
    public string Name
    {
        get; set;
    }
    public float NormalHeart
    {
        get; set;

    }
    public float SoulHeart
    {
        get; set;

    }
    public float Str
    {
        get; set;

    }
    public float ShotSpeed
    {
        get; set;

    }

    public float RateSpeed
    {
        get; set;

    }
    public float Speed
    {
        get; set;

    }
    public float Luck
    {
        get; set;

    }
    public float Range
    {
        get; set;

    }


    public bool Die
    {
        get; set;
    }
    public int KeyCount
    {
        get; set;
    }
    public int BoomCount
    {
        get; set;

    }
    public int CoinCount
    {
        get; set;
    }


    public Player_Stat()
    {

    }
    public Player_Stat(int id, string name, float normalHeart, float soulHeart, float str,
                    float shotSpeed, float rateSpeed, float speed, float luck, float range, bool die)
    {
        this.id = id;
        this.name = name;
        this.normalHeart = normalHeart;
        this.soulHeart = soulHeart;
        this.str = str;
        this.shotSpeed = shotSpeed;
        this.rateSpeed = rateSpeed;
        this.speed = speed;
        this.luck = luck;
        this.range = range;
        this.die = die;
    }
}
