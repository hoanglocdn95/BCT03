using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArmorSkill : Skills
{
    private CircleCollider2D circleCollider;

    [SerializeField] private float originalScale;
    [SerializeField] private float ratioScale = 3;

    [SerializeField] private float scaleTime = 1.5f;
    [SerializeField] private float counter = 0f;
    [SerializeField] private bool isScaling = false;
    [SerializeField] private bool isScaledCompleted = false;

    [SerializeField] private float durationTime = 1.5f;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.isTrigger = true;
    }

    private void OnEnable()
    {
        transform.localScale = this.player.localScale;
        Debug.Log($"transform: {transform.localScale.x}");
        originalScale = this.player.localScale.x;

        counter = 0;
        isScaling = true;
    }

    private void Update()
    {
        transform.position = player.position;
    }

    private void FixedUpdate()
    {

        if (counter < scaleTime && isScaling)
        {
            counter += Time.fixedDeltaTime;
            Scaling();
        }

        if (counter >= scaleTime)
        {
            counter = 0;
            isScaling = false;
            isScaledCompleted = true;
        }

        if (isScaledCompleted)
        {
            isScaledCompleted = false;
            StartCoroutine(DespawnArmor());
        }
    }

    private void OnDisable()
    {
        isScaling = false;
    }

    private void Scaling()
    {
        float currentRatio = counter / (scaleTime - 0) + originalScale;
        Debug.Log($"currentRatio: {currentRatio}");

        currentRatio = currentRatio > originalScale * ratioScale ? originalScale * ratioScale : currentRatio;

        transform.localScale = new Vector3(currentRatio, currentRatio, transform.localScale.z);
    }

    private IEnumerator DespawnArmor()
    {
        yield return new WaitForSeconds(durationTime);
        SkillManager.Instance.PutObjectInPool(transform);
    }
}
