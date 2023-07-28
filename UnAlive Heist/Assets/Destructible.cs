using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : Obstacle
{
    [SerializeField] Collider hitBox;
    [SerializeField] Animator animator;

    DestructibleTrigger destructibleTrigger;

    private void Awake()
    {
        destructibleTrigger = GetComponentInChildren<DestructibleTrigger>();
    }

    private void OnEnable()
    {
        destructibleTrigger.OnPlayerPunched += OnPlayerPunched;
    }

    private void OnDisable()
    {
        destructibleTrigger.OnPlayerPunched -= OnPlayerPunched;
    }

    private void OnPlayerPunched()
    {
        hitBox.enabled = false;
        animator.SetTrigger("Break");
    }

}
