using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private float timeToTrigger = 10f;
    [SerializeField] private float triggerDuration = 10f;
    [SerializeField] private float counter = 0f;
    public bool isTriggered = false;
    private Transform iconModel;

    private void Awake()
    {
        iconModel = transform.Find("Model");
    }

    private void FixedUpdate()
    {
        if (transform.parent.gameObject.activeInHierarchy)
        {
            counter += Time.fixedDeltaTime;
            if (counter >= timeToTrigger && !isTriggered)
            {
                isTriggered = true;
                counter = 0;
                SetEnemyStatus();
            }

            if (counter >= triggerDuration && isTriggered)
            {
                isTriggered = false;
                counter = 0;
                SetEnemyStatus();
            }
        }
    }

    private void SetEnemyStatus ()
    {
        iconModel.gameObject.SetActive(isTriggered);
    }
}
