using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    PlayerStats playerStats;

    [SerializeField]
    GameObject playerHPConainter;
    [SerializeField]
    Slider playerHpSlider;

    [SerializeField]
    GameObject playerSoulsContainer;
    [SerializeField]
    Slider playerSoulsSlider;

    void Start()
    {
        playerStats = GameSceneManager.ActivePlayer.PlayerStats;

        playerHpSlider.maxValue = PlayerStats.MaxHP;
        playerStats.onHPChange += OnHPChangeHandler;
        OnHPChangeHandler(playerStats.HP);

        //playerStats.onSoulsChange += OnSoulsChangeHandler;
        //playerSoulsSlider.maxValue = PlayerStats.MaxSouls;
        //OnSoulsChangeHandler(playerStats.Souls);
    }

    void OnDestroy()
    {
        if (playerStats != null)
        {
            playerStats.onHPChange -= OnHPChangeHandler;
            playerStats.onSoulsChange -= OnSoulsChangeHandler;
        }
    }

    void OnHPChangeHandler(float newHP)
    {
        playerHpSlider.value = newHP;
    }

    void OnSoulsChangeHandler(float newSoul)
    {
        playerSoulsSlider.value = newSoul;
    }
}
