using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private Rigidbody2D itemRigid;
    private RectTransform itemRect;
    // Start is called before the first frame update
    void Start()
    {
        itemRect = GetComponent<RectTransform>();
        itemRigid = GetComponent<Rigidbody2D>();
        StartCoroutine(ItemDropEffect());

    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator ItemDropEffect()
    {
        itemRect.localScale = new Vector3(3f, 3f, 1);
        yield return new WaitForSeconds(0.1f);
        itemRigid.velocity = Vector2.up * 5;
        itemRect.localScale = new Vector3(2.5f, 2.5f, 1);
        yield return new WaitForSeconds(0.1f);
        itemRigid.velocity = Vector2.zero;
        itemRect.localScale = new Vector3(3.5f, 3.5f, 1);
        yield return new WaitForSeconds(0.1f);
        itemRigid.velocity = Vector2.down * 5;
        itemRect.localScale = new Vector3(3.5f, 1.5f, 1);
        yield return new WaitForSeconds(0.1f);
        itemRigid.velocity = Vector2.zero;
        itemRect.localScale = new Vector3(3f, 3f, 1);
    }
}
