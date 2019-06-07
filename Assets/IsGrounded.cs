using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{

   private  PlayerMove parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<PlayerMove>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            parent.IsGrounded1 = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            parent.IsGrounded1 = false;
    }
}
