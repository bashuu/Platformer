using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogics : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private PlayerData playerData;
    public LayerMask platformLayerMask;


    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        handleWalljump();
        handleJump();
        handleDash(playerData.lastMoveDir);

        rb.velocity = playerData.movement * playerData.moveSpeed + new Vector2(0.0f, rb.velocity.y);
    }


    private void handleJump()
    {

        if (this.GetComponent<Player>().isGrounded())
            playerData.jumpCount = playerData.MaxAirJump;

        if (playerData.isJumpPressed && playerData.jumpCount > 0)
        {
            if (!this.GetComponent<Player>().onWall())
                this.GetComponent<Player>().jump();
        }
    }
    private void handleWalljump()
    {

        if (playerData.isCtrlPressed)
        {
            if (this.GetComponent<Player>().onWall())
            {
                rb.constraints = RigidbodyConstraints2D.FreezePosition;

                if (playerData.isJumpPressed && !this.GetComponent<Player>().facingWall())
                {
                    rb.constraints = RigidbodyConstraints2D.None;
                    this.GetComponent<Player>().jump();
                }


            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.None;
            }
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }
    private void handleDash(int dir)
    {

        if (playerData.isDashPressed)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(dir, 0), playerData.dashDis, platformLayerMask);
            if (hit.collider != null)
            {
                float distance = Mathf.Abs(hit.point.x - transform.position.x);
                this.GetComponent<Player>().dash(distance - 0.2f);
                return;
            }
            this.GetComponent<Player>().dash(playerData.dashDis);

        }

    }

    private void handleCombat()
    {

    }
}
