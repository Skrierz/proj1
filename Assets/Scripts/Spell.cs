using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    private AnimatorStateInfo anim;
    private double CastDuration=0.0;
    [SerializeField]
    private int damgae;
    [SerializeField]
    private double CastTime = 0.417;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collapse();
    }


    private void Collapse()
    {
        if (CastDuration < CastTime)
        {
            CastDuration += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerMove player = collision.gameObject.GetComponent<PlayerMove>();
            player.TakeDamage(damgae);
        }
    }
}
