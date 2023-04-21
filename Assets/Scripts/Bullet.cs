using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(speed * Time.deltaTime, 0, 0);
        transform.Translate(-movement);
    }
}
