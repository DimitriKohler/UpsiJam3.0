using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;
    public bool resetCamera = true;

    private Vector3 originalPosition = new Vector3(0,0,-10);
    private float timeLeft = 0f;

    void Update()
    {
        if (timeLeft > 0)
        {
            // Shake the camera by a random amount in each direction
            Vector3 shakePosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;
            transform.position = new Vector3(shakePosition.x, shakePosition.y, -10);

            // Decrease the remaining shake time
            timeLeft -= Time.deltaTime;
        }
        else if (resetCamera)
        {
            // Reset the camera position if the shake is finished
            transform.position = new Vector3(0, 0, -10);
        }
    }

    public void Shake()
    {
        // Start the screen shake effect
        originalPosition = transform.position;
        timeLeft = shakeDuration;
    }
}
