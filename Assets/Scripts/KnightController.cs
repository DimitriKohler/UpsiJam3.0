using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{

    private GameObject player;
    private GameObject camera;
    private CameraShake cameraShake;
    private Vector3 startingPosition; // the starting position of the enemy
    private bool canMove = false;
    private Animator knightAnimator;
    private bool cdHurt = false;
    [SerializeField] private GameObject cloudDeathPrefab;

    public float speed = 2f;
    public int lives = 3;

    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        camera = GameObject.Find("Main Camera");
        cameraShake = camera.GetComponent<CameraShake>();
        startingPosition = transform.position;
        StartCoroutine(WaitAndStartMoving());
        knightAnimator = gameObject.GetComponentInChildren<Animator>();
    }

    IEnumerator WaitAndStartMoving()
    {
        yield return new WaitForSeconds(0.5f);
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") && !cdHurt)
        {
            lives -= 1;
            StartCoroutine(HurtAnimation());

            if (lives <= 0)
            {
                // Death cloud
                Instantiate(cloudDeathPrefab, transform.position, Quaternion.identity);

                Destroy(this.gameObject);
                //cameraShake.shakeDuration = 2;
                //cameraShake.shakeMagnitude = 4;
                cameraShake.Shake();
            }
        }
    }

    IEnumerator HurtAnimation()
    {
        knightAnimator.SetBool("isHurt", true);
        cdHurt = true;
        yield return new WaitForSeconds(0.2f);
        knightAnimator.SetBool("isHurt", false);
        cdHurt = false;

    }

}
