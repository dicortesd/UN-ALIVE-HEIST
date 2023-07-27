using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    LineRenderer lineRenderer;
    [SerializeField] GameObject player;
    [SerializeField] GameObject NPC;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        lineRenderer.SetPositions(new Vector3[] { player.transform.position, NPC.transform.position });
    }
}
