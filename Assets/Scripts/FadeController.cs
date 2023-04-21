using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public float fadeTime = 0.5f;

    private Image fadeImage;

    void Start()
    {
        fadeImage = GetComponent<Image>();
        Debug.Log(fadeImage);
    }

    public void FadeToBlack()
    {
        StartCoroutine(FadeToBlackCoroutine());
    }

    public void FadeToScene()
    {
        StartCoroutine(FadeToSceneCoroutine());
    }

    private IEnumerator FadeToBlackCoroutine()
    {
        float elapsedTime = 0f;
        Color startColor = fadeImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < fadeTime)
        {
            float t = elapsedTime / fadeTime;
            fadeImage.color = Color.Lerp(startColor, endColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = endColor;
    }

    private IEnumerator FadeToSceneCoroutine()
    {
        float elapsedTime = 0f;
        Color startColor = fadeImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < fadeTime)
        {
            float t = elapsedTime / fadeTime;
            fadeImage.color = Color.Lerp(startColor, endColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = endColor;
    }
}
