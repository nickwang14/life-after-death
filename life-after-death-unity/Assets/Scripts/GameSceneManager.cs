using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameSceneManager : MonoBehaviour
{
    enum GameState { Playing, GameSceneMenu, GameOverMenu }

    GameState gameState = GameState.Playing;

    [SerializeField]
    GameSceneMenu gameSceneMenu;

    [SerializeField]
    GameSceneMenu gameOverMenu;

    public static GameSceneManager instance = null;

    static public event Action<PlayerStats.PlayerState> onPlayerStateChange = delegate { };

    PlayerStats.PlayerState playerState = PlayerStats.PlayerState.Alive;

    [SerializeField]
    Player player;

    [SerializeField]
    FollowCamera followCamera;

    [SerializeField]
    string[] checkpointFlags;

    [SerializeField]
    Transform[] checkpointPositions;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //followCamera.Target = player.transform;
        playerState = player.PlayerStats.State;
        player.PlayerStats.onPlayerStateChange += OnStateChangeHandler;
        CheckpointLoad();
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
        if (Input.GetKeyDown(KeyCode.Escape) && gameState == GameState.Playing)
            OpenGameSceneMenu();
    }

    static public Player ActivePlayer
    {
        get { return instance != null ? instance.player : null; }
    }

    void OnStateChangeHandler(PlayerStats.PlayerState newState)
    {
        playerState = newState;
        onPlayerStateChange(playerState);
    }

    void OpenGameSceneMenu()
    {
        gameSceneMenu.OpenMenu();
        gameState = GameState.GameSceneMenu;
        PauseGame();
    }

    public void OpenGameOverMenu()
    {
        gameOverMenu.OpenMenu();
        gameState = GameState.GameOverMenu;
    }

    static void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public static void UnPauseGame()
    {
        Time.timeScale = 1f;
        instance.gameState = GameState.Playing;
    }

    void CheckpointLoad()
    {
        ProgresionFlags flags = GameController.ProgresionFlags;
        for (int i = checkpointFlags.Length - 1; i >= 0; i--)
        {
            string flag = checkpointFlags[i];
            if (flags.HasFlag(flag))
            {
                Vector3 movePoint = checkpointPositions[i].position;
                player.transform.position = movePoint;
                return;
            }
        }
    }
}
