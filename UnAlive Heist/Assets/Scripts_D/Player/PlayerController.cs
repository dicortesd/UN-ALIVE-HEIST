using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Movement movement;
    NPCController NPC;
    public KeyCode keyToPull = KeyCode.P;
    public KeyCode keyToPunch = KeyCode.W;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        NPC = FindObjectOfType<NPCController>();
    }


    private void Update()
    {
        if (movement.ReachedLane())
        {
            if (Input.GetKeyDown(keyToPull))
            {
                NPC.PullToLane(movement.GetCurrentLaneNumber());
                AudioManager.instance.PlaySound(SoundName.Pull);
                return;
            }

            float input = Input.GetAxisRaw("Horizontal");
            if (input == 1)
            {
                AudioManager.instance.PlaySound(SoundName.PlayerMovement);
                movement.MoveRight();
            }
            else if (input == -1)
            {
                AudioManager.instance.PlaySound(SoundName.PlayerMovement);
                movement.MoveLeft();
            }


        }
    }

    public bool IsPunching()
    {
        AudioManager.instance.PlaySound(SoundName.PlayerPunch);
        return Input.GetKeyDown(keyToPunch);
    }
}
