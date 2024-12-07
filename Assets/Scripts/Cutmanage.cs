using System.Collections.Generic;
using UnityEngine;

public class Cutmanage : MonoBehaviour
{
    public GameObject CutPrefab;
    public GameObject Cut2Prefab;

    public AudioClip CutClip;
    public AudioSource SoundSource;

    public List<GameObject> Cuts = new List<GameObject>();

    public int player1CutCount = 0;
    public int player2CutCount = 0;

    public Transform player1CutSpawnPoint;
    public Transform player2CutSpawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void RegisterCut(int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1CutCount++;
            //if (player1CutCount % 3 == 0) AddCutSection(1);
            //if (player1CutCount % 6 == 0) RemoveOldestCutSection(1);
        }
        else if (playerNumber == 2)
        {
            player2CutCount++;
            //if (player2CutCount % 3 == 0) AddCutSection(2);
            //if (player2CutCount % 6 == 0) RemoveOldestCutSection(2);
        }
    }
    public void AddCutSection(int playerNumber)
    {
        SoundSource.PlayOneShot(CutClip);
        if (playerNumber == 1)
        {
            if (player1CutSpawnPoint != null)
            {
                Instantiate(CutPrefab, player1CutSpawnPoint.position, Quaternion.identity);
            }
        }
        else if (playerNumber == 2)
        {
            if (player2CutSpawnPoint != null)
            {
                Instantiate(Cut2Prefab, player2CutSpawnPoint.position, Quaternion.identity);
            }
        }
    }
}
