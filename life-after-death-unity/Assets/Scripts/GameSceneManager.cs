using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField]
    GameSceneMenu gameSceneMenu;

    static GameSceneManager instance = null;

    static public event Action<PlayerStats.PlayerState> onPlayerStateChange = delegate { };

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OpenGameSceneMenu();
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

    public void OpenGameSceneMenu()
    {
        gameSceneMenu.OpenMenu();
        PauseGame();
    }

    static void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public static void UnPauseGame()
    {
        Time.timeScale = 1f;
    }
}
