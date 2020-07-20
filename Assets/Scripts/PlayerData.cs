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
    public bool isJumpPressed;
    public bool isDashPressed;
    public bool isCtrlPressed;
    public int jumpCount;
    public int lastMoveDir;
    public Vector2 movement;
}
