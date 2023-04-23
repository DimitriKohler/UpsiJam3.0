using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleBinImageScript : MonoBehaviour
{

    public float startScale = 0.1f;
    public float endScale = 0.5f;
    public float lerpSpeed = 0.5f;
    
    public Transform startTransform;
    public Transform endTransform;

    private float currentScale = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
    private void OnEnable()
    {
        StartCoroutine(LerpScale());
    }

    private IEnumerator LerpScale()
    {
        float elapsedTime = 0;

        while (elapsedTime < lerpSpeed)
        {
            currentScale = Mathf.Lerp(startScale, endScale, elapsedTime / lerpSpeed);
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            transform.position = Vector3.Lerp(startTransform.position, endTransform.position, elapsedTime / lerpSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = new Vector3(endScale, endScale, endScale);
        transform.position = endTransform.position;
    }
}


