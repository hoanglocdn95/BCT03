using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private CircleCollider2D cc;
    [SerializeField] private Transform player;
    [SerializeField] private bool isPlayerAvailable = false;
    [SerializeField] private bool isPlayerAttached = false;
    [SerializeField] private float dashSpeed = 6f;
    [SerializeField] private float attachedDistance = 1f;

    [SerializeField] private float followDurationTime = 5f;
    private float counter = 0;

    private void Awake()
    {
        cc = GetComponent<CircleCollider2D>();
        cc.isTrigger = true;
        cc.radius = 2.5f;
    }

    private void OnEnable()
    {
        isPlayerAvailable = false;
        isPlayerAttached = false;
    }

    private void Update()
    {
        if (isPlayerAttached)
        {
            float dirY = player.position.y + 1f + (player.localScale.y - 1) / 2;
            transform.parent.position = new Vector3(player.position.x - 1f, player.position.y, player.position.z);

            counter += Time.deltaTime;
            if (counter >= followDurationTime)
            {
                counter = 0;
                Transform disappearFx = FxManager.Instance.Spawn(transform.position, new Quaternion(), 0);
                disappearFx.gameObject.SetActive(true);

                transform.parent.gameObject.SetActive(false);
                GameDirector.Instance.spawnEnemy.DecreaseEnemyCount();
            }

        } else
        {
            if (isPlayerAvailable)
            {
                MoveToPlayer();
            }

            if (CheckPlayerAttached())
            {
                isPlayerAttached = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagCollection.Player))
        {
            isPlayerAvailable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagCollection.Player))
        {
            isPlayerAvailable = false;
        }
    }

    private void MoveToPlayer()
    {
        transform.parent.position = Vector3.MoveTowards(transform.parent.position, player.position, Time.deltaTime * dashSpeed);
    }

    private bool CheckPlayerAttached()
    {
        float distanceX = Vector3.Distance(transform.parent.position, player.position);

        return distanceX <= attachedDistance;
    }
}

