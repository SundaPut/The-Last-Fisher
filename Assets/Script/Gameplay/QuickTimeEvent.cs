using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class QTEKey
{
    public KeyCode[] keys;
    public GameObject prefab;
}

public class QuickTimeEvent : MonoBehaviour
{
    [Header("QTE Settings")]
    public Transform arrowParent;
    public GameObject arrowPanel;
    public float qteTime = 4f;

    [Header("Arrow Prefabs")]
    public GameObject arrowLeftPrefab;
    public GameObject arrowRightPrefab;
    public GameObject arrowUpPrefab;
    public GameObject arrowDownPrefab;

    private List<QTEKey> possibleKeys = new List<QTEKey>();
    private List<QTEKey> currentSequence = new List<QTEKey>();
    private List<GameObject> spawnedArrows = new List<GameObject>();

    private int currentIndex = 0;
    private float timer;
    private bool active = false;
    private int mistakes = 0;

    public System.Action onSuccess;
    public System.Action onFail;

    void Start()
    {
        possibleKeys.Add(new QTEKey { keys = new KeyCode[] { KeyCode.LeftArrow }, prefab = arrowLeftPrefab });
        possibleKeys.Add(new QTEKey { keys = new KeyCode[] { KeyCode.RightArrow }, prefab = arrowRightPrefab });
        possibleKeys.Add(new QTEKey { keys = new KeyCode[] { KeyCode.UpArrow }, prefab = arrowUpPrefab });
        possibleKeys.Add(new QTEKey { keys = new KeyCode[] { KeyCode.DownArrow }, prefab = arrowDownPrefab });

        arrowPanel.SetActive(false);
    }

    public void StartQTE(int length)
    {
        ClearArrows();
        currentSequence.Clear();
        arrowPanel.SetActive(true);

        currentIndex = 0;
        timer = qteTime;
        active = true;

        for (int i = 0; i < length; i++)
        {
            int rand = Random.Range(0, possibleKeys.Count);
            QTEKey chosen = possibleKeys[rand];
            currentSequence.Add(chosen);

            GameObject arrow = Instantiate(chosen.prefab, arrowParent);
            spawnedArrows.Add(arrow);
        }
    }

    void Update()
    {
        if (!active) return;

        timer -= Time.deltaTime;
        if (timer <= 0f) { Fail(); return; }

        if (Input.anyKeyDown)
        {
            bool correct = false;

            foreach (KeyCode key in currentSequence[currentIndex].keys)
            {
                if (Input.GetKeyDown(key))
                {
                    correct = true;
                    break;
                }
            }

            if (correct)
            {
                OnCorrectInput();
            }
            else
            {
                OnWrongInput();
            }
        }
    }

    void OnCorrectInput()
    {
        var arrowObj = spawnedArrows[currentIndex];
        var anim = arrowObj.GetComponent<Animator>();

        if (anim != null)
            anim.SetTrigger("Correct");

        currentIndex++;
        if (currentIndex >= currentSequence.Count)
        {
            FinishQTE();
        }
    }

    void OnWrongInput()
    {
        mistakes++;

        var arrowObj = spawnedArrows[currentIndex];
        var anim = arrowObj.GetComponent<Animator>();

        if (anim != null)
            anim.SetTrigger("Wrong");

        currentIndex++;
        if (currentIndex >= currentSequence.Count)
        {
            FinishQTE();
        }
    }


    void Success()
    {
        active = false;
        ClearArrows();
        onSuccess?.Invoke();
    }

    void Fail()
    {
        active = false;
        ClearArrows();
        onFail?.Invoke();
    }

    void FinishQTE()
    {
        active = false;
        ClearArrows();

        if (mistakes == 0)
        {
            onSuccess?.Invoke();
        }
        else
        {
            onFail?.Invoke();
        }
    }

    void ClearArrows()
    {
        foreach (var arrow in spawnedArrows) Destroy(arrow);
        arrowPanel.SetActive(false);
        spawnedArrows.Clear();
    }
    public int GetMistakes()
    {
        return mistakes;
    }
    public int GetTotalKeys()
    {
        return currentSequence.Count;
    }

    public void ResetMistakes()
    {
        mistakes = 0;
    }
}