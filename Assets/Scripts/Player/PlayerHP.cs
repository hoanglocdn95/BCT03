using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHP : HPController
{
    [SerializeField] private Image HPStatus;
    [SerializeField] private Image HPStatus_2;
    private float threshold_level_0 = 1f;
    private float threshold_level_1 = 0.5f;
    private float threshold_level_2 = 0.2f;

    private static PlayerHP instance;
    public static PlayerHP Instance { get => instance; }

    private void Awake()
    {
        if (PlayerHP.instance == null)
        {
            PlayerHP.instance = this;
        }
        HPStatus_2.fillAmount = 0;
    }

    private void Reset()
    {
        this.thresholdHP = 100f;
        this.currentHP = 100f;
        this.HPStatus.color = Color.green;
        PlayerMovement.Instance.ResetStatus();
    }

    protected override float GetDirection()
    {
        return PlayerMovement.Instance.isFacingRight ? 1f : -1f;
    }

    protected override void Die()
    {
        if (currentHP <= 0)
        {
            PlayerLife.Instance.Decrease();
            Reset();
            ScaleBody();
            GameDirector.Instance.spawnPlayer.SpawnPlayerRandom();
        }
    }

    protected override void AdjustHPBar()
    {
        base.AdjustHPBar();
        float ratioHP = currentHP / thresholdHP;
        HPStatus.fillAmount = ratioHP;

        if (ratioHP >= threshold_level_0)
        {
            HPStatus_2.fillAmount = ratioHP - threshold_level_0;
        }

        if (ratioHP > threshold_level_1 && ratioHP <= threshold_level_0)
        {
            HPStatus.color = Color.green;
            HPStatus_2.fillAmount = 0;
        }
        if (ratioHP <= threshold_level_1 && ratioHP > threshold_level_2)
        {
            HPStatus.color = Color.yellow;
            HPStatus_2.fillAmount = 0;
        }

        if (ratioHP <= threshold_level_2)
        {
            HPStatus.color = Color.red;
            HPStatus_2.fillAmount = 0;
        }
    }
}