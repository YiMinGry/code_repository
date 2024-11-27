using StylizedWater2;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(BoxCollider))]
public class AutoScaleWaterChild : MonoBehaviour
{
    private Transform objectTransform;
    private BoxCollider boxCollider;
    private Transform waterObject;
    private Transform waterCheck;
    private Vector3 previousScale;

    private void Awake()
    {
        objectTransform = transform;
        boxCollider = GetComponent<BoxCollider>();
        waterObject = transform.Find("WaterObject");
        waterCheck = transform.Find("WaterCheck");
        UpdateBoxColliderSize();
    }

    private void Update()
    {
        if (objectTransform.localScale != previousScale)
        {
            UpdateBoxColliderSize();
            previousScale = objectTransform.localScale;
        }
    }

    private void UpdateBoxColliderSize()
    {
        if (boxCollider != null && objectTransform != null)
        {
            Vector3 newSize = objectTransform.localScale * 50f;
            boxCollider.size = newSize;
            waterObject.localScale = objectTransform.localScale / 2;
            waterCheck.localScale = newSize;
        }
    }
}
