using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Movement movement;
    NPCController NPC;
    public KeyCode KEY_TO_PULL = KeyCode.P;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        NPC = FindObjectOfType<NPCController>();
    }


    private void Update()
    {
        if (movement.ReachedLane())
        {
            if (Input.GetKeyDown(KEY_TO_PULL))
            {
                NPC.PullToLane(movement.GetCurrentLaneNumber());
                return;
            }

            float input = Input.GetAxisRaw("Horizontal");
            if (input == 1)
            {
                movement.MoveRight();
            }
            else if (input == -1)
            {
                movement.MoveLeft();
            }


        }
    }
}
