using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
    public UIType uiType;
    public Button[] buttons;
    private int currentIndex = 0;

    [SerializeField] protected TextMeshProUGUI titleText;
    [SerializeField] protected TextMeshProUGUI contentText;

    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();

        buttons = transform.GetComponentsInChildren<Button>();
        if (buttons.Length > 0)
        {
            EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
        }
    }

    private void Update()
    {
        if (buttons.Length > 0 && UIHelper.Instance.IsUIActiveCount > 0)
        {
            GameObject activePopup = UIHelper.Instance.GetMostRecentUI();

            if (activePopup != null)
            {
                GameObject selected = EventSystem.current.currentSelectedGameObject;
                if (selected == null || !selected.transform.IsChildOf(activePopup.transform))
                {
                    EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
                }
            }
        }
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);

        if (animator != null)
        {
            animator.Play("Open");
        }

        Debug.Log("UI opened.");
    }


    public virtual void Close()
    {
        if (animator != null)
        {
            animator.Play("Close");
            Destroy(gameObject, 0.3f);
        }
        else
        {
            Destroy(gameObject);
        }

        Debug.Log("UI closed.");
    }
    public void OnClose()
    {
        UIHelper.Instance.CloseUI(uiType);
    }

    public void InvokeConfirm()
    {
        GameObject selectedGameObject = EventSystem.current.currentSelectedGameObject;
        if (selectedGameObject != null)
        {
            Button button = selectedGameObject.GetComponent<Button>();

            if (button != null)
            {
                button.onClick.Invoke();
            }
        }
        else
        {
            OnClose();
        }

        Debug.Log(gameObject.name + ": InvokeConfirm");
    }
    public void InvokeCancel()
    {
        OnClose();
        Debug.Log(gameObject.name + ": InvokeCancel");
    }
}
