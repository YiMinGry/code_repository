using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;
#endif

using System.Collections;
using System.Collections.Generic;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;
    public float zoom;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    public bool isGamePadConnected = false;

    void Awake()
    {
        StartCoroutine(CheckForControllers());
    }

#if ENABLE_INPUT_SYSTEM
    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            LookInput(value.Get<Vector2>());
        }
    }

    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }

    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }

    public void OnZoom(InputValue value)
    {
        ZoomInput(value.Get<Vector2>().y);
        EventManager.Invoke("InputZoom");
    }

    public void OnFnc(InputValue value)
    {
        Debug.Log("OnFnc");
        EventManager.Invoke("InputFunction");
    }
    public void OnInven(InputValue value)
    {
        Debug.Log("OnInven");
        EventManager.Invoke("InputInven");
    }
    public void OnMenu(InputValue value)
    {
        Debug.Log("OnMenu");
        EventManager.Invoke("InputMenu");
    }

    public void OnTab(InputValue value)
    {
        Debug.Log("OnTab");
        EventManager.Invoke("InputTab");
    }

    #region guitarmode
    public void OnGuitarC(InputValue value)
    {
        if (value.isPressed)
        {
            EventManager.Invoke("OnGuitarC");
        }
    }
    public void OnGuitarD(InputValue value)
    {
        if (value.isPressed)
        {
            EventManager.Invoke("OnGuitarD");
        }
    }
    public void OnGuitarE(InputValue value)
    {
        if (value.isPressed)
        {
            EventManager.Invoke("OnGuitarE");
        }
    }
    public void OnGuitarF(InputValue value)
    {
        if (value.isPressed)
        {
            EventManager.Invoke("OnGuitarF");
        }
    }
    public void OnGuitarG(InputValue value)
    {
        if (value.isPressed) 
        { 
            EventManager.Invoke("OnGuitarG");
        }
    }
    public void OnGuitarA(InputValue value)
    {
        if (value.isPressed) 
        { 
            EventManager.Invoke("OnGuitarA");
        }
    }
    public void OnGuitarB(InputValue value)
    {
        if (value.isPressed) 
        { 
            EventManager.Invoke("OnGuitarB");
        }
    }
    public void OnGuitarStop(InputValue value)
    {
        if (value.isPressed)
        {
            EventManager.Invoke("OnGuitarStop");
        }
    }
    #endregion

    #region uimode
    public void OnUIOK(InputValue value)
    {
        if (value.isPressed) 
        {
            UIHelper.Instance.ConfirmMostRecentUI();
        }
    }
    public void OnUINO(InputValue value)
    {
        if (value.isPressed) 
        {
            UIHelper.Instance.CancelMostRecentUI();
        }
    }
    #endregion

    #region uimode
    #endregion

    #region uimode
    #endregion

    #region uimode
    #endregion

    #region uimode
    #endregion

    #region uimode
    #endregion

#endif


    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    public void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }

    public void ZoomInput(float newZoomValue)
    {
        zoom = newZoomValue;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !newState;
    }

    public void CursorLock(bool locked)
    {
        SetCursorState(locked);

        cursorLocked = locked;
        cursorInputForLook = locked;
    }

    IEnumerator CheckForControllers()
    {
        while (true)
        {
            var controllers = Input.GetJoystickNames();

            // 빈 문자열 필터링
            int validControllerCount = 0;
            foreach (var controller in controllers)
            {
                if (!string.IsNullOrEmpty(controller))
                {
                    validControllerCount++;
                }
            }

            if (!isGamePadConnected && validControllerCount > 0)
            {
                isGamePadConnected = true;
                Debug.Log("Connected");
            }
            else if (isGamePadConnected && validControllerCount == 0)
            {
                isGamePadConnected = false;
                Debug.Log("Disconnected");
            }

            yield return new WaitForSeconds(1f);
        }
    }
}