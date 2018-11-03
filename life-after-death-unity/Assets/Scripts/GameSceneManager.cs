using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameSceneManager : MonoBehaviour
{
    static GameSceneManager instance = null;

    static public event Action<PlayerStats.PlayerState> onPlayerStateChange = delegate { };

    PlayerStats.PlayerState playerState = PlayerStats.PlayerState.Alive;

    [SerializeField]
    GameObject lightWorldTerrain;

    [SerializeField]
    GameObject darkWorldTerrain;

    [SerializeField]
    [Range(0, 1)]
    float transparentValue = 0.25f;

    class worldObject
    {
        public Renderer objectRenderer
        {
            get;
            set;
        }

        public Collider2D objectCollider
        {
            get;
            set;
        }
    }

    List<worldObject> darkWorldTerrainList = new List<worldObject>();
  
    List<worldObject> lightWorldTerrainList = new List<worldObject>();

    [SerializeField]
    Player player;

    [SerializeField]
    FollowCamera followCamera;

    void Awake()
    {
        instance = this;

        if (lightWorldTerrain != null)
        {
            
            foreach (Transform item in lightWorldTerrain.transform)
            {
                worldObject newObject = new worldObject();
                newObject.objectRenderer = item.GetComponent<Renderer>();
                newObject.objectCollider = item.GetComponent<Collider2D>();

                lightWorldTerrainList.Add(newObject);
            }
            
        }

        else
            Debug.Log("Light world terrain not set!");

        if (darkWorldTerrain != null)
        {
            foreach (Transform item in darkWorldTerrain.transform)
            {
                worldObject newObject = new worldObject();
                newObject.objectRenderer = item.GetComponent<Renderer>();
                newObject.objectCollider = item.GetComponent<Collider2D>();

                darkWorldTerrainList.Add(newObject);
            }
        }

        else
            Debug.Log("Dark world terrain not set!");
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

        HandleWorldSwitch(playerState);
    }

    void HandleWorldSwitch(PlayerStats.PlayerState newState)
    {
        if(newState == PlayerStats.PlayerState.Alive)
        {
            foreach (worldObject i  in lightWorldTerrainList)
            {
                //Alpha
                Color newAlpha = i.objectRenderer.material.color;
                newAlpha.a = 1.0f;
                i.objectRenderer.material.color = newAlpha;

                //Collider
                i.objectCollider.enabled = true;
            }

            foreach (worldObject i in darkWorldTerrainList)
            {
                //Alpha
                Color newAlpha = i.objectRenderer.material.color;
                newAlpha.a = transparentValue;
                i.objectRenderer.material.color = newAlpha;

                //Collider
                i.objectCollider.enabled = false;
            }
        }

        else if(newState == PlayerStats.PlayerState.Dead)
        {
            foreach (worldObject i in lightWorldTerrainList)
            {
                //Alpha
                Color newAlpha = i.objectRenderer.material.color;
                newAlpha.a = transparentValue;
                i.objectRenderer.material.color = newAlpha;

                //Collider
                i.objectCollider.enabled = false;
            }

            foreach (worldObject i in darkWorldTerrainList)
            {
                //Alpha
                Color newAlpha = i.objectRenderer.material.color;
                newAlpha.a = 1.0f;
                i.objectRenderer.material.color = newAlpha;

                //Collider
                i.objectCollider.enabled = true; 
            }
        }
    }
}
