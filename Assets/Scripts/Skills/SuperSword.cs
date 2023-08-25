using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSword : MonoBehaviour
{
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private CountTimeSkill countTimeSkill;
    private float timeAvailable = 10;

    private void OnEnable()
    {
        countTimeSkill.SetStatus(true);
        playerAttack.GetAttackObject("SuperCloseAttack");
        StartCoroutine(DisableSkill());
    }

    private IEnumerator DisableSkill()
    {
        float counter = timeAvailable;

        do
        {
            CountTimeSkill.Instance.ChangeTimeText(counter);
            yield return new WaitForSeconds(1);
            counter--;
        } while (counter > 0);

        playerAttack.GetAttackObject("CloseAttack");

        countTimeSkill.SetStatus(false);
        SkillManager.Instance.PutObjectInPool(transform);
    }
}
