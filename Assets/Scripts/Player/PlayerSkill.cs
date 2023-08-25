using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    private static PlayerSkill instance;
    public static PlayerSkill Instance { get => instance; }

    [SerializeField] private PlayerData playerData;
    [SerializeField] private SkillData skillData;

    [SerializeField] private List<Text> listTextItem;

    private void Awake()
    {
        playerData.pickedUpItems.Clear();
        if (PlayerSkill.instance == null)
        {
            PlayerSkill.instance = this;
        }
    }

    private void OnEnable()
    {
        ClearAllItem();
    }

    private void ClearAllItem()
    {
        playerData.pickedUpItems.Clear();
        foreach (Text item in listTextItem)
        {
            item.text = "";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            int indexSkill = CheckSkill();
            if (indexSkill == -1)
            {
                Debug.Log("No skill");
            }
            else
            {
                Transform skill = SkillManager.Instance.Spawn(PositionSpawnSkill(indexSkill), new Quaternion(), indexSkill);
                skill.gameObject.SetActive(true);
                ClearAllItem();
            }
        }
    }

    private Vector3 PositionSpawnSkill(int indexSkill)
    {
        float dirX;
        switch (indexSkill)
        {
            case (int)SkillData.SkillSet.JL_SlowRocket:
                if (transform.parent.localScale.x > 0)
                {
                    dirX = transform.parent.position.x + 0.6f * transform.parent.localScale.x + 1.6f;
                }
                else
                {
                    dirX = transform.parent.position.x + 0.6f * transform.parent.localScale.x - 1.6f;
                }
                return new Vector3(dirX, transform.position.y, transform.position.z);
            case (int)SkillData.SkillSet.JC_FireArmor:
            case (int)SkillData.SkillSet.CL_SuperSword:
            default:
                return transform.position;
        }
    }

    public void SetItem(int itemIndex)
    {
        if (playerData.pickedUpItems.Count >= playerData.maxNumberItem)
        {
            playerData.pickedUpItems.RemoveAt(0);
        }
        playerData.pickedUpItems.Add(itemIndex);

        SetTextItem();
    }

    private string SortAlphabetically(string input)
    {
        char[] charArray = input.ToCharArray();
        System.Array.Sort(charArray);
        return new string(charArray);
    }

    private int CheckSkill()
    {
        if (playerData.pickedUpItems.Count >= playerData.maxNumberItem)
        {
            string skillName = "";
            for (int i = 0; i < playerData.pickedUpItems.Count; i++)
            {
                skillName += SkillData.ItemEnumName[playerData.pickedUpItems[i]];
            }

            for (int s = 0; s < SkillData.SkillSetName.Length; s++)
            {
                if (SortAlphabetically(SkillData.SkillSetName[s]) == SortAlphabetically(skillName))
                {
                    return s;
                }
            }
            return -1;
        }
        else
        {
            return -1;
        }
    }

    private void SetTextItem()
    {
        if (playerData.pickedUpItems.Count == 0) return;

        for (int i = 0; i < playerData.pickedUpItems.Count; i++)
        {
            listTextItem[i].text = SkillData.ItemEnumName[playerData.pickedUpItems[i]];
        }
    }
}
