using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    public bool isGrounded;
    public bool onEnemy;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platfrom")
        {
            isGrounded = true;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            onEnemy = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platfrom")
        {
            isGrounded = false;
        }
    }
}
