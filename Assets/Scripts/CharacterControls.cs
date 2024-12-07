using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    [Header("Player Settings")]
    public float climbSpeed = 5f; // Speed of climbing
    public KeyCode player1ChopKey = KeyCode.Z; // Player 1 Chop key
    public KeyCode player1InsertKey = KeyCode.X; // Player 1 Insert key
    public KeyCode player1ClimbKey = KeyCode.UpArrow; // Player 1 Climb key
    public KeyCode player2ChopKey = KeyCode.N; // Player 2 Chop key
    public KeyCode player2InsertKey = KeyCode.M; // Player 2 Insert key
    public KeyCode player2ClimbKey = KeyCode.W; // Player 2 Climb key

    [Header("Player GameObjects")]
    public Transform player1;
    public Transform player2;

    [Header("Standplate Settings")]
    public GameObject standplatePrefab; // Prefab for the standplate
    public Transform player1PlateSpawnPoint; // Spawn point for Player 1's plate
    public Transform player2PlateSpawnPoint; // Spawn point for Player 2's plate

    [Header("Player ChangingPic")]
    public GameObject playerReady;
    public GameObject player2Ready;
    public GameObject playerChop;
    public GameObject player2Chop;

    private bool player1PlatePlaced = false;
    private bool player2PlatePlaced = false;

    [Header("Gameplay Settings")]
    public int initialChops = 5; // Initial number of chops allowed
    public int chopsAddedByClimb = 3; // Number of chops added after each climb

    private int player1ChopCount;
    private int player2ChopCount;
    private int player1ChopsSinceLastClimb = 0;
    private int player2ChopsSinceLastClimb = 0;
    private bool player1CanClimb = false;
    private bool player2CanClimb = false;
    private bool player1CanInsert = false;
    private bool player2CanInsert = false;

    [Header("SoundEffects")]
    public AudioClip ChopSound;
    public AudioClip plankingSound;
    public AudioClip JumpingSound;
    public AudioSource SoundeffectSource;

    ScoreTrack scoreTrack;
    TreeManager treeManager;
    Cutmanage cutmanage;

    private void Start()
    {
        player1ChopCount = initialChops;
        player2ChopCount = initialChops;

        playerReady.SetActive(true);
        player2Ready.SetActive(true);
        playerChop.SetActive(false);
        player2Chop.SetActive(false);

        treeManager = FindAnyObjectByType<TreeManager>();
        scoreTrack = FindAnyObjectByType<ScoreTrack>();
        cutmanage = FindAnyObjectByType<Cutmanage>();
    }

    private void Update()
    {
        HandlePlayer1Input();
        HandlePlayer2Input();
    }

    private void HandlePlayer1Input()
    {
        if (Input.GetKeyDown(player1ChopKey) && player1ChopCount > 0)
        {
            Invoke("Player1Reset", 0.2f);
            Player1Chop();
        }

        if (Input.GetKeyDown(player1InsertKey) && player1CanInsert && !player1PlatePlaced)
        {
            Player1Insert();
        }

        if (Input.GetKey(player1ClimbKey) && player1CanClimb && player1PlatePlaced)
        {
            Player1Climb();
        }
    }

    private void HandlePlayer2Input()
    {
        if (Input.GetKeyDown(player2ChopKey) && player2ChopCount > 0)
        {
            Invoke("Player2Reset", 0.2f);
            Player2Chop();
        }

        if (Input.GetKeyDown(player2InsertKey) && player2CanInsert && !player2PlatePlaced)
        {
            Player2Insert();
        }

        if (Input.GetKey(player2ClimbKey) && player2CanClimb && player2PlatePlaced)
        {
            Player2Climb();
        }
    }

    private void Player1Chop()
    {
        player1ChopCount--;
        player1ChopsSinceLastClimb++;

        if (player1ChopsSinceLastClimb >= 3)
        {
            cutmanage.AddCutSection(1);
            player1CanInsert = true;
            player1CanClimb = true;
            player1ChopsSinceLastClimb = 0;
        }

        SoundeffectSource.PlayOneShot(ChopSound);
        scoreTrack.RegisterChop(1);
        playerReady.SetActive(false);
        playerChop.SetActive(true);
    }

    private void Player1Reset()
    {
        playerReady.SetActive(true);
        playerChop.SetActive(false);
    }

    private void Player1Insert()
    {
        if (player1PlateSpawnPoint != null)
        {
            SoundeffectSource.PlayOneShot(plankingSound);
            Instantiate(standplatePrefab, player1PlateSpawnPoint.position, Quaternion.identity);
            player1PlatePlaced = true;
            player1CanInsert = false;
        }
    }

    private void Player1Climb()
    {
        SoundeffectSource.PlayOneShot(JumpingSound);
        player1.position = new Vector3(player1.position.x, player1.position.y + 3f, player1.position.z);
        player1ChopCount += chopsAddedByClimb;
        player1CanClimb = false;
        player1PlatePlaced = false;
        treeManager.RegisterClimb(1);
    }

    private void Player2Chop()
    {
        player2ChopCount--;
        player2ChopsSinceLastClimb++;

        if (player2ChopsSinceLastClimb >= 3)
        {
            cutmanage.AddCutSection(2);
            player2CanInsert = true;
            player2CanClimb = true;
            player2ChopsSinceLastClimb = 0;
        }

        SoundeffectSource.PlayOneShot(ChopSound);
        scoreTrack.RegisterChop(2);
        player2Ready.SetActive(false);
        player2Chop.SetActive(true);
    }

    private void Player2Reset()
    {
        player2Ready.SetActive(true);
        player2Chop.SetActive(false);
    }

    private void Player2Insert()
    {
        if (player2PlateSpawnPoint != null)
        {
            SoundeffectSource.PlayOneShot(plankingSound);
            Instantiate(standplatePrefab, player2PlateSpawnPoint.position, Quaternion.identity);
            player2PlatePlaced = true;
            player2CanInsert = false;
        }
    }

    private void Player2Climb()
    {
        SoundeffectSource.PlayOneShot(JumpingSound);
        player2.position = new Vector3(player2.position.x, player2.position.y + 3f, player2.position.z);
        player2ChopCount += chopsAddedByClimb;
        player2CanClimb = false;
        player2PlatePlaced = false;
        treeManager.RegisterClimb(2);
    }
}
