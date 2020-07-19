using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public bool onWall;
    public bool isGrounded;
    public GameObject groundCheck;

    private void Awake()
    {
        isGrounded = groundCheck.GetComponent<GroundCheck>().isGrounded;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platfrom")
        {
            onWall = true;
            return;
        }
        onWall = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platfrom")
        {
            onWall = false;
        }
    }
}
