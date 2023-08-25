using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Configuration/Items", order = 1)]
public class ItemData : ScriptableObject
{
	public float amountHeal = 20f;
	public string nameItem = "joy";
	public int indexItem = 0;
}