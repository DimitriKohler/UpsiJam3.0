using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{

    private GameObject player;
    private GameObject camera;
    private CameraShake cameraShake;

    public Vector3 startingPosition; // the starting position of the enemy
    public float speed = 2f;
    public int lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        camera = GameObject.Find("Main Camera");
        cameraShake = camera.GetComponent<CameraShake>();
        transform.position = startingPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            lives -= 1;
            if (lives <= 0)
            {
                Destroy(this.gameObject);
                //cameraShake.shakeDuration = 2;
                //cameraShake.shakeMagnitude = 4;
                cameraShake.Shake();
            }
        }
    }

}
