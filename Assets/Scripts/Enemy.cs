using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private HealthStat health;

    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject Spell;
    [SerializeField]
    private bool isInterrupt;
    [SerializeField]
    private float InterruptValue;
    [SerializeField]
    private double CastTime=3.0;
    private double CastDuration = 0.0;

    private Transform target;

    public Transform Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
        }
    }
    private Animator anim;

    private bool facingR = true;
    private bool startattack = false;
    private bool CanAttack=false;
    public bool CanAttack1 { get => CanAttack; set => CanAttack = value; }
    private Vector3 AttackTarget;
    public Vector3 AttackTarget1 { get => AttackTarget; set => AttackTarget = value; }
    private Canvas healtCanvas;




    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        health.Initrialize();
        healtCanvas = transform.GetComponentInChildren<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
        Attack();
        flip();
        Animation();
    }


    private void FollowTarget()
    {
        if(target !=null)
        {
            if (!CanAttack && !startattack)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }
    }

    void Attack()
    {
        if (CanAttack || startattack) 
        {
            startattack = true;
            Vector3 SpellTarget = AttackTarget;
            if (CastDuration < CastTime)
            {
                CastDuration += Time.deltaTime;
            }
            else
            {
                var rotation = Quaternion.Euler(0, 0, 0);
                Instantiate(Spell, SpellTarget, rotation);
                CastDuration = 0.0;
                startattack = false;
            }

        }
    }
    public void TakeDamage(int damage)
    {
        if (isInterrupt)
        {
            CastDuration -= InterruptValue;
            if (CastDuration<0.0)
            {
                CastDuration = 0.0;
            }
        }
        if (!healtCanvas.isActiveAndEnabled)
        {
            healtCanvas.enabled = true;
        }
        health.CurrentVal -= damage;

        if (health.CurrentVal<=0)
        {
            Destroy(gameObject);
        }
    }


    void Animation()
    {
        anim.SetBool("CanAttack", startattack);
    }

    void flip()
    {
        if (target!=null)
        if ( target.position.x<gameObject.transform.position.x && facingR || target.position.x > gameObject.transform.position.x && !facingR)
        {
            facingR = !facingR;

            transform.Rotate(0f, 180f, 0f);
        }
    }
}
