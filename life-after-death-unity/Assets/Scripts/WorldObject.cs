using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldObject : MonoBehaviour
{
    enum WorldState
    {
        Light,
        Dark
    }

    [SerializeField]
    WorldState startingWorldState = WorldState.Light;
    WorldState objectCurrentWorldState;

    Renderer objectRenderer;
    Collider2D objectCollider;

    [SerializeField]
    [Range(0, 1)]
    float transparentValue = 0.25f;

	// Use this for initialization
	void Start ()
    {
        objectRenderer = GetComponent<Renderer>();
        objectCollider = GetComponent<Collider2D>();

        objectCurrentWorldState = startingWorldState;

        if (startingWorldState == WorldState.Dark)
            DisableObject();
        else
            EnableObject();

        GameSceneManager.onPlayerStateChange += WorldChanged;
	}

    private void OnDestroy()
    {
        GameSceneManager.onPlayerStateChange -= WorldChanged;
    }

    private void WorldChanged(PlayerStats.PlayerState newState)
    {
        if(newState == PlayerStats.PlayerState.Alive)
        {
            if(objectCurrentWorldState == WorldState.Light)
            {
                EnableObject();
            }

            else
            {
                DisableObject();
            }
        }

        else if(newState ==PlayerStats.PlayerState.Dead)
        {
            if (objectCurrentWorldState == WorldState.Light)
            {
                DisableObject();
            }

            else
            {
                EnableObject();
            }
        }
    }

    void EnableObject()
    {
        Color newAlpha = objectRenderer.material.color;
        newAlpha.a = 1.0f;
        objectRenderer.material.color = newAlpha;

        objectCollider.enabled = true;
    }

    void DisableObject()
    {
        Color newAlpha = objectRenderer.material.color;
        newAlpha.a = transparentValue;
        objectRenderer.material.color = newAlpha;

        objectCollider.enabled = false;
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
