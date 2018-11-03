using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    public event Action<float> onHPChange = delegate { };
    public event Action<float> onSoulsChange = delegate { };
    public event Action<PlayerState> onPlayerStateChange = delegate { };

    public enum PlayerState { Alive, Dead, Destroyed };

    float startingHP = 50f;
    float startingSouls = 50f;

    float soulsDecayRate = 1f;//Soul decay per second

    const float MaxHP = 100f;
    const float MaxSouls = 100f;

    float currentHP;
    float currentSouls;

    PlayerState state = PlayerState.Alive;

    public PlayerState State
    {
        get { return state; }
        protected set
        {
            state = value;
            onPlayerStateChange(value);
        }
    }

    public float HP
    {
        get { return currentHP; }
        protected set
        {
            currentHP = value;
            if (currentHP < 0f)
            {
                currentHP = 0f;
                KillPlayer();
            }
            onHPChange(currentHP);
        }
    }

    public float Souls
    {
        get { return currentSouls; }
        set
        {
            currentSouls = value;
            if (currentSouls < 0f)
            {
                currentSouls = 0f;
                DestroyPlayer();
            }
            onSoulsChange(currentSouls);
        }
    }

    void Update()
    {
        switch (state)
        {
            case PlayerState.Alive:
                break;
            case PlayerState.Dead:
                Souls -= soulsDecayRate * Time.deltaTime;
                break;
            case PlayerState.Destroyed:
                break;
        }
    }

    public void KillPlayer()
    {
        State = PlayerState.Dead;
        Souls = startingSouls;
    }

    public void ResurectPlayer()
    {
        State = PlayerState.Alive;
        HP = startingHP;
    }

    public void DestroyPlayer()
    {
        State = PlayerState.Destroyed;
    }
}
