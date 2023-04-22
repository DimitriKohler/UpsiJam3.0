using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBars : MonoBehaviour
{
    private GameObject ennemies;

    // Start is called before the first frame update
    void Start()
    {
        ennemies = GameObject.Find("Ennemies");
    }

    // Update is called once per frame
    void Update()
    {
        if (ennemies.transform.childCount == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
