using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public int enemiesKilled = 0;
    public TextMeshProUGUI enemiesKilledCountText;

   
    Animator animator;
    bool isDead;

    void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    void Update()
    {
        DeathTransition();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            animator.SetBool("isDead", true);
            isDead = true;
        }
    }

    void DeathTransition()
    {
        if(isDead == true)
        {
            Invoke("ChangeBool", 0.25f);
        }
    }

    void ChangeBool()
    {
        isDead = false;
        health = 50f; 
        animator.SetBool("isTransitioning", true);
        animator.SetBool("isDead", false);
    }
}
