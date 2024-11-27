using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIGuitar : UIBase
{
    [SerializeField] private GameObject[] icons;
    [SerializeField] private GameObject[] keyIcons;
    public void OnEnable()
    {
        bool isGamePadConnected = Main.Instance.player.controller.input.isGamePadConnected;

        foreach (var icon in icons)
        {
            icon.SetActive(isGamePadConnected);
        }

        foreach (var keyIcon in keyIcons)
        {
            keyIcon.SetActive(!isGamePadConnected);
        }

        EventSystem.current.SetSelectedGameObject(null);
    }

    private void Awake()
    {
        EventManager.Regist("UIGuitarPlay0", () => { PlayButton(0); });
        EventManager.Regist("UIGuitarPlay1", () => { PlayButton(1); });
        EventManager.Regist("UIGuitarPlay2", () => { PlayButton(2); });
        EventManager.Regist("UIGuitarPlay3", () => { PlayButton(3); });
        EventManager.Regist("UIGuitarPlay4", () => { PlayButton(4); });
        EventManager.Regist("UIGuitarPlay5", () => { PlayButton(5); });
        EventManager.Regist("UIGuitarPlay6", () => { PlayButton(6); });
    }

    public void PlayButton(int idx)
    {
        if (buttons.Length > idx)
        {
            StartCoroutine(CO_ButtonFlash(buttons[idx]));
        }
    }


    private IEnumerator CO_ButtonFlash(Button button)
    {
        Image buttonImage = button.targetGraphic.GetComponent<Image>();

        if (buttonImage == null)
        {
            Debug.LogWarning("Button does not have an Image component!");
            yield break;
        }

        Color originalColor = Color.white;

        Color targetColor = Color.gray;

        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration / 2)
        {
            elapsedTime += Time.deltaTime;
            buttonImage.color = Color.Lerp(originalColor, targetColor, elapsedTime / (duration / 2));
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < duration / 2)
        {
            elapsedTime += Time.deltaTime;
            buttonImage.color = Color.Lerp(targetColor, originalColor, elapsedTime / (duration / 2));
            yield return null;
        }

        buttonImage.color = originalColor;
    }
}
