using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float jumpforce;
    public float speed;

   public bool isjump=false;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
   
    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * move, rb.velocity.y);
        Jump();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isjump = false;
            rb.velocity = Vector2.zero;
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isjump)
        {
            isjump = true;
            rb.AddForce(new Vector2(rb.velocity.x, jumpforce));
        }
    }
  
}
