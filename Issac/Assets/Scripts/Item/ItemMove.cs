using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    private bool turn = false;

    private Rigidbody2D item;
    // Start is called before the first frame update
    void Start()
    {
        item = gameObject.GetRigid();
    }

    // Update is called once per frame
    void Update()
    {
        if(!turn){
            turn = true;
            StartCoroutine("ItemMoving");
        }
        
    }
    IEnumerator ItemMoving(){
        item.velocity = new Vector2(0,0.3f);
        yield return new WaitForSeconds(1f);
        item.velocity = new Vector2(0,-0.3f);
        yield return new WaitForSeconds(1f);
        turn = false;

    }
}
