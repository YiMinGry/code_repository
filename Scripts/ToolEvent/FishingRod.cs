using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class FishingRod : ToolEventBase
{
    public GameObject objectA;  // 첫 번째 오브젝트
    public GameObject objectB;  // 두 번째 오브젝트
    private LineRenderer lineRenderer;

    [SerializeField]
    private GameObject fx;
    [SerializeField]
    private GameObject fxLast;

    float rayDistance = 20f;

    Color enableColor = new Color(1f, 1f, 1f, 0.5f);

    Vector3 hitPos;
    Vector3 randomOffset;

    GameObject fxCopy;

    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.005f;

        lineRenderer.startColor = enableColor;
        lineRenderer.endColor = enableColor;

        lineRenderer.material = new Material(Shader.Find("Unlit/Texture"));
        lineRenderer.material.color = enableColor;
    }

    void Update()
    {
        if (objectB.activeInHierarchy)
        {
            if (Main.Instance.player.objectDetector.hitInfo.point != null)
            {
                Vector3 displayPos = Main.Instance.player.objectDetector.hitInfo.point;
                Debug.Log(Main.Instance.player.objectDetector.hitInfo);
                displayPos.y -= 0.3f;

                Vector3 targetPosition = displayPos;
                objectB.transform.position = Vector3.Lerp(objectB.transform.position, targetPosition, Time.deltaTime * 5);
                hitPos = displayPos;
            }


            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, objectA.transform.position);
            lineRenderer.SetPosition(1, objectB.transform.position);
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
    }

    public void OnFlinch()
    {
        SoundManager.Instance.Play("fishing", SoundType.Effect);

        float randomRadius = 0.3f;
        float randomHeight = 0.3f;

        randomOffset = new Vector3(
           Random.Range(-randomRadius, randomRadius),
           Random.Range(-randomHeight, 0),
           Random.Range(-randomRadius, randomRadius)
        );

        fxCopy = Instantiate(fx, hitPos, Quaternion.identity);
    }

    public void OnFlinchLast()
    {
        SoundManager.Instance.Play("fishing", SoundType.Effect);
        float randomRadius = 0.6f;
        float randomHeight = 0.6f;

        randomOffset = new Vector3(
           Random.Range(-randomRadius, randomRadius),
           Random.Range(-randomHeight, 0),
           Random.Range(-randomRadius, randomRadius)
        );

        fxCopy = Instantiate(fxLast, hitPos, Quaternion.identity);
    }
}
