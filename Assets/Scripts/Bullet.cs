using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(speed * Time.deltaTime, 0, 0);
        transform.Translate(-movement);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Hurt"))
        {
            Destroy(this.gameObject);
        }
    }
}
