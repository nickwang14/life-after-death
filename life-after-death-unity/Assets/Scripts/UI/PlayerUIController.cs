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

    [SerializeField]
    Image keyIcon;
    [SerializeField]
    Text numberOfKeyText;

    void Start()
    {
        playerStats = GameSceneManager.ActivePlayer.PlayerStats;

        playerHpSlider.maxValue = PlayerStats.MaxHP;
        playerStats.onHPChange += OnHPChangeHandler;
        OnHPChangeHandler(playerStats.HP);

        //playerStats.onSoulsChange += OnSoulsChangeHandler;
        //playerSoulsSlider.maxValue = PlayerStats.MaxSouls;
        //OnSoulsChangeHandler(playerStats.Souls);

        playerStats.onPlayerStateChange += OnStateChangeHandler;

        OnKeyNumberChangeHandler(0);
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

    void OnKeyNumberChangeHandler(int newNumberOfKeys)
    {
        if (newNumberOfKeys == 0)
        {
            keyIcon.gameObject.SetActive(false);
            numberOfKeyText.gameObject.SetActive(false);
        }
        else if (newNumberOfKeys == 1)
        {
            keyIcon.gameObject.SetActive(true);
            numberOfKeyText.gameObject.SetActive(false);
        }
        else
        {
            keyIcon.gameObject.SetActive(true);
            numberOfKeyText.gameObject.SetActive(true);
            numberOfKeyText.text = string.Format("x {0}", newNumberOfKeys.ToString());
        }
    }
    void OnStateChangeHandler(PlayerStats.PlayerState newState)
    {
        switch (newState)
        {
            case PlayerStats.PlayerState.Alive:
                break;
            case PlayerStats.PlayerState.Dead:
                break;
            case PlayerStats.PlayerState.Destroyed:
                break;
        }
    }
}
