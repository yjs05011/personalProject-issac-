using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class GFunc
{

    public static void SetVelocity(Rigidbody2D rigid, float X, float Y)
    {
        rigid.velocity = new Vector2(X, Y);
    }
    public static Vector2 GetVelocity(Rigidbody2D rigid)
    {
        Vector2 getVector = new Vector2(rigid.velocity.x, rigid.velocity.y);

        return getVector;
    }

    public static GameObject GetChiled(this Transform _obj, int index)
    {
        return _obj.transform.GetChild(index).GetComponent<GameObject>();
    }
    public static RectTransform GetRect(this GameObject _obj)
    {
        return _obj.GetComponent<RectTransform>();
    }
    public static BoxCollider2D GetBoxCollider(this GameObject _obj)
    {
        return _obj.GetComponent<BoxCollider2D>();
    }
    public static Rigidbody2D GetRigid(this GameObject _obj)
    {
        return _obj.GetComponent<Rigidbody2D>();
    }
    public static void SetRotation(this RectTransform nowRect, Vector3 eulerAngle)
    {


        nowRect.rotation = Quaternion.Euler(eulerAngle);

    }
    public static void SetRotation(this RectTransform nowRect, float X, float Y, float Z)
    {


        nowRect.rotation = Quaternion.Euler(new Vector3(X, Y, Z));

    }
    public static void Direction(RectTransform Player, float x, float y)
    {
        Vector2 direction = new Vector2(Player.position.x - x, Player.position.y - y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 270f, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(Player.rotation, angleAxis, 1);
        Player.rotation = rotation;
    }
    public static IEnumerator PlayerHit(Collider2D other, int i, string sfxName, AudioClip clip)
    {
        other.transform.GetComponent<Player_Active>().playerAct.sprite = other.transform.GetComponent<Player_Active>().PlayerActSprite[i];
        SoundManager.instance.SfxPlay(sfxName, clip, SoundManager.instance.sfx / 100f);
        other.transform.GetChild(1).gameObject.SetActive(true);
        other.transform.GetChild(4).gameObject.SetActive(false);
        other.transform.GetChild(3).gameObject.SetActive(false);
        for (int j = 0; j < 5; j++)
        {
            other.transform.GetComponent<Player_Active>().playerAct.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.1f);
            other.transform.GetComponent<Player_Active>().playerAct.color = new Color(1, 1, 1, 1f);
            yield return new WaitForSeconds(0.1f);
        }
        if (GameManager.instance.player_Stat.Die)
        {

        }
        else
        {
            other.transform.GetChild(1).gameObject.SetActive(false);
            other.transform.GetChild(4).gameObject.SetActive(true);
            other.transform.GetChild(3).gameObject.SetActive(true);
        }


    }
    public static IEnumerator MonsterHit(Collider2D other)
    {
        other.transform.GetComponent<Monster_Active>().monsterRenderer.color = new Color(1, 0, 0, 1f);
        yield return new WaitForSeconds(0.1f);
        other.transform.GetComponent<Monster_Active>().monsterRenderer.color = new Color(1, 1, 1, 1f);
    }
    public static void PlayerStatReset(int Id)
    {

        GameManager.instance.player_Stat.ID = GameManager.instance.player[Id].id;
        GameManager.instance.player_Stat.Name = GameManager.instance.player[Id].name;
        GameManager.instance.player_Stat.MaxHp = GameManager.instance.player[Id].maxHp;
        GameManager.instance.player_Stat.NormalHeart = GameManager.instance.player[Id].normalHeart;
        GameManager.instance.player_Stat.SoulHeart = GameManager.instance.player[Id].soulHeart;
        GameManager.instance.player_Stat.Str = GameManager.instance.player[Id].str;
        GameManager.instance.player_Stat.ShotSpeed = GameManager.instance.player[Id].shotSpeed;
        GameManager.instance.player_Stat.RateSpeed = GameManager.instance.player[Id].rateSpeed;
        GameManager.instance.player_Stat.Speed = GameManager.instance.player[Id].speed;
        GameManager.instance.player_Stat.Luck = GameManager.instance.player[Id].luck;
        GameManager.instance.player_Stat.Range = GameManager.instance.player[Id].range;
        GameManager.instance.player_Stat.Die = GameManager.instance.player[Id].die;
        GameManager.instance.player_Stat.KeyCount = GameManager.instance.player[Id].keyCount;
        GameManager.instance.player_Stat.BoomCount = GameManager.instance.player[Id].boomCount;
        GameManager.instance.player_Stat.CoinCount = GameManager.instance.player[Id].coinCount;
        GameManager.instance.SceneChanger = true;
        GameManager.instance.monsterCount = 0;
    }

}