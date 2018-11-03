using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameSceneManager : MonoBehaviour
{
    static GameSceneManager instance = null;

<<<<<<< HEAD

    [SerializeField]
    GameObject playerPrefab;
=======
    static public event Action<PlayerStats.PlayerState> onPlayerStateChange;

    PlayerStats.PlayerState playerState = PlayerStats.PlayerState.Alive;
>>>>>>> 98977a8e49fe2b14fd60e0186b48dabb6060ea6e

    [SerializeField]
    Player player;

    [SerializeField]
    FollowCamera followCamera;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        followCamera.Target = player.transform;
        playerState = player.PlayerStats.State;
        player.PlayerStats.onPlayerStateChange += OnStateChangeHandler;
    }

    void OnDestroy()
    {
        if (instance == this)
            instance = null;

        if (player != null && player.PlayerStats != null)
            player.PlayerStats.onPlayerStateChange -= OnStateChangeHandler;
    }

    static public Player ActivePlayer
    {
        get { return instance.player; }
    }

    void OnStateChangeHandler(PlayerStats.PlayerState newState)
    {
        playerState = newState;
        onPlayerStateChange(playerState);
    }
}
