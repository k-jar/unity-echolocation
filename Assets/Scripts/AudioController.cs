using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public List<AudioClip> footsteps;
    private AudioSource footstepSource;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        footstepSource = GetComponent<AudioSource>();
    }

    public void PlayFootstep()
    {
        footstepSource.clip = footsteps[Random.Range(0, footsteps.Count)];
        footstepSource.volume = Random.Range(0.8f, 1f);
        footstepSource.pitch = Random.Range(0.8f, 1.2f);
        footstepSource.Play();
        Debug.Log("Playing footstep sound");
    }
}
