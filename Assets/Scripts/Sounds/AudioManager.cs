using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Spawner
{
    public static AudioManager instance;
    public static AudioManager Instance { get => instance; }

    private float counter = 0f;

    public enum SoundEnum
    {
        themeNormal = 0,
        themeIntersection,
        themeBoss,
        playerMoving,
        playerShooting,
        playerAttack,
        playerJump,
        playerDie,
        playerDash,
        playerIdle,
        playerHeal,
        enemyHit,
        enemyMoving,
        enemyFlying,
        enemyAttack,
        enemySpawn,
        enemyDie,
        itemPickup,
        itemSpawn,
    }

    protected override void Awake()
    {
        base.Awake();
        if (AudioManager.instance == null)
        {
            AudioManager.instance = this;
        }
    }

    protected void Start()
    {
        this.PlayTheme((int)SoundEnum.themeNormal, 0.1f);
    }

    public void PlayTheme(int indexTheme, float volume)
    {
        Transform tfTheme = this.Spawn(new Vector3(0, 0, 0), new Quaternion(), indexTheme);
        tfTheme.gameObject.SetActive(true);
        AudioSource audioSource = tfTheme.GetComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlaySound(int indexSound, float volume = 0.7f, bool isDeplay = false)
    {
        Transform tf = this.Spawn(new Vector3(0, 0, 0), new Quaternion(), indexSound);
        tf.gameObject.SetActive(true);
        AudioSource audioSource = tf.GetComponent<AudioSource>();

        if (isDeplay)
        {
            float soundDuration = audioSource.clip.length;

            this.counter += Time.fixedDeltaTime;
            if (this.counter < soundDuration) return;
            this.counter = 0f;

            audioSource.volume = volume;
            audioSource.Play();
        }
        else
        {
            audioSource.volume = volume;
            audioSource.Play();
        }
    }
}
