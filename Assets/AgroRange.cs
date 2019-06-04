using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgroRange : MonoBehaviour
{
    private Enemy parent;


    private void Start()
    {
        parent = GetComponentInParent<Enemy>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            parent.Target = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            parent.Target = null;
        }
    }
}
