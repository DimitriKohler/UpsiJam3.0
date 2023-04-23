using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioClip highlightSound;

    private AudioSource audioSource;
    private Button button;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        button = GetComponent<Button>();
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }

    public void PlayHighlightSound()
    {
        audioSource.PlayOneShot(highlightSound);
    }

}
