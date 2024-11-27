using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILogItem : MonoBehaviour
{
    [SerializeField] private float waitSeconds = 5f;
    [SerializeField] private float fadeDuration = 1f;

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI contentText;

    public void Set(Sprite sprite, string msg, bool isAlert = false)
    {
        icon.sprite = sprite;
        contentText.text = msg;

        gameObject.SetActive(true);

        StartCoroutine(CO_FadeOutAndDestroy());
    }

    private IEnumerator CO_FadeOutAndDestroy()
    {
        yield return new WaitForSeconds(waitSeconds);

        float timeElapsed = 0f;

        Color initialColor = icon.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0);

        while (timeElapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(initialColor.a, targetColor.a, timeElapsed / fadeDuration);

            icon.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            if (contentText != null)
            {
                contentText.color = new Color(contentText.color.r, contentText.color.g, contentText.color.b, alpha);
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
