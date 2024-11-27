using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteAlways]
public class CircularLayout : LayoutGroup
{
    public float radius = 100f; // ���� ������
    public bool clockwise = true; // �ð� ���� ����
    public float arcAngle = 180f; // ��ġ���� ���� ���� ����

    protected override void OnEnable()
    {
        base.OnEnable();
        CalculateLayout();
        StartCoroutine(CO_Start());
    }

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        CalculateLayout();
    }

    public override void CalculateLayoutInputVertical()
    {
        CalculateLayout();
    }

    public override void SetLayoutHorizontal() { }

    public override void SetLayoutVertical() { }

    private void CalculateLayout()
    {
        // Ȱ��ȭ�� �ڽ� ������Ʈ�� ���͸�
        var activeChildren = GetActiveChildren();

        if (activeChildren.Count == 1)
        {
            ArrangeSingle(activeChildren[0]);
        }
        else if (activeChildren.Count <= 4)
        {
            ArrangeInArc(activeChildren);
        }
        else
        {
            ArrangeInCircle(activeChildren);
        }
    }

    private List<RectTransform> GetActiveChildren()
    {
        var activeChildren = new List<RectTransform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i) as RectTransform;
            if (child != null && child.gameObject.activeSelf) // Ȱ��ȭ�� ������Ʈ�� �߰�
            {
                activeChildren.Add(child);
            }
        }
        return activeChildren;
    }

    private void ArrangeSingle(RectTransform child)
    {
        if (child == null) return;

        Quaternion originalRotation = child.localRotation;

        // �߾ӿ� ��ġ
        child.localPosition = Vector3.zero;
        child.localRotation = originalRotation; // �����̼� ����
    }

    private void ArrangeInArc(List<RectTransform> activeChildren)
    {
        int childCount = activeChildren.Count;
        float angleStep = arcAngle / (childCount - 1); // ��ġ�� ���� �й�
        float startAngle = -arcAngle / 2; // �߾� �������� �й�

        for (int i = 0; i < childCount; i++)
        {
            RectTransform child = activeChildren[i];
            Quaternion originalRotation = child.localRotation;

            float angle = startAngle + angleStep * i;
            Vector3 position = new Vector3(
                Mathf.Sin(angle * Mathf.Deg2Rad) * radius,
                Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                0f
            );

            child.localPosition = position;
            child.localRotation = originalRotation; // �����̼� ����
        }
    }

    private void ArrangeInCircle(List<RectTransform> activeChildren)
    {
        int childCount = activeChildren.Count;
        float angleStep = 360f / childCount; // ���� ���� �й�

        for (int i = 0; i < childCount; i++)
        {
            RectTransform child = activeChildren[i];
            Quaternion originalRotation = child.localRotation;

            float angle = angleStep * i * (clockwise ? -1 : 1);
            Vector3 position = new Vector3(
                Mathf.Sin(angle * Mathf.Deg2Rad) * radius,
                Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                0f
            );

            child.localPosition = position;
            child.localRotation = originalRotation; // �����̼� ����
        }
    }

    public IEnumerator CO_Start()
    {
        Vector3 initialScale = Vector3.zero;
        Vector3 targetScale = Vector3.one;

        float duration = 0.3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            foreach (RectTransform obj in GetActiveChildren())
            {
                obj.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            }

            yield return null;
        }

        foreach (RectTransform obj in GetActiveChildren())
        {
            obj.transform.localScale = targetScale;
        }
    }
}
