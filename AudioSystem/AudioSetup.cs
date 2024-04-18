using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSetup : MonoBehaviour
{
    public AudioClip drawSword;
    public AudioClip SwordAttack1;
    public AudioClip SwordAttack2;
    public AudioClip GunShoot;
    public AudioClip SwordSliceFree;
    public AudioClip GranadeExplode;
    public AudioClip HumanHurt;
    public AudioClip HumanHurt2;
    public AudioClip Humandead;
    public AudioClip Environment;
    public AudioClip FistPunch1;
    public AudioClip FistPunch2;
    public AudioClip ComboPunch;
    public AudioClip Kick;
    public AudioClip EmptyFistPunch;
    public AudioClip jumpDown;
    public AudioClip reloadPistol;
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioClip shootAndRelaod;

    public AudioClip[] footstepSounds;
    public float walkingStepInterval = 0.8f;
    public float runningStepInterval = 0.3f;
    public float stepInterval;
    public static bool isWalking = false;
    private bool isRightFoot = false; // Flag to track which foot is currently playing sound
    private float timer = 0f;

    private bool isPlaying = false;
    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        if (audioSource2 == null)
        {
            audioSource2 = GetComponent<AudioSource>();
            if (audioSource2 == null)
            {
                audioSource2 = gameObject.AddComponent<AudioSource>();
            }
        }
        stepInterval = runningStepInterval;

        // Assign the sound clip to the audio source


    }
    void Update()
    {
        // Check if the button is pressed
        if(RifleControl.isReloading)
            playShootAndReloadSound();

        // Check if the sound has finished playing
        if (isPlaying && !audioSource.isPlaying)
        {
            isPlaying = false;
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            playGranadeSound();
        }

        if (isWalking && !SingleMeleeAttack.isAttackingWithSword && !RifleControl.isReloading)
        {
            // Count time for footstep intervals
            timer += Time.deltaTime;
            if (timer >= stepInterval)
            {
                // Play footstep sound
                PlayFootstepSound();

                // Reset timer
                timer = 0f;
            }
        }
    }
    private  void PlayFootstepSound()
    {
        if (footstepSounds.Length == 0 || audioSource2 == null)
        {
            Debug.LogWarning("FootstepSound script is missing audio source or footstep sounds.");
            return;
        }

        int soundIndex = Random.Range(0, footstepSounds.Length);
        AudioClip footstepSound = footstepSounds[soundIndex];

        if (footstepSound != null)
        {
            audioSource2.Play();
        }

        // Toggle foot flag for next footstep
        isRightFoot = !isRightFoot;
    }
    public void playGunSound()
    {
        audioSource.clip = GunShoot;
        // Check if the sound is not already playing
        if (!isPlaying)
        {
            // Play the sound
            audioSource.Play();
            isPlaying = true;
        }
    }
   
    public void playShootAndReloadSound()
    {
        audioSource.clip = shootAndRelaod;
        if (!isPlaying)
        {
            // Play the sound
            audioSource.Play();
            isPlaying = true;
        }
    }
    public void playGranadeSound()
    {
        audioSource.clip = GranadeExplode;
        if (!isPlaying)
        {
            // Play the sound
            audioSource.Play();
            isPlaying = true;
        }
    }
    public void playDrawSwordSound()
    {
        audioSource.clip = drawSword;
        // Check if the sound is not already playing
        if (!isPlaying)
        {
            // Play the sound
            audioSource.Play();
            isPlaying = true;
        }
    }
    public void playSwordSliceEmptySound()
    {
        audioSource.clip = SwordSliceFree;
        // Check if the sound is not already playing
        if (!isPlaying)
        {
            // Play the sound
            audioSource.Play();
            isPlaying = true;
        }
    }
    public void playSwordAttack1Sound()
    {
        audioSource.clip = SwordAttack1;
        // Check if the sound is not already playing
        if (!isPlaying)
        {
            // Play the sound
            audioSource.Play();
            isPlaying = true;
        }
    }
    public void playSwordAttack2Sound()
    {
        audioSource.clip = SwordAttack2;
        // Check if the sound is not already playing
        if (!isPlaying)
        {
            // Play the sound
            audioSource.Play();
            isPlaying = true;
        }
    }
    public void playfistAttackEmptySound()
    {
        audioSource.clip = EmptyFistPunch;
        // Check if the sound is not already playing
        if (!isPlaying)
        {
            // Play the sound
            audioSource.Play();
            isPlaying = true;
        }
    }
    public void playFistPunch1Sound()
    {
        audioSource.clip = FistPunch1;
        // Check if the sound is not already playing
        if (!isPlaying)
        {
            // Play the sound
            audioSource.Play();
            isPlaying = true;
        }
    }
    public void playFistPunch2Sound()
    {
        audioSource.clip = FistPunch2;
        // Check if the sound is not already playing
        if (!isPlaying)
        {
            // Play the sound
            audioSource.Play();
            isPlaying = true;
        }
    }
    public void playHumanHurt1Sound()
    {
        audioSource.clip = HumanHurt;
        // Check if the sound is not already playing
        if (!isPlaying)
        {
            // Play the sound
            audioSource.Play();
            isPlaying = true;
        }
    }
    public void playHumanHurt2Sound()
    {
        audioSource.clip = HumanHurt2;
        // Check if the sound is not already playing
        if (!isPlaying)
        {
            // Play the sound
            audioSource.Play();
            isPlaying = true;
        }
    }
    public void playHumanDeadSound()
    {
        audioSource.clip = Humandead;
        // Check if the sound is not already playing
        if (!isPlaying)
        {
            // Play the sound
            audioSource.Play();
            isPlaying = true;
        }
    }
    public void playComboPunchSound()
    {
        audioSource.clip = HumanHurt;
        // Check if the sound is not already playing
        if (!isPlaying)
        {
            // Play the sound
            audioSource.Play();
            isPlaying = true;
        }
    }
}
