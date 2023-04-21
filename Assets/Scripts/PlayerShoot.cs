using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float timeoutBetweenBullets = 1f;
    [SerializeField] AudioClip shootingSound; 

    bool _alreadyShot = false;
    AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!_alreadyShot && (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)))
        {
            Instantiate(projectile, transform.position, transform.rotation);
            StartCoroutine(Timeout(timeoutBetweenBullets));
            _audioSource.PlayOneShot(shootingSound);
        }
    }

    IEnumerator Timeout(float seconds)
    {
        _alreadyShot = true;
        yield return new WaitForSeconds(seconds);
        _alreadyShot = false;
    }
}