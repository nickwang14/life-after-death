using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainObject : WorldObject
{
    bool PlayerIsInWall = false;

    protected override void EnableObject()
    {
        if (CheckIfPlayerIsInTerrain())
        {
            Debug.Log("You ded");
            GameSceneManager.ActivePlayer.PlayerStats.DestroyPlayer();
            PlayerIsInWall = false;
        }

        base.EnableObject();
    }


    bool CheckIfPlayerIsInTerrain()
    {
        if (PlayerIsInWall)
            return true;
        else
            return false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            PlayerIsInWall = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {

        if (col.tag == "Player")
        {
            PlayerIsInWall = false;
        }
    }
}
