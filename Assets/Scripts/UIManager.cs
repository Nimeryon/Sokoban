using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Menu Items")]
    [SerializeField] private GameObject LevelButton;
    [SerializeField] private Transform LevelsGroup;
    private bool isPopulated;

    [Header("Game Items")]
    [SerializeField] private Button PreviousLevelButton;
    [SerializeField] private Button NextLevelButton;
    [SerializeField] private Button ResetLevelButton;
    [SerializeField] private Button UndoLevelButton;
    [SerializeField] private TextMeshProUGUI GoalsText;
    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] private TextMeshProUGUI MoveText;
    [SerializeField] private bool SetGameButtons = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (SetGameButtons && GameManager.Instance != null)
        {
            if (GameManager.Instance.SelectedLevel == 0) PreviousLevelButton.interactable = false;

            if (GameManager.Instance.SelectedLevel == GameManager.Instance.LevelCount - 1) NextLevelButton.interactable = false;

            LevelText.SetText("Level " + (GameManager.Instance.SelectedLevel + 1));
        }
    }

    public void PopulateLevelButtons()
    {
        if (isPopulated) return;

        string[] levelsName = Directory.GetFiles(Application.streamingAssetsPath + "/Levels", "*.lvl");
        foreach (var levelName in levelsName)
        {
            int levelNumber = int.Parse(levelName.Split("\\")[1].Split(".")[0]);

            GameObject levelButton = Instantiate(LevelButton, LevelsGroup);
            levelButton.transform.Find("Name").GetComponent<TextMeshProUGUI>().SetText((levelNumber + 1).ToString());
            levelButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    GameManager.Instance.SelectedLevel = levelNumber;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            );
        }
        isPopulated = true;
    }

    public void SetGoalText(int goals, int completeGoals)
    {
        GoalsText.SetText(completeGoals + "/" + goals);
    }

    public void SetMoveText(int moves)
    {
        MoveText.SetText("Move count: " + moves);
    }

    public void NextLevel()
    {
        GameManager.Instance.NextLevel();
    }

    public void PreviousLevel()
    {
        GameManager.Instance.PreviousLevel();
    }

    public void ResetLevel()
    {
        GameManager.Instance.ResetLevel();
    }

    public void Undo()
    {
        GameManager.Instance.Undo();
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
