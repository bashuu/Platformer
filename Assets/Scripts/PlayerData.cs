using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu (fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    public float moveSpeed = 6f;
    public float jumpHeight = 5f;
    public float dashDis = 3f;
    public int lastMoveDir = 1;
    public int MaxAirJump = 3;
}
