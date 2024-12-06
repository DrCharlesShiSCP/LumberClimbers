using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    [Header("Tree Settings")]
    public GameObject treeSectionPrefab; // Prefab for a tree section
    public Transform player1TreeBase;   // Base position for Player 1's tree
    public Transform player2TreeBase;   // Base position for Player 2's tree
    public float sectionHeight = 6.3f;    // Height of each tree section

    public List<GameObject> player1TreeSections = new List<GameObject>();
    public List<GameObject> player2TreeSections = new List<GameObject>();

    public int player1ClimbCount = 0;
    public int player2ClimbCount = 0;

    private void Start()
    {
        player1TreeBase.position = new Vector3(player1TreeBase.position.x, player1TreeBase.position.y, player1TreeBase.position.z);
        player2TreeBase.position = new Vector3(player2TreeBase.position.x, player1TreeBase.position.y, player2TreeBase.position.z);
    }

    public void RegisterClimb(int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1ClimbCount++;
            if (player1ClimbCount % 1 == 0) AddTreeSection(1);
            if (player1ClimbCount % 4 == 0) RemoveOldestTreeSection(1);
        }
        else if (playerNumber == 2)
        {
            player2ClimbCount++;
            if (player2ClimbCount % 1 == 0) AddTreeSection(2);
            if (player2ClimbCount % 4 == 0) RemoveOldestTreeSection(2);
        }
    }

    private void AddTreeSection(int playerNumber)
    {
        Transform treeBase = playerNumber == 1 ? player1TreeBase : player2TreeBase;
        List<GameObject> treeSections = playerNumber == 1 ? player1TreeSections : player2TreeSections;

        // Calculate new section position
        Vector3 newPosition = treeBase.position + Vector3.up * sectionHeight * treeSections.Count;

        // Instantiate and add to the list
        GameObject newSection = Instantiate(treeSectionPrefab, newPosition, Quaternion.identity, treeBase);
        treeSections.Add(newSection);
    }

    private void RemoveOldestTreeSection(int playerNumber)
    {
        List<GameObject> treeSections = playerNumber == 1 ? player1TreeSections : player2TreeSections;

        if (treeSections.Count > 0)
        {
            // Remove and destroy the oldest section
            GameObject oldestSection = treeSections[0];
            treeSections.RemoveAt(0);
            Destroy(oldestSection);
        }
    }
}
