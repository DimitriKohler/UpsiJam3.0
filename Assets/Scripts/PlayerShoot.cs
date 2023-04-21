using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float timeoutBetweenBullets = 1f;

    bool _alreadyShot = false;

    void Update()
    {
        if (!_alreadyShot && (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)))
        {
            Instantiate(projectile, transform.position, transform.rotation);
            StartCoroutine(Timeout(timeoutBetweenBullets));
        }
    }

    IEnumerator Timeout(float seconds)
    {
        _alreadyShot = true;
        yield return new WaitForSeconds(seconds);
        _alreadyShot = false;
    }
}