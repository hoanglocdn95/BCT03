using UnityEngine;

public class HPController : MonoBehaviour
{
    [SerializeField] protected float thresholdHP = 100f;
    [SerializeField] protected float currentHP = 100f;
    public float minScale = 0.5f;
    public float maxScale = 2f;
    public float ratio = 1f;

    public void IncreaseHP(float amount)
    {
        currentHP += amount;
        ScaleBody();
    }

    public void DecreaseHP(float amount)
    {
        currentHP -= amount;
        ScaleBody();
        Die();
    }

    protected virtual float GetDirection()
    {
        return transform.parent.localScale.x < 0 ? -1 : 1;
    }

    protected virtual void ScaleBody()
    {
        ratio = currentHP / thresholdHP;
        if (ratio < minScale)
        {
            ratio = minScale;
        }
        else if (ratio > maxScale)
        {
            ratio = maxScale;
        }

        AdjustHPBar();

        float direction = GetDirection();
        transform.parent.localScale = new Vector3(ratio * direction, ratio, transform.parent.localScale.z);
    }

    protected virtual void Die()
    {
        if (currentHP <= 0)
        {
            transform.parent.gameObject.SetActive(false);
            AfterDie();
        }
    }

    protected virtual void AdjustHPBar() { }
    protected virtual void AfterDie() { }
}
