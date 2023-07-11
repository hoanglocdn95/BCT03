using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform objectFollowed;
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.15f;
    private Vector3 velocity = Vector3.zero;

    private static CameraFollow instance;
    public CameraFollow Instance { get => instance; }

    private void Awake()
    {
        if (CameraFollow.instance == null)
        {
            CameraFollow.instance = this;
        }
    }

    private void Update()
    {
        Vector3 targetPosition = objectFollowed.position + offset;
        transform.parent.position = Vector3.SmoothDamp(transform.parent.position, targetPosition, ref velocity, smoothTime);
    }
}
