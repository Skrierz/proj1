using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    [Range(1, 10)]
    private float jumpForce;

    [SerializeField]
    private float speed;

    [SerializeField]
    private LayerMask ground;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float fallMultiplier;

    [SerializeField]
    private float lowJumpMultiplier;

    private bool isGrounded;

    private bool facingR = true;

    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float horizontAxis = Input.GetAxis("Horizontal");
        isGrounded = IsGrounded();

        Movement(horizontAxis);

        EnchancedJump();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
            isGrounded = false;
        }        
    }

    private void Movement(float horizontal)
    {
        rb.velocity = new Vector2(speed * horizontal, rb.velocity.y);
        Flip(horizontal);

    }

    private void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
    }

    private void EnchancedJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

    }

    bool IsGrounded()
    {
        if (rb.velocity.y == 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, ground);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
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
