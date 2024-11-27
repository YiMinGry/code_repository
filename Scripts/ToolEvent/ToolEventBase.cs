using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolType
{
    fishingRod,
    guitar,
    shovel,
    axe,
    objectRooting,
    piano
}

public class ToolEventBase : MonoBehaviour
{
    private Animator animator;
    public ToolType toolType;
    public bool isPlaying = false;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void Play()
    {
        if (animator != null)
        {
            animator.SetTrigger("Start");
        }
    }

    public void OnStart()
    {
        isPlaying = true;
    }

    public void OnEnd()
    {
        isPlaying = false;
    }
}
