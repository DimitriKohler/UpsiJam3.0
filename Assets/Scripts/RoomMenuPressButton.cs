using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMenuPressButton : MonoBehaviour
{

    private AudioSource _clickAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        _clickAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            _clickAudioSource.Play();
        }
    }
}
