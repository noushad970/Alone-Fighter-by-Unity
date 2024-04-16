using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{
   /* public AudioSource audioSource;
    public AudioClip[] footstepSounds;
     float stepInterval = 1f;
    public static bool isWalking = false;
    private bool isRightFoot = false; // Flag to track which foot is currently playing sound
    private float timer = 0f;


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
    }
    private void Update()
    {
        if (isWalking)
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
    private void PlayFootstepSound()
    {
        if (footstepSounds.Length == 0 || audioSource == null)
        {
            Debug.LogWarning("FootstepSound script is missing audio source or footstep sounds.");
            return;
        }

        int soundIndex = Random.Range(0, footstepSounds.Length);
        AudioClip footstepSound = footstepSounds[soundIndex];

        if (footstepSound != null)
        {
            audioSource.Play();
        }

        // Toggle foot flag for next footstep
        isRightFoot = !isRightFoot;
    }*/
}
