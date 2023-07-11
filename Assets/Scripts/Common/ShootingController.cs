using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform holder;
    [SerializeField] private GameObject bulletPrefab;

    public int bulletCount = 0;
    private int bulletAmount = 4;
    private float bulletHP = 10f;

    private float intervalTime = 0.2f;
    private float delayTime;

    private static ShootingController instance;
    public static ShootingController Instance { get => instance; }

    private void Awake()
    {
        delayTime = intervalTime;
        if (ShootingController.instance == null)
        {
            ShootingController.instance = this;
        }
    }

    private void Update()
    {
        delayTime += Time.deltaTime;
        if (delayTime > intervalTime * 2) delayTime = intervalTime;
        if (delayTime < intervalTime) return;
        if (CanShoot())
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                delayTime = 0f;
                Shooting();
            }
        }
    }

    private void Shooting()
    {
        if (bulletCount >= bulletAmount) return;

        Quaternion rotation = Quaternion.identity;
        var bullet = Instantiate(bulletPrefab, spawnPoint.position, rotation);
        bullet.SetActive(true);
        bullet.transform.SetParent(holder);
        bulletCount++;
        PlayerHP.Instance.DecreaseHP(bulletHP);
    }

    private bool CanShoot()
    {
        return PlayerHP.Instance.ratio > PlayerHP.Instance.minScale;
    }
}
