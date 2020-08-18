using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    ParticleSystem deathPartical;

    private void Awake()
    {
    }

    public void onDeath()
    {
        Debug.Log("dead");
        
    }
    
}
