using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private void Awake()
    {
    }


    public void killEnemy()
    {
        Debug.Log("ded");
        Destroy(gameObject);
    }
    
}
