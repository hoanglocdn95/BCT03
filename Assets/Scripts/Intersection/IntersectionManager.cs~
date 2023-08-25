using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionManager : MonoBehaviour
{
    //[SerializeField] private bool isMoving = true;
    //[SerializeField] private bool isRotating = false;

    [SerializeField] private float delayTime = 6f;
    [SerializeField] private float counter = 0f;

    [SerializeField] private Transform targetRotation;

    private float rotationTime = 3f;

    private void FixedUpdate()
    {
        counter += Time.fixedDeltaTime;
        if (counter >= delayTime)
        {
            counter = 0f;
            StartCoroutine(RotateOverTime());
        }
    }

    private IEnumerator RotateOverTime()
    {
        float elapsedTime = 0f;
        float startAngle = transform.eulerAngles.z;
        float endAngle = startAngle + 90f; // Góc xoay mục tiêu

        while (elapsedTime < rotationTime)
        {
            float currentAngle = Mathf.Lerp(startAngle, endAngle, elapsedTime / rotationTime);
            transform.parent.eulerAngles = new Vector3(0f, 0f, currentAngle);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.parent.eulerAngles = new Vector3(0f, 0f, endAngle);
    }

}
