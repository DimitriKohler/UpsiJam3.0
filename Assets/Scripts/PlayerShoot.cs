using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float timeoutBetweenBullets = 1f;
    [SerializeField] AudioClip shootingSound;

    Camera _camera;

    private Vector2 positionOnScreen;
    private Vector2 mouseOnScreen;
    private float angle;

    bool _alreadyShot = false;
    AudioSource _audioSource;

    void Start()
    {
        _camera = Camera.main;
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

        // ROTATE ACCORDING TO MOUSE
        // thanks: https://answers.unity.com/questions/855976/make-a-player-model-rotate-towards-mouse-location.html
        positionOnScreen = _camera.WorldToViewportPoint(transform.position);
        mouseOnScreen = _camera.ScreenToViewportPoint(Input.mousePosition);
        angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    IEnumerator Timeout(float seconds)
    {
        _alreadyShot = true;
        yield return new WaitForSeconds(seconds);
        _alreadyShot = false;
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}