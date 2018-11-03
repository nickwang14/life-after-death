using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    static GameSceneManager instance = null;


    [SerializeField]
    GameObject playerPrefab;

    Player player;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = Instantiate<GameObject>(playerPrefab).GetComponent<Player>();
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
