using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : SpawnPoint
{
    [SerializeField] private Transform Player;
    private float delaySpawn = 2f;

    public void SpawnPlayerRandom()
    {
        Player.gameObject.SetActive(false);
        Debug.Log("SpawnPlayerRandom ");

        //yield return new WaitForSeconds(delaySpawn);

        Player.position = PointSpawn();
        Debug.Log("Player.position " + Player.position);
        Player.gameObject.SetActive(true);
    }
}
