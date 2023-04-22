using System;
using System.Collections;
using UnityEngine;

public class MageShoot : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float timeoutBetweenBullets = 1f;
    [SerializeField] private float delayFirstShoot = 2f;
    [SerializeField] AudioClip shootingSound;
    [SerializeField] float sideBulletAngle = 30f;

    Camera _camera;
    private GameObject _player;
    
    AudioSource _audioSource;

    private void Awake()
    {
        _camera = Camera.main;
        _audioSource = GetComponent<AudioSource>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(delayFirstShoot);
        
        while(enabled)
        {
            Fire(-sideBulletAngle);
            Fire(0);
            Fire(sideBulletAngle);
            _audioSource.PlayOneShot(shootingSound);

            yield return new WaitForSeconds(timeoutBetweenBullets);
        }
    }

    void Fire(float angle)
    {
        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().Initialize(_player.transform.position, angle);
    }
}