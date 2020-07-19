using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Rigidbody2D rb;

    public float moveSpeed = 5f;
    public float jumpHeight = 8f;
    public float fallMult = 0.005f;
    public float dashDis = 3f;

    [SerializeField] public LayerMask platformLayerMask;

    private const int MaxAirJump = 3;

    private bool isJumpPressed;
    private bool isDashPressed;
    private int jumpCount;

    public Vector2 movement;
    private int lastMoveDir = 1;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
    }

    private void Update()
    {
        changeDir(lastMoveDir);
        handleMovementInput();

        isJumpPressed = Input.GetKeyDown(KeyCode.Space);
    }

    private void FixedUpdate()
    {

        handleJump();
        handleDash(lastMoveDir);

        rb.velocity = movement * moveSpeed + new Vector2(0.0f, rb.velocity.y);
    }

    bool isGrounded()
    {
        return transform.Find("GroundCheck").GetComponent<GroundCheck>().isGrounded;
    }

    bool onWall()
    {
        return transform.Find("WallCheck").GetComponent<WallCheck>().onWall;
    }

    void dash(float distance)
    {
        transform.position += new Vector3(lastMoveDir, 0, 0) * distance;
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

        isJumpPressed = Input.GetKeyDown(KeyCode.Space);
        isDashPressed = Input.GetKeyDown(KeyCode.LeftShift);
    }

    private void handleJump()
    {
        if(isGrounded())
            isJumpPressed = Input.GetKey(KeyCode.Space);
        else
            isJumpPressed = Input.GetKeyDown(KeyCode.Space);
        if(isGrounded())
            jumpCount = MaxAirJump;

        if (isJumpPressed && jumpCount > 0)
        {
            rb.velocity = Vector2.up * jumpHeight;
            jumpCount--;
        }

        /*if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMult - 1) * Time.deltaTime;
        }
        */
    }
    
    private void handleDash(int dir)
    {
        
        if (isDashPressed)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(dir, 0), dashDis, platformLayerMask);
            if (hit.collider != null)
            {
                float distance = Mathf.Abs(hit.point.x - transform.position.x);
                dash(distance - 0.2f);
                return;
            }            
            dash(dashDis);

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
