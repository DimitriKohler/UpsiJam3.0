using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    public int lives = 3;
    public float immunityInSeconds;
    private bool isImmune = false;
    [SerializeField] private GameObject livesUI;
    [SerializeField] private GameObject heart;
    [SerializeField] private string gameLostScene;

    public GameObject roomManager;
    private RoomManagerScript roomManagerScript;

    // Movement components
    private float horizontalMovement;
    private float verticalMovement;

    private bool isMovable = true;
    private bool isReseting = false;

    private FadeController fadeController;
    private AudioSource _walkingAudioSource;
    private Animator _animator;
    private AudioSource _hurtAudioSource;
    private Collider2D _collider;
    private readonly Stack<GameObject> _hearts = new Stack<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        fadeController = FindObjectOfType<FadeController>();
        roomManagerScript = roomManager.GetComponent<RoomManagerScript>();
        _walkingAudioSource = GetComponent<AudioSource>();
        _hurtAudioSource = GetComponent<AudioSource>();
        _collider = GetComponents<Collider2D>()[0];
        _animator = GetComponentInChildren<Animator>();

        // Display as many hearts as there are lives
        for (int i = 0; i < lives; i++)
        {
            _hearts.Push(Instantiate(heart, livesUI.transform));
        }
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isMovable)
        {
            // https://answers.unity.com/questions/285476/maintain-direction-regardless-of-rotation.html
            transform.Translate(horizontalMovement * movementSpeed * Time.deltaTime, verticalMovement * movementSpeed * Time.deltaTime, 0, Space.World);
        }

        if (!isMovable || (horizontalMovement == 0 && verticalMovement == 0))
        {
            _walkingAudioSource.Stop();
        }
        else if (!_walkingAudioSource.isPlaying)
        {
            _walkingAudioSource.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Top"))
        {
            isMovable = false;            

            Vector3 bottomPos = new Vector3(0, -3.5f, 0); 

            StartCoroutine(ResetPlayerPosition(bottomPos));
        }
        else if(other.CompareTag("Bottom"))
        {
            isMovable = false;

            Vector3 topPos = new Vector3(0, 3.5f, 0);

            StartCoroutine(ResetPlayerPosition(topPos));
        }
        else if (other.CompareTag("Left"))
        {
            isMovable = false;            

            Vector3 rightPos = new Vector3(7.5f, 0, 0);

            StartCoroutine(ResetPlayerPosition(rightPos));
        }
        else if (other.CompareTag("Right"))
        {
            isMovable = false;

            Vector3 leftPos = new Vector3(-7.5f, 0, 0);

            StartCoroutine(ResetPlayerPosition(leftPos));
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Hurt") && !isImmune)
        {
            DecrementLives();
            StartCoroutine(Immunity());
            // Copy de Fried Phoenix

            //_animator.SetBool(IsHurt, true);
            //_hurtAudioSource.Play();

            //Destroy(_hearts.Pop());

            // Immunity
            isImmune = true;
            //_animator.SetBool(IsHurt, false);
        }
    }

    IEnumerator Immunity()
    {
        yield return new WaitForSeconds(immunityInSeconds);
        isImmune = false;
    }

    IEnumerator ResetPlayerPosition(Vector3 playerPos)
    {
        if (!isReseting)
        {
            isReseting = true;
            fadeController.FadeToBlack();

            yield return new WaitForSeconds(0.5f);


            roomManagerScript.NextRoom();
            transform.position = playerPos;

            fadeController.FadeToScene();

            yield return new WaitForSeconds(0.5f);

            isMovable = true;
            isReseting = false;
        }
    }
    
    private void DecrementLives()
    {
        lives--;
        Debug.Log(lives);
        if (lives <= 0)
        {
            SceneManager.LoadScene(gameLostScene, LoadSceneMode.Single);
        }
        Destroy(_hearts.Pop());
    }
}
