using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCutscene : MonoBehaviour
{
    const string PickupSwordBool = "Pickup";

    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
            StartCutscene(player);
    }

    void StartCutscene(Player player)
    {
        Animator anum = player.GetComponent<Animator>();
    }
}
