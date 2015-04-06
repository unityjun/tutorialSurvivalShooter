﻿using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
	public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    Animator anim;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;
	GameObject player;


    void Awake ()
    {
		player = GameObject.FindWithTag ("PlayerNav");
        
		playerHealth = player.GetComponent<PlayerHealth>();
		enemyHealth = GetComponent<EnemyHealth>();
        
		anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)
    {
		if(other.gameObject.tag == "Player" || other.gameObject.tag == "PlayerNav")
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit (Collider other)
    {
		if(other.gameObject.tag == "Player" || other.gameObject.tag == "PlayerNav")
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }

        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }
    }


    void Attack ()
    {
        timer = 0f;

        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
