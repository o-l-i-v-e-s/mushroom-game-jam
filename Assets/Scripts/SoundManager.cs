using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] GameObject SoundButton;
    Image SoundImage;
    bool IsSoundOn = true;
    [SerializeField] float Volume = 0.5f;
    [SerializeField] AudioClip pickupPowerup;
    [SerializeField] AudioClip placeBomb;
    [SerializeField] AudioClip explodeBomb;
    [SerializeField] AudioClip characterDeath;
    [SerializeField] AudioClip gameEnd;
    [SerializeField] AudioSource backgroundMusic;

    // Start is called before the first frame update
    AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("audioSource is null on SoundManager");
        }
        if (SoundButton == null)
        {
            Debug.LogError("SoundButton is null");
        } else
        {
            SoundImage = SoundButton.GetComponent<Image>();
            if(SoundImage == null)
            {
                Debug.LogError("SoundImage is null on SoundManager");
            }
        }
        if (pickupPowerup == null)
        {
            Debug.LogError("pickupPowerup is null on SoundManager");
        }
        if (placeBomb == null)
        {
            Debug.LogError("placeBomb is null on SoundManager");
        }
        if (explodeBomb == null)
        {
            Debug.LogError("explodeBomb is null on SoundManager");
        }
        if (characterDeath == null)
        {
            Debug.LogError("characterDeath is null on SoundManager");
        }
        if (gameEnd == null)
        {
            Debug.LogError("gameEnd is null on SoundManager");
        }
        if (backgroundMusic == null)
        {
            Debug.LogError("backgroundMusic is null on SoundManager");
        }
    }

    public void ToggleSound()
    {
        IsSoundOn = !IsSoundOn;
        float alpha = 1f;
        if (!IsSoundOn)
        {
            backgroundMusic.enabled = false;
            alpha = 0.3f;
        } else
        {
            backgroundMusic.enabled = true;
        }
        SoundImage.color = new Color(SoundImage.color.r, SoundImage.color.g, SoundImage.color.b, alpha);
    }

    public void PlaySoundPickupPowerup()
    {
        if(IsSoundOn)
        {
            audioSource.PlayOneShot(pickupPowerup, Volume);
        }
    }

    public void PlaySoundPlaceBomb()
    {
        if (IsSoundOn)
        {
            audioSource.PlayOneShot(placeBomb, Volume);
        }
    }

    public void PlaySoundExplodeBomb()
    {
        if (IsSoundOn)
        {
            audioSource.PlayOneShot(explodeBomb, Volume);
        }
    }

    public void PlaySoundCharacterDeath()
    {
        if (IsSoundOn)
        {
            audioSource.PlayOneShot(characterDeath, Volume);
        }
    }

    public void PlaySoundEndGame()
    {
        if (IsSoundOn)
        {
            audioSource.PlayOneShot(gameEnd, Volume);
        }
    }

}
