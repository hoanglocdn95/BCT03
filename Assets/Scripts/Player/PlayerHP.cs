using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHP : HPController
{
    [SerializeField] private Image HP_Level_1;
    private float threshold_level_1 = 0.5f;
    [SerializeField] private Image HP_Level_2;
    private float threshold_level_2 = 0.2f;
    [SerializeField] private Image HP_Level_3;

    private static PlayerHP instance;
    public static PlayerHP Instance { get => instance; }

    private void Awake()
    {
        if (PlayerHP.instance == null)
        {
            PlayerHP.instance = this;
        }
    }

    private void Reset()
    {
        this.thresholdHP = 100f;
        this.currentHP = 100f;
    }

    protected override float GetDirection()
    {
        return PlayerMovement.Instance.isFacingRight ? 1f : -1f;
    }

    protected override void Die()
    {
        if (currentHP <= 0)
        {
            //transform.parent.gameObject.SetActive(false);
            PlayerLife.Instance.Decrease();
            GameDirector.Instance.spawnPlayer.SpawnPlayerRandom();
            //StartCoroutine(GameDirector.Instance.spawnPlayer.SpawnPlayerRandom());
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Reset();
            ScaleBody();
        }
    }

    protected override void AdjustHPBar()
    {

        float ratioHP = currentHP / thresholdHP;

        base.AdjustHPBar();
        if (HP_Level_1.gameObject.activeInHierarchy && ratioHP <= threshold_level_1)
        {
            HP_Level_1.gameObject.SetActive(false);
        }
        else
        {
            HP_Level_1.fillAmount = ratioHP;
        }

        if (HP_Level_2.gameObject.activeInHierarchy && ratioHP <= threshold_level_2)
        {
            HP_Level_2.gameObject.SetActive(false);
        }
        else
        {
            HP_Level_2.fillAmount = ratioHP;
        }

        HP_Level_3.fillAmount = ratioHP;
    }
}