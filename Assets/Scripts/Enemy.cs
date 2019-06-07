using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject Spell;
    [SerializeField]
    private double CastTime=3.0;
    [SerializeField]
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

  

   


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
            if (!CanAttack)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }
    }

    void Attack()
    {
        if (CanAttack)
        {
            startattack = true;
            Transform SpellTarget = target;
            if (CastDuration < CastTime)
            {
                CastDuration += Time.deltaTime;
            }
            else
            {
                Instantiate(Spell, SpellTarget.position, SpellTarget.rotation);
                CastDuration = 0.0;
            }

        }
    }


    void Animation()
    {
        anim.SetBool("CanAttack", CanAttack);
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
