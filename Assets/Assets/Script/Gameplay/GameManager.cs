using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Hunger Settings")]
    public Slider playerHungerBar;
    public float maxHunger = 100f;
    public float hungerDecreaseRate = 2f;
    private float currentPlayerHunger;

    [Header("NPC Hunger Settings")]
    public Slider npcHungerBar;
    public float npcMaxHunger = 100f;
    public float npcHungerDecreaseRate = 1.5f;
    private float currentNPCHunger;

    [Header("Game Settings")]
    public float gameTime = 120f;
    public TextMeshProUGUI timerText;
    private float timer;

    [Header("UI Win Or Lose")]
    public GameObject WinPanel;
    public GameObject LosePanel;

    public bool GameOver { get; private set; } = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        Time.timeScale = 1;
        currentPlayerHunger = maxHunger;
        playerHungerBar.maxValue = maxHunger;
        playerHungerBar.value = currentPlayerHunger;

        currentNPCHunger = npcMaxHunger;
        npcHungerBar.maxValue = npcMaxHunger;
        npcHungerBar.value = currentNPCHunger;

        timer = gameTime;

        // pastikan panel win/lose mati di awal
        if (WinPanel) WinPanel.SetActive(false);
        if (LosePanel) LosePanel.SetActive(false);
    }

    void Update()
    {
        if (GameOver) return;

        timer -= Time.deltaTime;
        if (timer < 0) timer = 0;

        UpdateTimerUI();

        ModifyPlayerHunger(-hungerDecreaseRate * Time.deltaTime);
        ModifyNPCHunger(-npcHungerDecreaseRate * Time.deltaTime);

        if (currentPlayerHunger <= 0 || currentNPCHunger <= 0)
        {
            LoseGame("Kelaparan!");
        }
        else if (timer <= 0)
        {
            if (currentPlayerHunger > 0 && currentNPCHunger > 0)
                WinGame();
            else
                LoseGame("Waktu habis dan kelaparan!");
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }


    public void ModifyPlayerHunger(float amount)
    {
        currentPlayerHunger = Mathf.Clamp(currentPlayerHunger + amount, 0, maxHunger);
        playerHungerBar.value = currentPlayerHunger;
    }

    public void ModifyNPCHunger(float amount)
    {
        currentNPCHunger = Mathf.Clamp(currentNPCHunger + amount, 0, npcMaxHunger);
        npcHungerBar.value = currentNPCHunger;
    }
    void WinGame()
    {
        GameOver = true;
        Debug.Log("Kamu menang!");
        if (WinPanel) WinPanel.SetActive(true);
        Time.timeScale = 0;
    }

    void LoseGame(string reason)
    {
        GameOver = true;
        Debug.Log("Kamu kalah! " + reason);
        if (LosePanel) LosePanel.SetActive(true);
        Time.timeScale = 0;
    }
}
