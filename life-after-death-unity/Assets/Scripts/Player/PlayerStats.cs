using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    public event Action<float> onHPChange = delegate { };
    public event Action<float> onSoulsChange = delegate { };
    public event Action<PlayerState> onPlayerStateChange = delegate { };
    public event Action<int> onKeysChange = delegate { };

    int PlayerLightLayer = 12;
    int PlayerDarkLayer = 13;

    public enum PlayerState { Alive, Dead, Destroyed };

    float startingHP = MaxHP;
    float startingSouls = 50f;

    float soulsDecayRate = 1f;//Soul decay per second

    public const float MaxHP = 100f;
    public const float MaxSouls = 100f;

    float currentHP;
    float currentSouls;

    Player player;

    [SerializeField]
    ParticleSystem soulsParticleSystem;

    [SerializeField]
    float invulTime = 1.5f;
    float invulTimer = 0f;

    // Destroy animation fields
    [SerializeField]
    Animator anim;
    const string isDestroyedString = "IsDestroyed";
    readonly int isDestroyedHash = Animator.StringToHash(isDestroyedString);

    // Player State Audio
    [SerializeField]
    SoundManager sfx;

    int numOfKeys = 0;

    SpriteRenderer spriteRenderer;

    PlayerState state = PlayerState.Alive;

    Color baseColor;

    public bool IsInvulnerable
    {
        get { return invulTimer > 0f; }
    }

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
            if (State != PlayerState.Dead)
                return;
            currentSouls = value;
            if (currentSouls < 0f)
            {
                currentSouls = 0f;
                DestroyPlayer();
                anim.SetBool(isDestroyedHash, true);
            }

            if (currentSouls > MaxSouls)
            {
                ResurrectPlayer();
            }
            onSoulsChange(currentSouls);
        }
    }

    void Start()
    {
        HP = MaxHP;

        player = GetComponent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        baseColor = spriteRenderer.color;
    }

    void Update()
    {
        switch (state)
        {
            case PlayerState.Alive:
                break;
            case PlayerState.Dead:
                if (player.PlayerMovement.AllowInput)
                    Souls -= soulsDecayRate * Time.deltaTime;
                ParticleSystem.EmissionModule emission;
                if(soulsParticleSystem != null)
                    emission = soulsParticleSystem.emission;
                float normalizedSouls = Souls / MaxSouls;
                if (soulsParticleSystem != null)
                    emission.rateOverTime = 100.0f * normalizedSouls;
                //soulsParticleSystem.setem = emission;
                break;
            case PlayerState.Destroyed:
                break;
        }

        if (Input.GetKeyDown(KeyCode.T) || Input.GetButtonDown("Jump"))
        {
            //Switch World
            if (State == PlayerStats.PlayerState.Alive)
            {
                KillPlayer();
            }

            else if (State == PlayerStats.PlayerState.Dead)
            {
                ResurrectPlayer();
            }

            else if(State == PlayerState.Destroyed)
            {
                HP = 0;
            }
        }

        if (IsInvulnerable)
        {
            float emission = Mathf.PingPong(Time.time * 7.5f, 1.0f);

            spriteRenderer.color = new Color(baseColor.r, emission, emission);
        }
        else
        {
            if (spriteRenderer.color != baseColor)
                spriteRenderer.color = baseColor;
        }

        invulTimer = Mathf.MoveTowards(invulTimer, 0f, Time.deltaTime);
    }

    public void KillPlayer()
    {
        State = PlayerState.Dead;
        Souls = startingSouls;
        if (soulsParticleSystem != null)
            soulsParticleSystem.Play();

        gameObject.layer = PlayerDarkLayer;
    }

    public void ResurrectPlayer()
    {
        State = PlayerState.Alive;
        HP = startingHP;
        if (soulsParticleSystem != null)
            soulsParticleSystem.Stop();

        invulTimer = invulTime;

        gameObject.layer = PlayerLightLayer;
    }

    public void DestroyPlayer()
    {
        State = PlayerState.Destroyed;
        sfx.PlaySound("destroy");
        soulsParticleSystem.Stop();
    }

    public void DamagePlayer(float amount, bool force = false)
    {
        if (force || !IsInvulnerable)
        {
            switch (state)
            {
                case PlayerState.Alive:
                    HP -= Mathf.Abs(amount);
                    invulTimer = invulTime;
                    sfx.PlaySound("dmg");
                    break;
                case PlayerState.Dead:
                    Souls -= Mathf.Abs(amount);
                    invulTimer = invulTime;
                    sfx.PlaySound("dmg");
                    break;
                case PlayerState.Destroyed:
                    break;
            }
        }
    }

    public bool HasKey
    {
        get { return numOfKeys > 0; }
    }

    public void AddKeys(int amount)
    {
        numOfKeys += amount;
        onKeysChange(numOfKeys);
        sfx.PlaySound("key");
    }

    public void RemoveKey()
    {
        if (HasKey)
        {
            numOfKeys--;
            onKeysChange(numOfKeys);
        }
    }

    public int GetNumOfKeys()
    {
        return numOfKeys;
    }
}
