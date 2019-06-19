using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private Transform StartPoint;
    [SerializeField]
    private HealthStat health;

    [SerializeField]
    [Range(1, 10)]
    private float jumpForce;

    [SerializeField]
    private float speed;
    [SerializeField]
    private int damage;
    [SerializeField]
    private int DashDistance;
    [SerializeField]
    private float speedAttack;
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

    [SerializeField]
    private BoxCollider2D meleeAttack;

    private Animator anim;

    private bool isGrounded;
    public bool IsGrounded1 { get => isGrounded; set => isGrounded = value; }


    private bool facingR = true;

    Rigidbody2D rb;



    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health.Initrialize();
    }

    private void Update()
    {
       
//        IsGrounded1 = IsGrounded();
        Movement();
        EnchancedJump();
        isDead();
        if (Input.GetButtonDown("Dash"))
        {
            Dash();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
        if (Input.GetButtonDown("Jump") && IsGrounded1)
        {
            Jump();
            IsGrounded1 = false;
        }
        RunAnimation();


    }
    /*
    private void FixedUpdate()
    {
        float horizontAxis = Input.GetAxis("Horizontal");
        isGrounded = IsGrounded();
        Movement(horizontAxis);
        EnchancedJump();
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        } 
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
            isGrounded = false;
        }
        RunAnimation();


    }*/

    private void Movement()
    {
        float horizontAxis = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * horizontAxis, rb.velocity.y);
        Flip(horizontAxis);

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
    private void Attack()
    {
        Debug.Log("attack");
        anim.SetTrigger("IsAttack");
        MeleeAttack();
        if (!isGrounded)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce * 0.1f;
        }
    }
    public void MeleeAttack()
    {
        meleeAttack.enabled = !meleeAttack.enabled;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }

    }
    public void TakeDamage(int damage)
    {
        health.CurrentVal -= damage;
    }

    private void isDead()
    {
        if (health.CurrentVal==0)
        {
            gameObject.transform.position = StartPoint.position;
            health.CurrentVal = health.MaxVal;
        }
    }
    /*
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
    }*/

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingR || horizontal <0 &&facingR)
        {
            facingR = !facingR;

            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void Dash()
    {
        int DashSpeed=50;
        if (facingR)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector3(transform.position.x + DashDistance, transform.position.y, transform.position.z),Time.deltaTime* DashSpeed);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector3(transform.position.x - DashDistance, transform.position.y, transform.position.z),Time.deltaTime*DashSpeed);
        }

       
    }

    private void RunAnimation()
    {
        float h = Input.GetAxis("Horizontal");
        if (h != 0 && isGrounded)
            anim.SetBool("IsRun", true);
        else
            anim.SetBool("IsRun", false);
        anim.SetBool("IsGrounded", IsGrounded1);

    }

    
}
