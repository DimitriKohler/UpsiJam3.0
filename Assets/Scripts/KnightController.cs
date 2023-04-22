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


    private AudioSource _hitAudioSource;
    private AudioSource _deathAudioSource;

    public float speed = 2f;
    public int lives = 3;
    public float _shakeMagnitude = 0.1f;
    public float _shakeDuration = 0.5f;
    public bool _resetCamera = true;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        camera = GameObject.Find("Main Camera");
        cameraShake = camera.GetComponent<CameraShake>();
        startingPosition = transform.position;
        StartCoroutine(WaitAndStartMoving());
        knightAnimator = gameObject.GetComponentInChildren<Animator>();
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
                _deathAudioSource.Play();

                this.gameObject.GetComponent<Collider2D>().enabled = false;
                int childCount = this.gameObject.transform.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
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

    IEnumerator HurtAnimation()
    {
        knightAnimator.SetBool("isHurt", true);
        cdHurt = true;
        yield return new WaitForSeconds(0.2f);
        knightAnimator.SetBool("isHurt", false);
        cdHurt = false;

    }

}
