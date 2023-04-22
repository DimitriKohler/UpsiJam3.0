using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonExitHelp : MonoBehaviour, IPointerEnterHandler
{
    public AudioClip AudioClipEnter;
    private AudioSource audioSourceEnter;

    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {

            canvas.GetComponent<Canvas>().enabled = false;
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioSourceEnter == null)
            audioSourceEnter = GetComponent<AudioSource>();
        if (audioSourceEnter == null)
            audioSourceEnter = gameObject.AddComponent<AudioSource>();

        audioSourceEnter.PlayOneShot(AudioClipEnter);
    }
}
