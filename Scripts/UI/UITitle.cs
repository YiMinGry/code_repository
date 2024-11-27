using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITitle : UIBase
{
    [SerializeField] private Image title;
    [SerializeField] private Image bg;

    [SerializeField] private float fadeDuration = 1.5f;  // 페이드 인/아웃 속도를 설정하는 변수
    private bool isFading = false;

    private IEnumerator Start()
    {
        Main.Instance.player.SetActive(false);

        Main.Instance.camManager.SetCamera(Cams.Title);

        yield return StartCoroutine(CO_FadeIn(1f));

        yield return new WaitUntil(() => Input.anyKeyDown);

        Main.Instance.LoadGame();

        SoundManager.Instance.Play(SoundManager.enter, SoundType.Effect);

        yield return StartCoroutine(CO_FadeOut());

        Main.Instance.camManager.SetCamera(Cams.PlayerFollow);

        Main.Instance.dayNightTimer.SetTimeScale(1, 12, 0);
        Main.Instance.player.SetActive(true);
        UIHelper.Instance.CloseUI(UIType.UITitle);
        UIHelper.Instance.uILog.Add(UIHelper.Instance.GetIcon("Info"), "환영합니다.");
    }

    public IEnumerator CO_FadeIn(float startDelay = 0)
    {
        yield return new WaitForSeconds(startDelay);

        yield return CO_Fade(0f, 1f);  // 0에서 1로 페이드 인
    }

    public IEnumerator CO_FadeOut(float startDelay = 0)
    {
        yield return new WaitForSeconds(startDelay);

        yield return CO_Fade(1f, 0f);  // 1에서 0으로 페이드 아웃
    }

    private IEnumerator CO_Fade(float startAlpha, float endAlpha)
    {
        if (isFading) yield break;

        isFading = true;
        Color c = title.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            title.color = c;
            bg.color = c;
            yield return null;
        }

        c.a = endAlpha;
        title.color = c;
        title.color = c;

        contentText.color = c;
        isFading = false;
    }
}