using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAudio : MonoBehaviour, IPointerEnterHandler
{
    public AudioClip AudioClipEnter;
    public AudioClip AudioClipPressed;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        var button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            _audioSource.PlayOneShot(AudioClipPressed);
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _audioSource.PlayOneShot(AudioClipEnter);
    }
}