using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Hurt"))
        {
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("HurtBullet"))
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
