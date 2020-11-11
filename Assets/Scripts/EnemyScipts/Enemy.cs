using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 movement;
    public int moveDir;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed + new Vector2(0.0f, rb.velocity.y);
    }
    public void killEnemy()
    {
        Debug.Log("ded");
        Destroy(gameObject);
    }
    
}
