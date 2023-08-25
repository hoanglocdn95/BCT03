using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowRocketSkill : Skills
{
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float distanceDespawn = 20f;
    private Vector3 initPosition;
    private Vector3 endPosition;
    private CapsuleCollider2D cc;

    private void Awake()
    {
        cc = transform.GetComponent<CapsuleCollider2D>();
        cc.isTrigger = false;
    }

    private void OnEnable()
    {
        initPosition = transform.position;

        float dirX = PlayerMovement.Instance.isFacingRight ? initPosition.x + distanceDespawn : initPosition.x - distanceDespawn;
        endPosition = new Vector3(dirX, initPosition.y, initPosition.z);

        transform.rotation = Quaternion.Euler(0, 0, PlayerMovement.Instance.isFacingRight ? -90f : 90f);
    }

    private void OnDisable()
    {
        transform.rotation = new Quaternion();
    }

    private void Update()
    {
        RocketMoving();
        DespawnByDistance();
    }

    public void DespawnByDistance()
    {
        float distance = Vector3.Distance(initPosition, transform.position);
        if (distance == distanceDespawn)
        {
            FxManager.Instance.Spawn(transform.position, new Quaternion(), 0);
            SkillManager.Instance.PutObjectInPool(transform);
        }
    }

    protected virtual void RocketMoving()
    {
        transform.position = Vector2.MoveTowards(transform.position, endPosition, Time.deltaTime * movementSpeed);
    }
}
