using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupItem : MonoBehaviour
{
    protected abstract void PickUpItem(Player player);

    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            PickUpItem(player);
            gameObject.SetActive(false);
        }
    }
}
