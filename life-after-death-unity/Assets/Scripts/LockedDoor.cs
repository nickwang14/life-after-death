using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [SerializeField]
    GameObject doorObject;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null && player.PlayerStats.HasKey)
        {
            player.PlayerStats.RemoveKey();
            doorObject.SetActive(false);
        }
    }
}
