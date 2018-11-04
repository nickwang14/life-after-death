using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : PickupItem
{
    protected override void PickUpItem(Player player)
    {
        player.PlayerStats.AddKeys(1);
    }
}
