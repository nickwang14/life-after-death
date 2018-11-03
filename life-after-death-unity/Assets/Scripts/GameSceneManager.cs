using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameSceneManager : MonoBehaviour
{
    static GameSceneManager instance = null;

    static public event Action<PlayerStats.PlayerState> onPlayerStateChange;

    PlayerStats.PlayerState playerState = PlayerStats.PlayerState.Alive;

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
        //followCamera.Target = player.transform;
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
