using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float jumpforce;
    [SerializeField]
    private float speed;


    private bool facingR=true;
    [SerializeField]
    private LayerMask groundLayer;
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
        float HorizontAxis = Input.GetAxis("Horizontal");
        float VerticalAxis = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(speed * HorizontAxis, rb.velocity.y);
        Flip(HorizontAxis);
        if (VerticalAxis==1)
        {
            Jump();
        }
       
    }

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;
        Debug.DrawRay(position, direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
    void Jump()
    {
        if (!IsGrounded())
        {
            return;
        }
        else
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpforce));
        }
       
    }

    void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingR || horizontal <0 &&facingR)
        {
            facingR = !facingR;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
  
}
