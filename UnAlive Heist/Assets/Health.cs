using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHits;

    Animator animator;

    int hits;
    bool isDead;

    public Action OnDead;
    public Action OnHit;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        hits = 0;
    }

    private void OnEnable()
    {
        Reset();
    }

    public float GetHits()
    {
        return hits;
    }
    public float GetMaxHits()
    {
        return maxHits;
    }

    public void TakeDamage()
    {
        if (isDead) return;
        hits += 1;
        OnHit?.Invoke();
        if (hits >= maxHits)
        {
            Die();
            hits = maxHits;
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void Reset()
    {
        hits = 0;
        isDead = false;
    }

    private void Die()
    {
        isDead = true;
        if (animator != null) animator.SetTrigger("Die");
        OnDead?.Invoke();
    }

    private void OnCollisionEnter(Collision other)
    {
        TakeDamage();
    }

}
