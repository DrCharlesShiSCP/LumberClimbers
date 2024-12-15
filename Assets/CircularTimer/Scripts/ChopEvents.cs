using UnityEngine;
using TMPro;

public class ChopEvents : MonoBehaviour
{
    [Header("obj")]
    CircularTimer timer;
    ScoreTrack tracker;
    public GameObject startbutton;
    public TextMeshProUGUI winText;
    public GameObject winScr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winScr.SetActive(false);
        startbutton.SetActive(true);
        timer = FindAnyObjectByType<CircularTimer>();
        tracker = FindAnyObjectByType<ScoreTrack>();
        Time.timeScale = 0f;
    }

    public void GameStartTimer()
    {
        Time.timeScale = 1f;
        timer.StartTimer();
        startbutton.SetActive(false);
    }

    public void GameEnded()
    {
        winScr.SetActive(true);
        Time.timeScale = 0f;
        if (tracker.player1Height > tracker.player2Height)
        {
            winText.text = ("Player 1 Won with a height of " + tracker.player1Height.ToString() + " feet");
        }
        else
        {
            winText.text = ("Player 2 Won with a height of " + tracker.player2Height.ToString() + " feet");
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
