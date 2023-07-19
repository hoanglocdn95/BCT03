using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private Transform objectFollowed;
    private float smoothTime = 0.15f;
    private Vector3 velocity = Vector3.zero;

    private static BackgroundMovement instance;
    public BackgroundMovement Instance { get => instance; }

    private void Awake()
    {
        if (BackgroundMovement.instance == null)
        {
            BackgroundMovement.instance = this;
        }
    }

    private void Update()
    {
        if (objectFollowed)
        {
            transform.position = Vector3.SmoothDamp(transform.position, objectFollowed.position, ref velocity, smoothTime);
        }
    }
}
