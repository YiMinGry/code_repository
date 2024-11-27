using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolHandler : MonoBehaviour
{
    [SerializeField]
    private List<ToolEventBase> tools = new List<ToolEventBase>();
    private ToolEventBase activeTool;

    public bool isPlaying
    {
        get
        {
            return activeTool != null && activeTool.isPlaying;
        }
    }

    public void EnableTool(ToolType toolType)
    {
        if (activeTool != null && activeTool.isPlaying)
        {
            Debug.Log("A tool is already in use.");
            return;
        }

        activeTool = tools.Find(tool => tool.toolType == toolType);

        if (activeTool != null)
        {
            activeTool.gameObject.SetActive(true);
            StartCoroutine(CO_WaitTool());
        }
        else
        {
            Debug.LogWarning($"Tool of type {toolType} not found in the tools list.");
        }
    }


    public void PlayTool(ToolType toolType)
    {
        if (activeTool == null)
        {
            Debug.Log("activeTool is null");
            return;
        }

        if (activeTool.toolType != toolType)
        {
            Debug.Log("miss math tool");
            return;
        }

        if (activeTool.isPlaying)
        {
            Debug.Log("A tool is already in use.");
            return;
        }

        activeTool.Play();
    }

    public IEnumerator CO_WaitTool()
    {
        if (activeTool != null)
        {
            yield return new WaitUntil(() => activeTool.isPlaying == true);

            yield return new WaitUntil(() => activeTool.isPlaying == false);
            activeTool.gameObject.SetActive(false);
            activeTool = null;
        }
        yield break;
    }
}
