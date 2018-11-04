using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    PlayerStats playerStats;

    [SerializeField]
    GameObject playerHPContainer;
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

        playerSoulsSlider.maxValue = PlayerStats.MaxSouls;
        playerStats.onSoulsChange += OnSoulsChangeHandler;
        OnSoulsChangeHandler(playerStats.Souls);

        playerStats.onPlayerStateChange += OnStateChangeHandler;
        OnStateChangeHandler(playerStats.State);

        playerStats.onKeysChange += OnKeyNumberChangeHandler;
        OnKeyNumberChangeHandler(playerStats.GetNumOfKeys());
    }

    void OnDestroy()
    {
        if (playerStats != null)
        {
            playerStats.onHPChange -= OnHPChangeHandler;
            playerStats.onSoulsChange -= OnSoulsChangeHandler;
            playerStats.onPlayerStateChange -= OnStateChangeHandler;
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
                playerHPContainer.SetActive(true);
                playerSoulsContainer.SetActive(false);
                break;
            case PlayerStats.PlayerState.Dead:
                playerHPContainer.SetActive(false);
                playerSoulsContainer.SetActive(true);
                break;
            case PlayerStats.PlayerState.Destroyed:
                playerHPContainer.SetActive(false);
                playerSoulsContainer.SetActive(false);
                break;
        }
    }
}
