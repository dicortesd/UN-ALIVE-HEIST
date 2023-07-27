using ExtensionMethods;
using UnityEngine;

public class Track : MonoBehaviour
{
    [SerializeField] GameObject lanePrefab;
    [SerializeField] float laneWidth;

    Lane[] lanes;

    private void Awake()
    {
        lanes = new Lane[transform.childCount];
        for (int i = 0; i < lanes.Length; i++)
        {
            lanes[i] = transform.GetChild(i).GetComponent<Lane>();
        }
    }

    public Lane GetLane(int lane)
    {
        if (lane < 1 || lane > lanes.Length) return null;
        return lanes[lane - 1];

    }

    public int GetNumberOfLanes()
    {
        return lanes.Length;
    }

    private void OnDrawGizmos()
    {
        int numberOfLanes = transform.childCount;
        if (numberOfLanes > 0)
        {
            Vector3 from = transform.position;
            for (int i = 0; i < numberOfLanes; i++)
            {
                from = transform.GetChild(i).position;
                Gizmos.DrawLine(from, from + transform.forward * 50);
            }
            from = from + transform.right * laneWidth;
            Gizmos.DrawLine(from, from + transform.forward * 50);
        }

    }

}

