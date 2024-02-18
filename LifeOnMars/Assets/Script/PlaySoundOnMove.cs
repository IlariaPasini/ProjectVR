using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnMove : MonoBehaviour
{
    public AudioClip[] footstepSounds; // Array to hold footstep sound clips
    private float minSpace = 0.05f; 
    public float maxTimeBetweenFootsteps = 0.6f; // Maximum time between footstep sounds

    private AudioSource audioSource; // Reference to the Audio Source component
    private bool isWalking = false; // Flag to track if the player is walking
    private float timeSinceLastFootstep; // Time since the last footstep sound
    private Vector3 transMove;

    private void Awake()
    {
        transMove = this.transform.position;
        audioSource = GetComponent<AudioSource>(); // Get the Audio Source component

    }

    private void Update()
    {
        float moveDistance = Mathf.Abs(Vector3.Distance(transform.position, transMove));
        // Check if the player is walking
        if (moveDistance >= minSpace)
        {
            // Check if enough time has passed to play the next footstep sound
            if (Time.time - timeSinceLastFootstep >= maxTimeBetweenFootsteps)
            {
                // Play a random footstep sound from the array
                AudioClip footstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];
                audioSource.PlayOneShot(footstepSound);

                timeSinceLastFootstep = Time.time; // Update the time since the last footstep sound
            }
        }
        transMove = transform.position;
    }

  
}
