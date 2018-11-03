using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public PlayerStats playerStats;
    public Slider playerHealth;
    public float health;
    public Slider playerSoul;
    
    // Use this for initialization
    void Start()
    {
        playerStats = GameSceneManager.ActivePlayer.PlayerStats;
        playerStats.onHPChange += HPHandler;
        playerStats.onSoulsChange += SoulHandler;
        //playerStats.onPlayerStateChange += StateHandler;
        playerHealth.maxValue = health;
    }

    // Teardown and Deregister
    private void OnDestroy()
    {
        playerStats.onHPChange -= HPHandler;
        playerStats.onSoulsChange -= SoulHandler;
        //playerStats.onPlayerStateChange -= StateHandler;
    }

    // Called on Playerstat change
    void HPHandler(float newHP)
    {
        playerHealth.value = newHP;
    }

    // Called on Playerstat change
    void SoulHandler(float newSoul)
    {
        playerSoul.value = newSoul;
    }

    // Called on Playerstat change
    /*void StateHandler(PlayerStats.Playerstate newState)
    {
    }*/


}
