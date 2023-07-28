using System;
using System.Collections;
using UnityEngine;

public class NPCController : MonoBehaviour
{

    Health health;
    Movement movement;
    Animator animator;

    Coroutine runSoundRoutine;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        health = GetComponentInChildren<Health>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        health.OnHit += OnHitObstacle;
        health.OnDead += OnDead;
    }



    private void OnDisable()
    {
        health.OnHit -= OnHitObstacle;
        health.OnDead -= OnDead;
    }


    private void OnDead()
    {
        animator.SetBool("Dead", true);
    }

    private void OnHitObstacle()
    {
    }

    public void PullToLane(int laneNumber)
    {
        if (!movement.ReachedLane()) return;
        if (laneNumber > movement.GetCurrentLaneNumber())
        {
            movement.MoveRight();
        }
        else if (laneNumber < movement.GetCurrentLaneNumber())
        {
            movement.MoveLeft();
        }
        else
        {
            //Do nothing;
        }
    }
}
