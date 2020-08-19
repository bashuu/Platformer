using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.UIElements;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] 
    private PlayerData playerData;
    public LayerMask platformLayerMask;
    public StaminaBar staminaBar;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
    }

    private void Start()
    {
        playerData.stamina = playerData.maxStamina;
        staminaBar.setMaxStamina(playerData.maxStamina);
    }

    private void Update()
    {
        changeDir(playerData.lastMoveDir);

        handleMovementInput();
        staminaBar.setStamina(playerData.stamina);
    }

    public bool onEnemy()
    {
        return transform.Find("GroundCheck").GetComponent<GroundCheck>().onEnemy;
    }
    public bool isGrounded()
    {
        return transform.Find("GroundCheck").GetComponent<GroundCheck>().isGrounded;
    }

    public bool onWall()
    {
        if (transform.Find("WallCheck").GetComponent<WallCheck>().onWall && !isGrounded())
        {
            return true;
        }
        return false;
    }

    public bool facingWall()
    {
        if(Physics2D.Raycast(transform.position, new Vector2(playerData.lastMoveDir, 0), 0.3f, platformLayerMask))
        {
            if(!isGrounded())
                playerData.movement.x = 0;
            return true;
        }
        return false;
    }

    public void dash(float distance)
    {
        playerData.stamina -= playerData.dashCost;
        transform.position += new Vector3(playerData.lastMoveDir, 0, 0) * distance;
    }

    public void jump()
    {
        rb.velocity = Vector2.up * playerData.jumpHeight;
        playerData.stamina -= playerData.jumpCost;
    }

    public void airJump()
    {
        rb.velocity = Vector2.up * playerData.jumpHeight;
        playerData.stamina -= playerData.airJumpCost;
    }

    public void wallJump()
    {
        rb.velocity = new Vector2(playerData.lastMoveDir * playerData.moveSpeed, 1 * playerData.jumpHeight);
    }


    public void changeDir(int lastMoveDir)
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

    private void handleMovementInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            playerData.movement.x = -1;
            playerData.lastMoveDir = -1;
        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                playerData.movement.x = 1;
                playerData.lastMoveDir = 1;
            }
            else
            {

                playerData.movement.x = 0;
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }


        playerData.isJumpPressed = Input.GetKeyDown(KeyCode.Space);

        playerData.isDashPressed = Input.GetKeyDown(KeyCode.LeftShift);


        playerData.isCtrlPressed = Input.GetKey(KeyCode.LeftControl);
    }
}
