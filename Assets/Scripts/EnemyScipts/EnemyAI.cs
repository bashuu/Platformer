using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        patroling
    }

    public bool checkGround;

    private void Awake()
    {
        state = State.patroling;
        checkGround = transform.Find("GroundCheck").GetComponent<GroundCheck>();
    }

    private State state;
    private void Update()
    {
        switch (state){
            default:
            case State.patroling:
                if (checkGround.isGrounded)
                {

                }
                break;
        }
        
    }
}
