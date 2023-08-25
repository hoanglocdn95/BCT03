using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Configuration/Skills", order = 3)]
public class SkillData : ScriptableObject
{
    public enum ItemEnum
    {
        J_Joy,
        C_Confident,
        L_Love,
        Pe_Peaceful,
        I_Interest,
        Pr_Proud,
    }

    public static string[] ItemEnumName = { "J", "C", "L", "Pe", "I", "Pr" };

    public enum SkillSet
    {
        JC_FireArmor,
        JL_SlowRocket,
        CL_SuperSword,
    }

    public static string[] SkillSetName = { "JC", "JL", "CL" };

    public enum SuperSkillSet
    {
        JCL_KaDiKoMeMa,
        PeIPr_BoLaSoMaQa,
    }

    public static string[] SuperSkillSetName = { "JCL", "PeIPr" };
}
