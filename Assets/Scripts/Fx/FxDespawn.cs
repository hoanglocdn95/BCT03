using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxDespawn : MonoBehaviour
{
    [SerializeField] private float delayTime = 2f;
    [SerializeField] private float counter = 0f;

    private void FixedUpdate()
    {
        if (this.CanDespawn())
        {
            FxManager.Instance.PutObjectInPool(transform);
        }
    }

    private bool CanDespawn()
    {
        if (this.counter > this.delayTime)
        {
            this.counter = 0f;
            return true;
        }

        this.counter += Time.fixedDeltaTime;
        return false;
    }
}
