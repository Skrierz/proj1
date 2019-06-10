using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    private Enemy parent;
    private bool InRange=false;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            parent.CanAttack1 = true;
            InRange = true;
            parent.AttackTarget1 = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y, 0.0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            InRange = false;
            parent.CanAttack1 = false;
            parent.AttackTarget1 = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y,0.0f);
        }
    }

    private void UpdatePosition()
    {

    }
}
