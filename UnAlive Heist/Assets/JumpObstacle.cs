using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpObstacle : Obstacle
{
    [SerializeField] NPCTrigger NPCTrigger;
    [SerializeField] PlayerTrigger playerTrigger;

    bool jumpEnabled = false;

    private void OnEnable()
    {
        playerTrigger.OnPlayerStay += OnPlayerStay;
        NPCTrigger.OnNPCEnter += OnNPCEnter;
    }

    private void OnDisable()
    {
        playerTrigger.OnPlayerStay -= OnPlayerStay;
        NPCTrigger.OnNPCEnter -= OnNPCEnter;
    }

    private void OnPlayerStay(PlayerController player)
    {
        if (!jumpEnabled && player.JumpCalled())
        {
            jumpEnabled = true;
        }
    }

    private void OnNPCEnter(NPCController NPC)
    {
        if (jumpEnabled) NPC.Jump();
    }
}
