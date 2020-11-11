﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerLogics : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private PlayerData playerData;
    public LayerMask platformLayerMask;
    public LayerMask enemyLayerMask;
    public bool onGround;
    public bool onWall;
    public Player player;


    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Debug.Log(playerData.stamina);
        onGround = this.GetComponent<Player>().isGrounded();
        onWall = this.GetComponent<Player>().onWall();
        player = this.GetComponent<Player>();
    }
    private void FixedUpdate()
    {
        handleDash(playerData.lastMoveDir);
        handleWalljump();
        handleJump();
        handleStamina();

        rb.velocity = playerData.movement * playerData.moveSpeed + new Vector2(0.0f, rb.velocity.y);
    }

    bool checkIdle()
    {
        if (playerData.movement.x == 0 && onGround)
            return true;
        return false;
    }
    private void handleJump()
    {

        if (onGround)
        {
            if (playerData.isJumpPressed && playerData.stamina >= playerData.jumpCost)
            {
                if (!onWall)
                    player.jump();
            }
            return;
        }
        else
        {
            if (playerData.isJumpPressed && playerData.stamina >= playerData.airJumpCost)
            {
                if (!onWall)
                    player.airJump();
            }
        }

    }
    private void handleWalljump()
    {

        if (playerData.isCtrlPressed && playerData.stamina > 0)
        {
            if (onWall)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePosition;

                if (playerData.isJumpPressed && !this.GetComponent<Player>().facingWall() && playerData.stamina >= playerData.jumpCost)
                {
                    rb.constraints = RigidbodyConstraints2D.None;
                    player.jump();
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

        if (playerData.isDashPressed && playerData.stamina >= playerData.dashCost)
        {
            RaycastHit2D hitPlatform = Physics2D.Raycast(transform.position, new Vector2(dir, 0), playerData.dashDis, platformLayerMask);
            RaycastHit2D hitEnemy = Physics2D.Raycast(transform.position, new Vector2(dir, 0), playerData.dashDis, enemyLayerMask);

            if (hitPlatform.collider != null)
            {
                float distance = Mathf.Abs(hitPlatform.point.x - transform.position.x);
                this.GetComponent<Player>().dash(distance - 0.2f);
                return;
            }

            if (hitEnemy.collider != null)
            {
                Debug.Log("Kill");
                float distance = Mathf.Abs(hitEnemy.point.x - transform.position.x);
                this.GetComponent<Player>().dash(distance);
                hitEnemy.collider.gameObject.GetComponent<Enemy>().onDeath();
                Destroy(hitEnemy.collider.gameObject, 1f);

                return;
            }

            this.GetComponent<Player>().dash(playerData.dashDis);
        }

    }
       
    private void handleStamina()
    {
        if(playerData.stamina < playerData.maxStamina)
        {
            if (onWall)
            {
                playerData.stamina -= playerData.wallStickCost * Time.deltaTime;
            }
            else if(checkIdle())
            {
                playerData.stamina += playerData.idleStaminaRegen * Time.deltaTime;
            }
            else
            {
                playerData.stamina += playerData.staminaRegen * Time.deltaTime;
            }
        }
    }
}