using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState { Alive, Dead };

    public EnemyState enemyState = EnemyState.Alive;

    [SerializeField]
    int EnemyHP = 20;

    int MaxHP;

    [SerializeField]
    float invulTime = 1.5f;

    float invulTimer = 0f;

    public bool IsInvulnerable
    {
        get { return invulTimer > 0f; }
    }


    [SerializeField]
    Renderer enemyRenderer;
    [SerializeField]
    Collider2D enemyCollider;

    [SerializeField]
    [Range(0, 1)]
    float transparentValue = 0.25f;

    [SerializeField]
    int EnemyHPIncrease = 10;

    // Use this for initialization
    void Start ()
    {
        MaxHP = EnemyHP;

        enemyRenderer = GetComponent<Renderer>();
        //enemyCollider = GetComponentInChildren<Collider2D>();
        GameSceneManager.ActivePlayer.PlayerStats.onPlayerStateChange += OnStateChangeHandler;
    }

    private void OnDestroy()
    {
        GameSceneManager.ActivePlayer.PlayerStats.onPlayerStateChange -= OnStateChangeHandler;
    }

    // Update is called once per frame
    void Update ()
    {
        invulTimer = Mathf.MoveTowards(invulTimer, 0f, Time.deltaTime);
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
        if(enemyState == EnemyState.Alive)
        {
            enemyState = EnemyState.Dead;
            if (GameSceneManager.ActivePlayer.PlayerStats.State == PlayerStats.PlayerState.Dead)
                EnableObject();

            else if (GameSceneManager.ActivePlayer.PlayerStats.State == PlayerStats.PlayerState.Alive)
                DisableObject();
        }

        else if(enemyState == EnemyState.Dead)
        {
            enemyState = EnemyState.Alive;
            if (GameSceneManager.ActivePlayer.PlayerStats.State == PlayerStats.PlayerState.Alive)
                EnableObject();
            else if (GameSceneManager.ActivePlayer.PlayerStats.State == PlayerStats.PlayerState.Dead)
                DisableObject();
        }

        MaxHP = MaxHP + EnemyHPIncrease;
        EnemyHP = MaxHP;
    }

    void DisableObject()
    {
        Color newAlpha = enemyRenderer.material.color;
        newAlpha.a = transparentValue;
        enemyRenderer.material.color = newAlpha;

        enemyCollider.enabled = false;
    }

    void EnableObject()
    {
        Color newAlpha = enemyRenderer.material.color;
        newAlpha.a = 1.0f;
        enemyRenderer.material.color = newAlpha;

        enemyCollider.enabled = true;
    }
}
