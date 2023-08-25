using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Configuration/PLayer", order = 0)]
public class PlayerData : ScriptableObject
{
	public string status = "nothing";
	public float movementSpeed = 5f;
	public float jumpingPower = 16f;
	public float extraJumpingPower = 12f;

	public float dashingPower = 24f;
	public float dashingTime = 0.2f;
	public float dashingCooldown = 1f;

	public float wallSlidingSpeed = 2f;

	public float wallJumpingTime = 0.2f;
	public float wallJumpingDuration = 0.4f;
	public Vector2 wallJumpingPower = new Vector2(4f, 16f);

	public float knockbackTime = 0.5f;
	public float knockbackForce = 2.5f;

	public List<int> pickedUpItems;

	public int maxNumberItem = 2;
}