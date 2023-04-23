using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    public int lives = 3;
    public int maxLives = 20;
    public float immunityInSeconds;
    private bool isImmune = false;
    [SerializeField] private GameObject livesUI;
    [SerializeField] private GameObject heart;
    [SerializeField] private float heartScaleFactor = 1.2f;
    [SerializeField] private string gameLostScene;

    public GameObject roomManager;
    private RoomManagerScript roomManagerScript;

    // Movement components
    private float horizontalMovement;
    private float verticalMovement;

    public bool isMovable = true;
    private bool isReseting = false;
    private bool isHidden = false;

    private FadeController fadeController;
    private AudioSource _walkingAudioSource;
    private Animator _animator;
    private AudioSource _hurtAudioSource;
    private Collider2D _collider;
    private readonly Stack<GameObject> _hearts = new Stack<GameObject>();
    private GridLayoutGroup _livesLayout;
    
    // Start is called before the first frame update
    void Start()
    {
        fadeController = FindObjectOfType<FadeController>();
        roomManagerScript = roomManager.GetComponent<RoomManagerScript>();
        _walkingAudioSource = GetComponent<AudioSource>();
        _hurtAudioSource = GetComponent<AudioSource>();
        _collider = GetComponents<Collider2D>()[0];
        _animator = GetComponentInChildren<Animator>();

         _livesLayout = livesUI.GetComponent<GridLayoutGroup>();
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
            if (isHidden)
            {
                this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Classic Rooms
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

        // Gravity Room
        if (other.CompareTag("Gravity"))
        {
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.75f;
        }

        // Desktop Room
        if (other.CompareTag("Quit"))
        {
            isMovable = false;

            Vector3 iconPos = new Vector3(-4.81f, 0.96f, 0);

            StartCoroutine(ResetPlayerPositionDesktop(iconPos));
        }

        // Folder Room
        if (other.CompareTag("Folder"))
        {
            isMovable = false;

            Vector3 startPos = new Vector3(-3f, -0.4f, 0);

            StartCoroutine(ResetPlayerPositionFolder(startPos));
        }

        // The End
        if (other.CompareTag("End"))
        {
            // TODO replace by END scene
            SceneManager.LoadScene(gameLostScene, LoadSceneMode.Single);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isImmune)
        {
            return;
        }
        
        if (other.CompareTag("Hurt"))
        {
            TakeHit();
        }
        else if (other.CompareTag("HurtBullet"))
        {
            TakeHit();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Heart"))
        {
            IncrementLives();
            Destroy(other.gameObject);
        }
    }

    private void TakeHit()
    {
        DecrementLives();
        StartCoroutine(Immunity());

        // Immunity
        isImmune = true;
        //_animator.SetBool(IsHurt, false);
    }

    IEnumerator Immunity()
    {
        _animator.SetBool("isHurt", true);
        yield return new WaitForSeconds(immunityInSeconds);
        _animator.SetBool("isHurt", false);
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
            // Find all game objects with the "Bullet" tag
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
            GameObject[] hurtBullets = GameObject.FindGameObjectsWithTag("HurtBullet");
            GameObject[] trashes = GameObject.FindGameObjectsWithTag("Trash");

            // Loop through the bullets array and destroy each bullet
            foreach (GameObject bullet in bullets)
            {
                Destroy(bullet);
            }
            foreach (GameObject bullet in hurtBullets)
            {
                Destroy(bullet);
            }
            foreach (GameObject trash in trashes)
            {
                Destroy(trash);
            }

            transform.position = playerPos;
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

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
            return;
        }

        _livesLayout.cellSize /= heartScaleFactor;
        Destroy(_hearts.Pop());
    }
    
    private void IncrementLives()
    {
        if (lives >= maxLives)
        {
            return;
        }
        
        lives++;
        Debug.Log(lives);
        _livesLayout.cellSize *= heartScaleFactor;
        _hearts.Push(Instantiate(heart, livesUI.transform));
    }

    IEnumerator ResetPlayerPositionDesktop(Vector3 playerPos)
    {
        if (!isReseting)
        {
            isReseting = true;
            fadeController.FadeToBlack();

            yield return new WaitForSeconds(0.5f);

            roomManagerScript.NextRoom();
            // Find all game objects with the "Bullet" tag
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
            GameObject[] hurtBullets = GameObject.FindGameObjectsWithTag("HurtBullet");

            // Loop through the bullets array and destroy each bullet
            foreach (GameObject bullet in bullets)
            {
                Destroy(bullet);
            }
            foreach (GameObject bullet in hurtBullets)
            {
                Destroy(bullet);
            }

            transform.position = playerPos;
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            this.gameObject.transform.localScale = new Vector3(0.45f, 0.45f, 1f);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(false);

            livesUI.SetActive(false);

            fadeController.FadeToScene();

            yield return new WaitForSeconds(0.5f);

            isMovable = true;
            isReseting = false;
            isHidden = true;
        }
    }

    IEnumerator ResetPlayerPositionFolder(Vector3 playerPos)
    {
        if (!isReseting)
        {
            isReseting = true;
            fadeController.FadeToBlack();

            yield return new WaitForSeconds(0.5f);

            roomManagerScript.NextRoom();
            // Find all game objects with the "Bullet" tag
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
            GameObject[] hurtBullets = GameObject.FindGameObjectsWithTag("HurtBullet");

            // Loop through the bullets array and destroy each bullet
            foreach (GameObject bullet in bullets)
            {
                Destroy(bullet);
            }
            foreach (GameObject bullet in hurtBullets)
            {
                Destroy(bullet);
            }

            transform.position = playerPos;
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            this.gameObject.transform.localScale = new Vector3(0.45f, 0.45f, 1f);

            fadeController.FadeToScene();

            yield return new WaitForSeconds(0.5f);

            isMovable = true;
            isReseting = false;
            isHidden = true;
        }
    }

}
