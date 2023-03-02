using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMoving : MonoBehaviour
{
    bool isMoving;
    public bool isActve;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(MovingTitle());
        }
        if (isActve)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 600f);
        }
    }
    IEnumerator MovingTitle()
    {
        transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.down * 2f;
        yield return new WaitForSeconds(0.4f);
        transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(0.4f);
        transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * 2f;
        yield return new WaitForSeconds(0.4f);
        transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(0.4f);
        isMoving = false;
    }
}
