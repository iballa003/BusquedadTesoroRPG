using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioManager instance;
    public AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip keysSound;
    public AudioClip closeDoorSound;
    public AudioClip unlockDoorSound;
    public AudioClip openDoorSound;

    public void PlayConfirmSound()
    {
        audioSource.PlayOneShot(clickSound);
    }

    public void PlayKeysSound()
    {
        audioSource.PlayOneShot(keysSound);
    }

    public void PlayCloseDoorSound()
    {
        audioSource.PlayOneShot(closeDoorSound, 0.2f);
    }
    public void PlayOpenDoorSound() {
        audioSource.PlayOneShot(openDoorSound, 0.2f);
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            // Intenta obtener el AudioSource si no está asignado
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
                if (audioSource == null)
                {
                    Debug.LogError("AudioManager: No se encontró un AudioSource en este GameObject.");
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
