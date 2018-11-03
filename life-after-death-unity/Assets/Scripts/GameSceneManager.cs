using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    static GameSceneManager instance = null;

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
    }

    void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    static public Player ActivePlayer
    {
        get { return instance.player; }
    }
}
