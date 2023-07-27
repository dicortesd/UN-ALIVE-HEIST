using UnityEngine;

public class NPCController : MonoBehaviour
{
    Track track;

    private void Awake()
    {
        track = FindObjectOfType<Track>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(ChangeLane), 0, 2);
    }

    private void ChangeLane()
    {
        transform.position = track.GetRandomLane().GetCenter();
    }
}
