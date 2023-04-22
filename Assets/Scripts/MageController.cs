using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageController : MonoBehaviour
{

    private GameObject player;
    private GameObject camera;
    private CameraShake cameraShake;
    private bool canMove = false;
    private Animator mageAnimator;
    private bool cdHurt = false;
    private bool isDead = false;
    [SerializeField] private GameObject cloudDeathPrefab;
    


    private AudioSource _hitAudioSource;
    private AudioSource _deathAudioSource;

    public float moveSpeed = 3f; // the speed at which the enemy moves
    public float moveRange = 5f; // the maximum distance the enemy can move from its starting position
    public float moveTime = 2f; // the amount of time the enemy spends moving in one direction before changing direction
    public int lives = 3;
    public float _shakeMagnitude = 0.1f;
    public float _shakeDuration = 0.5f;
    public bool _resetCamera = true;

    private Vector3 startingPosition; // the starting position of the enemy
    private Vector3 targetPosition; // the target position the enemy is moving towards
    private float moveTimer; // the timer for how long the enemy has been moving in one direction


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        camera = GameObject.Find("Main Camera");
        cameraShake = camera.GetComponent<CameraShake>();
        startingPosition = transform.position;
        targetPosition = GetRandomTargetPosition();
        StartCoroutine(WaitAndStartMoving());
        mageAnimator = gameObject.GetComponentInChildren<Animator>();
        _hitAudioSource = GetComponents<AudioSource>()[0];
        _deathAudioSource = GetComponents<AudioSource>()[1];
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
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // If the enemy has reached the target position, get a new target position
            if (transform.position == targetPosition)
            {
                targetPosition = GetRandomTargetPosition();
            }

            // Increment the move timer
            moveTimer += Time.deltaTime;

            // If the move timer has reached the move time, get a new target position
            if (moveTimer >= moveTime)
            {
                targetPosition = GetRandomTargetPosition();
                moveTimer = 0f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") && !cdHurt)
        {
            lives -= 1;
            StartCoroutine(HurtAnimation());
            if (lives <= 0 && !isDead)
            {
                isDead = true;

                // Death cloud
                Instantiate(cloudDeathPrefab, transform.position, Quaternion.identity);
                _deathAudioSource.Play();

                this.gameObject.GetComponent<Collider2D>().enabled = false;
                int childCount = this.gameObject.transform.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    this.gameObject.transform.GetChild(i).gameObject.SetActive(false);
                }

                Destroy(this.gameObject, 1f);
                cameraShake.shakeDuration = _shakeDuration;
                cameraShake.shakeMagnitude = _shakeMagnitude;
                cameraShake.resetCamera = _resetCamera;
                cameraShake.Shake();
            }
            else
            {
                _hitAudioSource.Play();
            }
        }
    }
    private Vector3 GetRandomTargetPosition()
    {
        // Get a random point inside a unit circle and multiply it by the move range to get a random target position within the move range
        Vector3 randomPoint = Random.insideUnitCircle.normalized * moveRange;
        randomPoint.z = 0;

        // Add the random point to the starting position to get the final target position
        return startingPosition + randomPoint;
    }


    IEnumerator HurtAnimation()
    {
        mageAnimator.SetBool("isHurt", true);
        cdHurt = true;
        yield return new WaitForSeconds(0.2f);
        mageAnimator.SetBool("isHurt", false);
        cdHurt = false;

    }
}
