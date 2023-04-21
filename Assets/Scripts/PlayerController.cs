using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;

    public GameObject roomManager;
    private RoomManagerScript roomManagerScript;

    private float horizontalMovement;
    private float verticalMovement;

    private bool isMovable = true;

    private FadeController fadeController;

    // Start is called before the first frame update
    void Start()
    {
        fadeController = FindObjectOfType<FadeController>();
        roomManagerScript = roomManager.GetComponent<RoomManagerScript>();
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        verticalMovement = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {         
        if (isMovable)
        {
            transform.Translate(horizontalMovement, verticalMovement, 0);
        }
    }
    /*
    void OnCollisionEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
        }
    }
    */
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

    IEnumerator ResetPlayerPosition(Vector3 playerPos)
    {
        fadeController.FadeToBlack();

        yield return new WaitForSeconds(0.5f);


        roomManagerScript.NextRoom();
        transform.position = playerPos;

        fadeController.FadeToScene();

        yield return new WaitForSeconds(0.5f);

        isMovable = true;
    }
}
