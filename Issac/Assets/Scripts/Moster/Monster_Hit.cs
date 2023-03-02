using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Hit : MonoBehaviour
{
    private BoxCollider2D hitBox;
    private Monster_Active monster;
    // Start is called before the first frame update
    void Start()
    {
        hitBox = GetComponent<BoxCollider2D>();
        monster = transform.parent.GetComponent<Monster_Active>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D other)
    {

    }
}
