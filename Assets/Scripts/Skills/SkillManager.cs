using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Spawner
{
    private static SkillManager instance;
    public static SkillManager Instance { get => instance; }

    protected override void Awake()
    {
        base.Awake();
        if (SkillManager.instance == null)
        {
            SkillManager.instance = this;
        }
    }
}
