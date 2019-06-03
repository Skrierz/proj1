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

    private Animator anim;

    [SerializeField]
    private bool isGrounded;

    private bool facingR = true;

    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        RunAnimation();


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

            transform.Rotate(0f, 180f, 0f);
        }
    }

    void RunAnimation()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 && v == 0)
            anim.SetBool("IsRun", true);
        else
            anim.SetBool("IsRun", false);
        anim.SetBool("IsGrounded", isGrounded);
    }
  
}
