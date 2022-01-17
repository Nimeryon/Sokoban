using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int LevelCount;
    public int SelectedLevel;

    private int CompleteGoals;
    private int Goals;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);

        LevelCount = Directory.GetFiles(Application.streamingAssetsPath + "/Levels", "*.lvl").Length;
    }

    public void SetGoals(int goals)
    {
        Goals = goals;
        UIManager.Instance.SetGoalText(Goals, CompleteGoals);
    }

    public void SetCompleteGoals(int completeGoals)
    {
        CompleteGoals = completeGoals;
        if (CompleteGoals == Goals) Invoke("NextLevel", .25f);
        UIManager.Instance.SetGoalText(Goals, CompleteGoals);
    }

    public void NextLevel()
    {
        if (SelectedLevel + 1 < LevelCount)
        {
            SelectedLevel++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public void PreviousLevel()
    {
        if (SelectedLevel - 1 >= 0)
        {
            SelectedLevel--;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Undo()
    {
        LevelManager.Instance.Grid.Undo();
    }
}
