using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip playerWalk, playerJump, playerLand, playerDmg, playerAtk, playerDeath, playerDestroy;
    public AudioClip newKey;
    static AudioSource playerAudio;

	// Use this for initialization
    void Start () {
        playerAudio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaySound (string clip)
    {
        switch(clip){
            case "walk":
                playerAudio.PlayOneShot(playerWalk);
                break;
            case "jump":
                playerAudio.PlayOneShot(playerJump);
                break;
            /*case "land":
                playerAudio.PlayOneShot(playerLand);
                break;*/
            case "dmg":
                playerAudio.PlayOneShot(playerDmg);
                break;
            case "atk":
                playerAudio.PlayOneShot(playerAtk);
                break;
            /*case "death":
                playerAudio.PlayOneShot(playerDeath);
                break;*/
            case "destroy":
                playerAudio.PlayOneShot(playerDestroy);
                break;
            case "key":
                playerAudio.PlayOneShot(newKey);
                break;
        }
    }
}
