using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Movement : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float reachedLaneTolerance = 0.1f;
    [SerializeField] float slowdownDistance = 0.5f;
    [SerializeField] float distanceFromTrackStart;

    Rigidbody rigidBody;
    Track track;
    int currentLaneNumber;

    Vector3 destinationLaneCenter;
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
        GoToStartPosition();
    }

    private void GoToStartPosition()
    {
        currentLaneNumber = Random.Range(1, track.GetNumberOfLanes());
        Vector3 laneCenter = track.GetLane(currentLaneNumber).GetCenter();
        transform.position = track.transform.position + track.transform.forward * distanceFromTrackStart + (laneCenter - track.transform.position);
        destinationLaneCenter = laneCenter;
    }

    private void FixedUpdate()
    {
        if (!ReachedLane())
        {
            UpdateCurrentSpeed();
            float deltaDistance = currentSpeed * Time.fixedDeltaTime;
            float remainingDistance = GetRemainingDistanceToLaneCenter();
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

    public bool ReachedLane()
    {
        return GetRemainingDistanceToLaneCenter() <= reachedLaneTolerance;
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

    public int GetCurrentLaneNumber()
    {
        return currentLaneNumber;
    }


    private float GetRemainingDistanceToLaneCenter()
    {
        return Vector3.Distance(transform.position - track.transform.forward * distanceFromTrackStart, destinationLaneCenter);
    }

    private void UpdateCurrentSpeed()
    {
        float remainingDistance = GetRemainingDistanceToLaneCenter();
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


    private bool CanMoveToLane(int laneNumber)
    {
        return laneNumber > 0 && laneNumber <= track.GetNumberOfLanes();
    }

    private void MoveToLane(int laneNumber)
    {
        Lane lane = track.GetLane(laneNumber);
        destinationLaneCenter = lane.GetCenter();
        currentLaneNumber = laneNumber;
        print(currentLaneNumber);
    }


}
