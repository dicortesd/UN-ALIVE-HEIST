using UnityEngine;

public class NPCController : MonoBehaviour
{

    Movement movement;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    private void Start()
    {
        AudioManager.instance.PlaySound(SoundName.HorseRun);
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
