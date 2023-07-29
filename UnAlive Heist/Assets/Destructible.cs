using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : Obstacle
{
    [SerializeField] Collider hitBox;
    [SerializeField] Animator animator;

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
        if (!broken && player.PunchThrown())
        {
            hitBox.enabled = false;
            animator.SetTrigger("Break");
            broken = true;
        }
    }

}
