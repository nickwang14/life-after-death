﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordinthestoneIntro : MonoBehaviour
{
    bool done = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.GetComponent<Player>() != null)
        {
            if (!done)
            {
                GameSceneManager.ActivePlayer.PlayerStats.KillPlayer();
            }
            done = true;
        }
    }
}
