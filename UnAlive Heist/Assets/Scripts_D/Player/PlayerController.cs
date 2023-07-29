using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Movement movement;
    NPCController NPC;
    [SerializeField] KeyCode keyToPull = KeyCode.P;
    [SerializeField] KeyCode keyToPunch = KeyCode.W;
    [SerializeField] KeyCode keyToCallJump = KeyCode.Space;

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

    public bool PunchThrown()
    {
        if (movement.ReachedLane() && Input.GetKeyDown(keyToPunch))
        {
            AudioManager.instance.PlaySound(SoundName.PlayerPunch);
            return true;
        }

        return false;
    }

    public bool JumpCalled()
    {
        if (movement.ReachedLane() && Input.GetKeyDown(keyToCallJump))
        {
            AudioManager.instance.PlaySound(SoundName.PlayerShout);
            return true;
        }

        return false;
    }
}
