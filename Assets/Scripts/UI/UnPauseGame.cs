using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnPauseGame : MonoBehaviour
{

    public AudioClip AudioClipEnter;
    private AudioSource audioSourceEnter;


    public GameObject pauseOverlay;
    
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            //Constants.Constant.PAUSE = false;
            pauseOverlay.SetActive(false);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
