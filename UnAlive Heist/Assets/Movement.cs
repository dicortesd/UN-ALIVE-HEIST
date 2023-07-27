using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Movement : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float reachedLaneTolerance = 0.1f;
    [SerializeField] float slowdownDistance = 0.5f;

    Rigidbody rigidBody;
    Track track;
    int currentLaneNumber;

    Vector3 destination;
    Vector3 movementDirection;
    float currentSpeed;
    float maxDecceleration;


    private void Awake()
    {
        track = FindObjectOfType<Track>();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        maxDecceleration = (maxSpeed * maxSpeed) / (2 * slowdownDistance);
        currentLaneNumber = Random.Range(1, track.GetNumberOfLanes());
        transform.position = track.GetLane(currentLaneNumber).GetCenter();
        destination = transform.position;
    }

    private void FixedUpdate()
    {
        if (!ReachedLane())
        {
            UpdateCurrentSpeed();
            float deltaDistance = currentSpeed * Time.fixedDeltaTime;
            float remainingDistance = GetRemainingDistance();
            if (deltaDistance >= remainingDistance)
            {
                deltaDistance = remainingDistance;
            }
            rigidBody.MovePosition(transform.position + movementDirection * deltaDistance);
        }
        else
        {
            currentSpeed = 0;
        }


    }


    private bool ReachedLane()
    {
        return GetRemainingDistance() <= reachedLaneTolerance;
    }

    private float GetRemainingDistance()
    {
        return Vector3.Distance(transform.position, destination);
    }

    private void UpdateCurrentSpeed()
    {
        float remainingDistance = GetRemainingDistance();
        if (remainingDistance >= slowdownDistance)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            float decceleration = Mathf.Min(maxDecceleration, acceleration);
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, decceleration * Time.deltaTime);
        }
    }

    public void MoveRight()
    {
        if (CanMoveToLane(currentLaneNumber + 1))
        {
            MoveToLane(currentLaneNumber + 1);
            movementDirection = track.transform.right;
        }
    }

    public void MoveLeft()
    {
        if (CanMoveToLane(currentLaneNumber - 1))
        {
            MoveToLane(currentLaneNumber - 1);
            movementDirection = -track.transform.right;
        }
    }

    private bool CanMoveToLane(int laneNumber)
    {
        return laneNumber > 0 && laneNumber <= track.GetNumberOfLanes();
    }

    private void MoveToLane(int laneNumber)
    {
        Lane lane = track.GetLane(laneNumber);
        destination = lane.GetCenter();
        currentLaneNumber = laneNumber;
        print(currentLaneNumber);
    }


}
