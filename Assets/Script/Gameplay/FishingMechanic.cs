using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishingMechanic : MonoBehaviour
{
    public KeyCode castKey = KeyCode.Space;
    public float minBiteTime = 2f;
    public float maxBiteTime = 5f;

    public TextMeshProUGUI resultText;
    public TextMeshProUGUI difficultyText;

    [Header("Difficulty Settings")]
    public DifficultySettings[] difficulties;
    public Slider difficultySlider;
    public int maxSliderValue = 100;
    public QuickTimeEvent qteManager;

    [Header("Fishing Line")]
    public GameObject fishingLinePrefab;
    public Transform fishPoint; 
    public Transform baitPoint;

    private GameObject currentLine;

    private bool isFishing = false;
    private DifficultySettings currentDifficulty;
    private Animator animator;
    private LineRenderer fishingLine;

    void Start()
    {
        resultText.text = "Tekan [Space] untuk memancing...";
        animator = GetComponent<Animator>();
        difficultyText.text = "Dificulty";

        if (difficultySlider != null)
        {
            difficultySlider.minValue = 0;
            difficultySlider.maxValue = maxSliderValue;
        }
    }

    void Update()
    {
        if (!isFishing && !GameManager.Instance.GameOver)
        {
            isFishing = true;
            animator.SetTrigger("isFishing");
            StartCoroutine(StartFishing());
        }
    }

    IEnumerator StartFishing()
    {
        isFishing = true;
        resultText.text = "Melempar kail...";
        yield return new WaitForSeconds(Random.Range(minBiteTime, maxBiteTime));

        PickDifficulty();
        float chance = currentDifficulty.successRate;

        resultText.text = "Ikan menggigit! Cepat tekan tombol!";

        // mulai QTE sesuai difficulty
        int qteLength = 5 + currentDifficulty.extraQTE;
        qteManager.onSuccess = CatchFish;
        qteManager.onFail = FailCatch;
        qteManager.StartQTE(qteLength);

        yield return new WaitForSeconds(4f);

        isFishing = false;
        maxSliderValue = 0;
        RemoveFishingLine();
    }

    void PickDifficulty()
    {
        currentDifficulty = difficulties[Random.Range(0, difficulties.Length)];

        difficultyText.text = $"{currentDifficulty.difficulty}";

        if (difficultySlider != null)
        {
            difficultySlider.value = currentDifficulty.sliderValue;
            var fill = difficultySlider.fillRect.GetComponent<UnityEngine.UI.Image>();
            if (fill != null)
            {
                fill.color = currentDifficulty.uiColor;
            }
        }
    }


    void CatchFish()
    {
        int mistakes = qteManager.GetMistakes();
        int totalKeys = qteManager.GetTotalKeys();

        float factor = Mathf.Max(0f, (float)(totalKeys - mistakes) / totalKeys);
        float finalHunger = currentDifficulty.hungerGain * factor;

        float share = finalHunger / 2f;

        GameManager.Instance.ModifyPlayerHunger(share);
        GameManager.Instance.ModifyNPCHunger(share);

        qteManager.ResetMistakes();
        ResetDifficultyUI();
    }

    void FailCatch()
    {
        float penalty = currentDifficulty.hungerPenalty / 2f;

        GameManager.Instance.ModifyPlayerHunger(-penalty);
        GameManager.Instance.ModifyNPCHunger(-penalty);

        resultText.text = " Gagal! Ikan kabur...";
        qteManager.ResetMistakes();
        ResetDifficultyUI();
    }

    void ResetDifficultyUI()
    {
        if (difficultySlider != null)
        {
            difficultySlider.value = 0;
        }
    }

    public void CastFishingLine()
    {
        if (fishingLinePrefab == null || fishPoint == null || baitPoint == null) return;

        if (currentLine != null) Destroy(currentLine);

        currentLine = Instantiate(fishingLinePrefab, Vector3.zero, Quaternion.identity);

        FishingLine line = currentLine.GetComponent<FishingLine>();
        line.startPoint = fishPoint;
        line.endPoint = baitPoint;
    }

    public void RemoveFishingLine()
    {
        if (currentLine != null) Destroy(currentLine);
    }
}