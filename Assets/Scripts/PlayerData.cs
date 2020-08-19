using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu (fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    public float moveSpeed = 6f;
    public float jumpHeight = 5f;
    public float dashDis = 3f;
    public bool isJumpPressed;
    public bool isDashPressed;
    public bool isCtrlPressed;
    public int jumpCount;
    public int lastMoveDir;
    public float maxStamina = 50f;
    public float stamina = 50f;
    public float staminaRegen = 01f;
    public float idleStaminaRegen = 20;
    public float jumpCost = 10f;
    public float airJumpCost = 15f;
    public float dashCost = 10f;
    public float wallStickCost = 2f;
    public Vector2 movement;
}
