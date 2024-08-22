using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGamePaused;
    public bool isPlayerWin;
    public bool isGameRunning = false;

    public List<Partygoer> partygoers = new();
    public int partygoersCount;
    public int escapedPartygoersCount;
    public int partygoersSlayedCound;
    [SerializeField] int maxPartygoer = 14;
    [SerializeField] int minPartygoer = 4;


    public GameObject partygoersPrefab;
    public Transform[] spawnPoints;
    public Transform exitTrans;
    public GameObject panelIntro;
    public GameObject panelLose;
    public GameObject panelWin;

    public Player player;
    public Timer timer;
    public PlayerMovement playerMovement;
    public DaySystem daySystem;
    public PlayerSpawn playerSpawn;
    public MoveCamera moveCamera;
    public PlayerCam playerCam;

    private void Start()
    {
        player = playerSpawn.SpawnPlayer();
        playerMovement = player.gameObject.GetComponent<PlayerMovement>();
        moveCamera.cameraPosition = player.cameraPosition;
        playerCam.orientation = player.transform;
    }

    private void Update()
    {
        if (isGameRunning)
        {
            if (partygoersCount <= 0 && escapedPartygoersCount <= 1)
            {
                isPlayerWin = true;
                isGameRunning = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                panelWin.SetActive(true);
            }
            else if (partygoersCount <= 0 && escapedPartygoersCount > 1)
            {
                isPlayerWin = false;
                isGameRunning = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                panelLose.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void StartGame()
    {
        isGameRunning = true;
        partygoersCount = Random.Range(minPartygoer, maxPartygoer);
        Spawn();
        playerMovement.enabled = true;
        timer.StartTimer();
    }

    public void Spawn()
    {
        for (int i = 0; i < partygoersCount; i++)
        {
            partygoers.Add(SpawnPartygoer(i));
        }
    }

    public Partygoer SpawnPartygoer(int i)
    {
        Partygoer partygoer = Instantiate(partygoersPrefab, spawnPoints[i].position, Quaternion.identity).GetComponent<Partygoer>();
        partygoer.Init(player.transform, exitTrans);
        partygoer.OnPartygoerDie += SubtractPartygoer;
        partygoer.OnPartygoerExit += AddEscapedPartygoer;
        return partygoer;
    }

    public void SubtractPartygoer(Partygoer partygoer)
    {
        if (partygoers.Contains(partygoer))
        {
            partygoer.OnPartygoerDie -= SubtractPartygoer;
            partygoer.OnPartygoerExit -= AddEscapedPartygoer;
            partygoers.Remove(partygoer);
            partygoersCount--;
            partygoersSlayedCound++;
        }
    }

    public void AddEscapedPartygoer(Partygoer partygoer)
    {
        escapedPartygoersCount++;
        partygoers.Remove(partygoer);
        partygoersCount--;
    }

    public void RestartGame()
    {
        Destroy(player.gameObject);
        partygoers.Clear();
        timer.TimerReset();
        if (minPartygoer < maxPartygoer)
            minPartygoer++;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        escapedPartygoersCount = 0;

        player = playerSpawn.SpawnPlayer();
        playerMovement = player.gameObject.GetComponent<PlayerMovement>();
        moveCamera.cameraPosition = player.cameraPosition;
        playerCam.orientation = player.transform;
    }

    private void OnDestroy()
    {
        foreach (Partygoer partygoer in partygoers)
        {
            if (partygoer != null)
            {
                partygoer.OnPartygoerDie -= SubtractPartygoer;
                partygoer.OnPartygoerExit -= AddEscapedPartygoer;
            }
        }
    }
}
