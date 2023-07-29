using System;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public Action<PlayerController> OnPlayerStay;
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            OnPlayerStay?.Invoke(player);
        }
    }
}
