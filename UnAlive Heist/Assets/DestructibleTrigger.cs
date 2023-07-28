using System;
using UnityEngine;

public class DestructibleTrigger : MonoBehaviour
{
    public Action OnPlayerPunched;
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            print("player in");
            if (player.IsPunching())
            {
                OnPlayerPunched?.Invoke();
            }
        }
    }
}
