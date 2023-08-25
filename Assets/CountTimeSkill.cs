using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountTimeSkill : MonoBehaviour
{
    private static CountTimeSkill instance;
    public static CountTimeSkill Instance { get => instance; }

    [SerializeField] private Transform player;
    [SerializeField] private Text timeText;


    private void Awake()
    {
        CountTimeSkill.instance = this;
        timeText = transform.GetComponentInChildren<Text>();
    }

    private void Update()
    {
        float dirY = player.position.y + 1f + (player.localScale.y - 1) / 2;
        transform.position = new Vector3(player.position.x, dirY, player.position.z);
    }

    public void ChangeTimeText(float time)
    {
        timeText.text = time.ToString();
    }

    public void SetStatus(bool isActive)
    {
        transform.gameObject.SetActive(isActive);
    }
}
