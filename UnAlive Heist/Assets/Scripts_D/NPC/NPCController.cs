using System;
using System.Collections;
using UnityEngine;

public class NPCController : MonoBehaviour
{

    Health health;
    Movement movement;
    Animator animator;
    [SerializeField]CapsuleCollider capsuleCollider;
    Coroutine runSoundRoutine;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        health = GetComponentInChildren<Health>();
        animator = GetComponentInChildren<Animator>(); 
        capsuleCollider = GetComponent<CapsuleCollider>();
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

        AudioManager.instance.PlaySound(SoundName.NPCHit);
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
            Jump();//
        }
    }
 


    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DeactivateColliderForSeconds(2f));
        }
    }

    private IEnumerator DeactivateColliderForSeconds(float seconds)
    {
        // Desactiva el collider
        capsuleCollider.enabled = false;

        // Espera durante el tiempo especificado
        yield return new WaitForSeconds(seconds);

        // Activa el collider nuevamente
        capsuleCollider.enabled = true;
    }
}
