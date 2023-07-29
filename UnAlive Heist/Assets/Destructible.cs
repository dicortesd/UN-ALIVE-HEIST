using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : Obstacle
{
    [SerializeField] Collider hitBox;
    [SerializeField] Animator animator;
    [SerializeField] GameObject interactionIndicator;


    PlayerTrigger playerTrigger;

    bool broken = false;

    private void Awake()
    {
        playerTrigger = GetComponentInChildren<PlayerTrigger>();
    }

    private void OnEnable()
    {
        playerTrigger.OnPlayerStay += OnPlayerStay;
    }

    private void OnDisable()
    {
        playerTrigger.OnPlayerStay -= OnPlayerStay;
    }

    private void OnPlayerStay(PlayerController player)
    {
        if (!broken)
        {
            interactionIndicator.SetActive(true);
            if (player.PunchThrown())
            {
                interactionIndicator.SetActive(false);
                hitBox.enabled = false;
                animator.SetTrigger("Break");
                broken = true;
            }
        }
    }

}
