using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainObject : WorldObject
{
    bool PlayerIsInWall = false;

    [SerializeField]
    Collider2D terrainHitBox;

    
    Collider2D playerHitBox;

    protected override void Start()
    {
        playerHitBox = GameSceneManager.ActivePlayer.GetComponent<Collider2D>();
        base.Start();
    }


    protected override void EnableObject()
    {
        if (CheckIfPlayerIsInTerrain())
        {
            Debug.Log("YOU DIED");
            GameSceneManager.ActivePlayer.PlayerStats.DestroyPlayer();
            PlayerIsInWall = false;
        }

        base.EnableObject();
    }


    bool CheckIfPlayerIsInTerrain()
    {
        PlayerIsInWall = terrainHitBox.bounds.Intersects(playerHitBox.bounds);


        if (PlayerIsInWall)
            return true;
        else
            return false;
    }


}
