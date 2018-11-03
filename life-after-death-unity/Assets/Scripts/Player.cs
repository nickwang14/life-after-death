using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    PlayerMovement playerMovement;

    [SerializeField]
    PlayerStats playerStats;

    public PlayerMovement PlayerMovement
    {
        get { return playerMovement; }
    }

    public PlayerStats PlayerStats
    {
        get { return playerStats; }
    }
}
