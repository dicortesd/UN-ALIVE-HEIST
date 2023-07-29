
using System.Collections;
using ExtensionMethods;
using UnityEngine;

public class NPCController : MonoBehaviour
{

    Collider myCollider;
    Health health;
    Movement movement;
    Animator animator;
    Coroutine runSoundRoutine;

    bool jumping = false;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        health = GetComponentInChildren<Health>();
        animator = GetComponentInChildren<Animator>();
        myCollider = GetComponent<Collider>();
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
        if (!movement.ReachedLane() || jumping) return;
        if (laneNumber > movement.GetCurrentLaneNumber())
        {
            movement.MoveRight();
        }
        else if (laneNumber < movement.GetCurrentLaneNumber())
        {
            movement.MoveLeft();
        }
    }

    public void Jump()
    {

        StartCoroutine(JumpRoutine());
    }

    private IEnumerator JumpRoutine()
    {
        jumping = true;
        myCollider.enabled = false;
        animator.SetTrigger("Jump");
        AudioManager.instance.PlaySound(SoundName.NPCJump);
        yield return AnimatorExtensions.WaitForCurrentAnimatorState(animator, 0);
        myCollider.enabled = true;
        jumping = false;
    }
}
