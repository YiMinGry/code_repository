using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAutoSave : MonoBehaviour
{
    [SerializeField] private Image icon;

    [SerializeField] private float fadeDuration = 1f;

    public void Play()
    {
        StartCoroutine(FadeSequence());
    }

    private IEnumerator FadeSequence()
    {
        // 페이드 인
        yield return StartCoroutine(CO_Fade(1f, fadeDuration));

        // 페이드 아웃
        yield return StartCoroutine(CO_Fade(0f, fadeDuration));
    }

    private IEnumerator CO_Fade(float targetAlpha, float duration)
    {
        float timeElapsed = 0f;

        Color initialColor = icon.color;
        float startAlpha = initialColor.a;

        while (timeElapsed < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / duration);

            icon.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        icon.color = new Color(initialColor.r, initialColor.g, initialColor.b, targetAlpha);
    }
}
