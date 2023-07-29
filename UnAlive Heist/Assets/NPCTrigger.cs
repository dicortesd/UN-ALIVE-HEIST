using System;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    public Action<NPCController> OnNPCEnter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<NPCController>(out NPCController NPC))
        {
            OnNPCEnter?.Invoke(NPC);
        }
    }
}
