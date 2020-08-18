using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu (fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    public float moveSpeed = 6f;
    public float jumpHeight = 5f;
    public float dashDis = 3f;
    public int MaxAirJump = 3;
    public int maxDash = 2;
    public bool isJumpPressed;
    public bool isDashPressed;
    public bool isCtrlPressed;
    public int jumpCount;
    public int lastMoveDir;
    public float maxStamina = 50f;
    public float stamina = 50f;
    public float staminaRegen = 5f;
    public float jumpCost = 10f;
    public float airJumpCost = 15f;
    public float dashCost = 10f;
    public float wallStickCost = 2f;
    public Vector2 movement;
}
