using UnityEngine;
using TMPro;

public class ScoreTrack : MonoBehaviour
{
    [Header("Player References")]
    public Transform player1;
    public Transform player2;

    [Header("UI References")]
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;

    private int player1Chops = 0;
    private int player2Chops = 0;
    public float player1Height = 0f;
    public float player2Height = 0f;
    //private int P1Climb = 0;
    //private int P2Climb = 0;

    private void Update()
    {
        // Update player heights and display scores
        player1Height = Mathf.Max(player1.position.y, player1Height);
        player2Height = Mathf.Max(player2.position.y, player2Height);

        UpdateUI();
    }

    public void RegisterChop(int playerNumber)
    {
        if (playerNumber == 1)
            player1Chops++;
        else if (playerNumber == 2)
            player2Chops++;
    }
    /*public void RegisterClimb(int playerNumber)
    {
        if (playerNumber == 1)
            P1Climb++;
        else if (playerNumber == 2)
            P2Climb++;
    }*/

    private void UpdateUI()
    {
        player1ScoreText.text = $"Chops: {player1Chops} | Height: {player1Height:F2}";
        player2ScoreText.text = $"Chops: {player2Chops} | Height: {player2Height:F2}";
    }
}
