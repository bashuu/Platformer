using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{

    public bool onWall;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platfrom")
        {
            onWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platfrom")
        {
            onWall = false;
        }
    }
}
