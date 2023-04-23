using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private float pitch = 1;
    private GameObject BGMusic;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        BGMusic = GameObject.Find("BackgroundMusic");
        audioSource = BGMusic.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
        Debug.Log("Hello");
        if (other.CompareTag("Player"))
        {
            audioSource.pitch = pitch;
        } else
        {
            audioSource.pitch = 1;
        }
    }
}
