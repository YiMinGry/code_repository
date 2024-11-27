using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : AnimalBase
{
    protected override void ChangeState(Motion newState)
    {
        if (newState == Motion.Walk || newState == Motion.Run) 
        {
            newState = Motion.Swim;
        }

        currentState = newState;

        if (animator != null && newState != Motion.ReturnToOriginal)
        {
            animator.Play(newState.ToString()); // ���� �̸��� ������ �ִϸ��̼� Ʈ���Ÿ� ����
        }
    }
}
