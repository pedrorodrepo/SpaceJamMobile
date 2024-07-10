using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health = 100f;
    [SerializeField] protected Rigidbody2D rb;

    void Start()
    {
        
    }

    public void TakeDamage(float damage)    {
        health -= damage;

        HurtSequence();
        if (health <= 0)
        {
            DieSequence();
        }
    }

    public virtual void HurtSequence()
    {
        // do something when hurt
    }

    public virtual void DieSequence()
    {
        // do something when die
    }
}
