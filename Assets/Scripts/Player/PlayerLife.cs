using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private Text lifeText;
    private int life = 3;

    private static PlayerLife instance;
    public static PlayerLife Instance { get => instance; }

    private void Awake()
    {
        if (PlayerLife.instance == null)
        {
            PlayerLife.instance = this;
        }
    }

    public void Increase ()
    {
        life += 1;
        changeText();
    }

    public void Decrease()
    {
        life -= 1;
        changeText();
    }

    private void changeText()
    {
        lifeText.text = life.ToString();
    }
}
