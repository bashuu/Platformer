using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Rigidbody2D rb;


    private bool isJumpPressed;
    private bool isDashPressed;
    private bool isCtrlPressed;
    private int jumpCount;
    int lastMoveDir = 1;
    [SerializeField] 
    private PlayerData playerData;
    public LayerMask platformLayerMask;
    public Vector2 movement;    

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
    }   

    private void Update()
    {
        changeDir(lastMoveDir);

        handleMovementInput();

    }

    private void FixedUpdate()
    {
        handleWalljump();
        handleJump();
        handleDash(lastMoveDir);

        rb.velocity = movement * playerData.moveSpeed + new Vector2(0.0f, rb.velocity.y);
    }

    bool isGrounded()
    {
        return transform.Find("GroundCheck").GetComponent<GroundCheck>().isGrounded;
    }

    bool onWall()
    {
        if (transform.Find("WallCheck").GetComponent<WallCheck>().onWall && !isGrounded())
        {
            return true;
        }
        return false;
    }

    void dash(float distance)
    {
        transform.position += new Vector3(lastMoveDir, 0, 0) * distance;
    }

    void jump()
    {
        rb.velocity = Vector2.up * playerData.jumpHeight;
        jumpCount--;
        
    }

    private void handleMovementInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            movement.x = -1;
            lastMoveDir = -1;
        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                movement.x = 1;
                lastMoveDir = 1;
            }
            else
            {

                movement.x = 0;
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }

        if (!isGrounded())
        {
            isJumpPressed = Input.GetKeyDown(KeyCode.Space);
        }
        else
            isJumpPressed = Input.GetKey(KeyCode.Space);

        isDashPressed = Input.GetKeyDown(KeyCode.LeftShift);


        isCtrlPressed = Input.GetKey(KeyCode.LeftControl);
        if (Input.GetKeyUp(KeyCode.LeftControl))
            isCtrlPressed = false;
    }

    private void handleJump()
    {

        if(isGrounded())
            jumpCount = playerData.MaxAirJump;
        if (isJumpPressed && jumpCount > 0)
        {
            jump();
        }
    }
    private void handleWalljump()
    {

        if(onWall())
        {
            bool facingWall = Physics2D.Raycast(transform.position, new Vector2(lastMoveDir, 0), 1f, platformLayerMask).collider;
            if (facingWall)
                movement.x = 0;
            if (isCtrlPressed)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePosition;

                if (isJumpPressed && !facingWall && movement.x != 0)
                {
                    rb.constraints = RigidbodyConstraints2D.None;
                    jump();
                }
            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.None;
            }
        }
    }
    private void handleDash(int dir)
    {
        
        if (isDashPressed)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(dir, 0), playerData.dashDis, platformLayerMask);
            if (hit.collider != null)
            {
                float distance = Mathf.Abs(hit.point.x - transform.position.x);
                dash(distance - 0.2f);
                return;
            }            
            dash(playerData.dashDis);

        }

    }

    void changeDir(int lastMoveDir)
    {
        if (lastMoveDir == -1)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }


}
