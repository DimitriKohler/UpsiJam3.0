using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseOverlay;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseOverlay.activeInHierarchy)
        {
            Time.timeScale = 0;
            //Constants.Constant.PAUSE = true;
            pauseOverlay.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseOverlay.activeInHierarchy)
        {
            Time.timeScale = 1;
            //Constants.Constant.PAUSE = false;
            pauseOverlay.SetActive(false);
        }
    }
}
