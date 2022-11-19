using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject _confetti;
    public HelixController Controls;
    public enum State
    {
        Playing,
        Won,
        Loss,
    }

    public State CurrenState { get; private set; }

    public void OnPlayerDied()
    {
        if (CurrenState != State.Playing) return;
       
        CurrenState = State.Loss;
        Controls.enabled = false;
        Debug.Log("Game Over!");
        ReloadLevel();
    }

    public void OnPlayerReachedFinish()
    {
        if (CurrenState != State.Playing) return;

        CurrenState = State.Won;
        Controls.enabled = false;
        LevelIndex++;
        Debug.Log("You won!");
        StartCoroutine(WaitToReload());
    }

    private IEnumerator WaitToReload()
    {
        yield return new WaitForSeconds(0.8f);
        _confetti.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        ReloadLevel();
    }

    public int LevelIndex
    {

        get => PlayerPrefs.GetInt(LevelIndexKey, 0);
        set
        {
            PlayerPrefs.SetInt(LevelIndexKey, value);
            PlayerPrefs.Save();
        }
    }
    private const string LevelIndexKey = "LevelIndex";

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
