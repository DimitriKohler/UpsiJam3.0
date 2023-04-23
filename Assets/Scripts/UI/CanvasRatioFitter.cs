using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CanvasRatioFitter : MonoBehaviour
{
    private CanvasScaler _canvasScaler;

    private void Start()
    {
        _canvasScaler = GetComponent<CanvasScaler>();
    }

    
    private void OnGUI()
    {
        float aspectRatio = Screen.width / Screen.height;
        if (aspectRatio > 1)
        {
            _canvasScaler.matchWidthOrHeight = 1;
        }
        else if (aspectRatio <= 1)
        {
            _canvasScaler.matchWidthOrHeight = 0;
        }
    }
}
