using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlockController : MonoBehaviour
{
    public int lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            lives -= 1;
            if (lives <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
