using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float timeoutBetweenBullets = 1f;
    [SerializeField] AudioClip shootingSound;
    [SerializeField] private float bulletSpeed = 1f;

    Camera _camera;

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
            ShootBullet();
            StartCoroutine(Timeout(timeoutBetweenBullets));
            _audioSource.PlayOneShot(shootingSound);
        }
    }

    void ShootBullet()
    {
        Vector2 target = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 position = transform.position;
        
        Vector2 direction = target - (Vector2)position;
        direction.Normalize();
        GameObject currentProjectile = Instantiate(this.projectile, position, Quaternion.identity);
        currentProjectile.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

    IEnumerator Timeout(float seconds)
    {
        _alreadyShot = true;
        yield return new WaitForSeconds(seconds);
        _alreadyShot = false;
    }
}