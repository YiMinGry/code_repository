using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : AnimalBase
{
    void Start()
    {
        rig.useGravity = false;
        transform.position = new Vector3(originalPosition.x, originalPosition.y + Random.Range(5, 10), originalPosition.z);
    }

    protected override void ChangeState(Motion newState)
    {
        if (newState == Motion.Walk || newState == Motion.Run)
        {
            newState = Motion.Fly;
        }

        currentState = newState;

        if (animator != null && newState != Motion.ReturnToOriginal)
        {
            animator.Play(newState.ToString()); // ���� �̸��� ������ �ִϸ��̼� Ʈ���Ÿ� ����
        }
    }
}
