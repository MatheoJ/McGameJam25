using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkEffect : MonoBehaviour
{
    [Header("Reference to the full-screen black Image")]
    public Image blinkImage;

    [Header("Blink timing settings")]
    public float fadeDuration = 0.15f; // how long it takes to fade in/out
    public float holdBlackDuration = 0.1f; // how long it stays fully black

    /// <summary>
    /// Public method to initiate the blink coroutine.
    /// </summary>
    public void StartBlink()
    {
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        // 1) Fade In to black
        float t = 0f;
        Color c = blinkImage.color; // assume black
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            c.a = alpha;
            blinkImage.color = c;
            yield return null;
        }

        // Ensure fully black
        c.a = 1f;
        blinkImage.color = c;

        // 2) Hold black for a short time
        yield return new WaitForSeconds(holdBlackDuration);

        // 3) Fade Out back to transparent
        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            c.a = alpha;
            blinkImage.color = c;
            yield return null;
        }

        // Ensure fully transparent
        c.a = 0f;
        blinkImage.color = c;
    }
}

