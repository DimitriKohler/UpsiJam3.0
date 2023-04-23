using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnPauseGame : MonoBehaviour
{
    public GameObject pauseOverlay;
    
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(UnPause);
    }

    void UnPause()
    {
        Debug.Log("unpause");
        Time.timeScale = 1;
        //Constants.Constant.PAUSE = false;
        pauseOverlay.SetActive(false);
    }
}
