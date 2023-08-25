using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    [SerializeField] public GameObject[] movingPoints;
    protected SpriteRenderer spriteRenderer;

    [SerializeField] private float speed = 5f;
    [SerializeField] private int currentPointIndex = 0;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.parent.position, movingPoints[currentPointIndex].transform.position) < 0.1f)
        {
            this.currentPointIndex++;

            if (this.currentPointIndex >= movingPoints.Length)
            {
                this.currentPointIndex = 0;
            }
        }

        //if (this.points[this.currentPointIndex].transform.position.x > transform.position.x)
        //{
        //    this.spriteRenderer.flipX = true;
        //}

        //if (this.points[this.currentPointIndex].transform.position.y > transform.position.y)
        //{
        //    this.spriteRenderer.flipY = true;
        //}

        this.Moving();
    }

    protected virtual void Moving()
    {
        transform.parent.position = Vector2.MoveTowards(transform.position, movingPoints[this.currentPointIndex].transform.position, Time.deltaTime * speed);
    }
}
