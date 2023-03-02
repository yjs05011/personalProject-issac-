using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Active : MonoBehaviour
{
    [SerializeField]
    public Monster stat;

    public float maxHp = default;

    public int monsterType = default;
    public SpriteRenderer monsterRenderer;
    // Start is called before the first frame update
    public virtual void OnEnable()
    {
        monsterRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Direction(Collider2D Player, float x, float y)
    {
        Vector2 direction = new Vector2(Player.transform.position.x - x, Player.transform.position.y - y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 270f, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(Player.transform.rotation, angleAxis, 1);
        Player.transform.rotation = rotation;
    }

    public virtual void HitThisMonster(float damage)
    {
        /* Do nothing */


    }
}
