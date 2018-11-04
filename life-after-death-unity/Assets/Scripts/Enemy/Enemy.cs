using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState { Alive, Dead };

    private EnemyState enemyState = EnemyState.Alive;

    [SerializeField]
    EnemyState startingEnemyState = EnemyState.Alive;

    [SerializeField]
    int EnemyHP = 20;

    [SerializeField]
    int soulsAmount = 75;

    int MaxHP;

    [SerializeField]
    float invulTime = 1.5f;

    float invulTimer = 0f;

    public bool IsInvulnerable
    {
        get { return invulTimer > 0f; }
    }

    [SerializeField]
    SpriteRenderer liveSprite;
    [SerializeField]
    SpriteRenderer deadSprite;

    [SerializeField]
    Collider2D enemyCollider;

    [SerializeField]
    ParticleSystem EnemyDeathParticleSystem;

    [SerializeField]
    [Range(0, 1)]
    float transparentValue = 0.25f;

    [SerializeField]
    int EnemyHPIncrease = 10;

    Color baseColor = Color.white;

    void Start()
    {
        MaxHP = EnemyHP;

        GameSceneManager.ActivePlayer.PlayerStats.onPlayerStateChange += OnStateChangeHandler;

        enemyState = startingEnemyState;
        SetShownSprite(enemyState);
        if (enemyState == EnemyState.Dead)
        {
            EnemyDeathParticleSystem.Play();
            DisableObject();
        }

    }

    private void OnDestroy()
    {
        if (GameSceneManager.ActivePlayer != null && GameSceneManager.ActivePlayer.PlayerStats != null)
            GameSceneManager.ActivePlayer.PlayerStats.onPlayerStateChange -= OnStateChangeHandler;
    }

    void Update()
    {
        invulTimer = Mathf.MoveTowards(invulTimer, 0f, Time.deltaTime);

        if (IsInvulnerable)
        {
            float emission = Mathf.PingPong(Time.time * 7.5f, 1.0f);
            SetSpriteColor(new Color(baseColor.r, emission, emission));
        }
        else
        {
            SetSpriteColor(baseColor);
        }
    }

    void OnStateChangeHandler(PlayerStats.PlayerState newState)
    {
        if (enemyState == EnemyState.Alive)
        {
            if (newState == PlayerStats.PlayerState.Dead)
                DisableObject();
            else
                EnableObject();
        }
        else if (enemyState == EnemyState.Dead)
        {
            if (newState == PlayerStats.PlayerState.Alive)
                DisableObject();
            else
                EnableObject();
        }
    }

    public void TakeDamage(int dmg)
    {
        EnemyHP -= dmg;
        EnemyHP = Mathf.Clamp(EnemyHP, 0, MaxHP);
        invulTimer = invulTime;
        if (EnemyHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (enemyState == EnemyState.Alive)
        {
            SetShownSprite(EnemyState.Dead);
            enemyState = EnemyState.Dead;
            EnemyDeathParticleSystem.Play();
            if (GameSceneManager.ActivePlayer.PlayerStats.State == PlayerStats.PlayerState.Dead)
                EnableObject();

            else if (GameSceneManager.ActivePlayer.PlayerStats.State == PlayerStats.PlayerState.Alive)
                DisableObject();
        }

        else if (enemyState == EnemyState.Dead)
        {
            SetShownSprite(EnemyState.Alive);
            enemyState = EnemyState.Alive;
            EnemyDeathParticleSystem.Stop();
            if (GameSceneManager.ActivePlayer.PlayerStats.State == PlayerStats.PlayerState.Alive)
                EnableObject();
            else if (GameSceneManager.ActivePlayer.PlayerStats.State == PlayerStats.PlayerState.Dead)
                DisableObject();
        }

        MaxHP = MaxHP + EnemyHPIncrease;
        EnemyHP = MaxHP;
        GameSceneManager.ActivePlayer.PlayerStats.Souls += soulsAmount;
    }

    void DisableObject()
    {
        SetSpriteAlpha(transparentValue);

        enemyCollider.enabled = false;
    }

    void EnableObject()
    {
        SetSpriteAlpha(1f);

        enemyCollider.enabled = true;
    }

    void SetShownSprite(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Alive:
                liveSprite.gameObject.SetActive(true);
                deadSprite.gameObject.SetActive(false);
                break;
            case EnemyState.Dead:
                liveSprite.gameObject.SetActive(false);
                deadSprite.gameObject.SetActive(true);
                break;
        }
    }

    void SetSpriteColor(Color newColor)
    {
        Color liveColor = newColor;
        liveColor.a = liveSprite.color.a;
        liveSprite.color = liveColor;

        Color deadColor = newColor;
        deadColor.a = deadSprite.color.a;
        deadSprite.color = deadColor;
    }

    void SetSpriteAlpha(float alpha)
    {
        Color liveColor = liveSprite.color;
        liveColor.a = alpha;
        liveSprite.color = liveColor;

        Color deadColor = deadSprite.color;
        deadColor.a = alpha;
        deadSprite.color = deadColor;
    }

    public void SetSpriteFlip(bool isFliped)
    {
        liveSprite.flipX = isFliped;
        deadSprite.flipX = isFliped;
    }
}
