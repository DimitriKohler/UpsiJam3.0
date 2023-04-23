using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleKnightController : MonoBehaviour
{

    private GameObject player;   
    private bool canMove = false;
    private bool cdHurt = false;
    private bool isDead = false;

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
        
        StartCoroutine(WaitAndStartMoving());
       
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

            if (lives <= 0 && !isDead)
            {
                isDead = true;

                _deathAudioSource.Play();

                this.gameObject.GetComponent<Collider2D>().enabled = false;
                int childCount = this.gameObject.transform.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    this.gameObject.transform.GetChild(i).gameObject.SetActive(false);
                }

                Destroy(this.gameObject, 1f);
            }
            else
            {
                if (_hitAudioSource)
                {
                    _hitAudioSource.Play();
                }
            }
        }
    }
}
