using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerr : MonoBehaviour
{

    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject GameOverGO;
    public GameObject scoreUITextGO;
    public GameObject TimeCounterGO;
    public GameObject GameTitleGO;

    public enum GameManagerState
    {
        Opening,
        Gameplay,
        Gameover,
    }

    GameManagerState GMState;

    // Use this for initialization
    void Start()
    {
        GMState = GameManagerState.Opening;
    }

    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                GameOverGO.SetActive(false);
                GameTitleGO.SetActive(true);
                playButton.SetActive(true);
                break;
            case GameManagerState.Gameplay:
                scoreUITextGO.GetComponent<GameScore>().Score = 0;
                playButton.SetActive(false);
                GameTitleGO.SetActive(false);
                playerShip.GetComponent<PlayerControl>().Init();
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();
                break;
            case GameManagerState.Gameover:
                TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                GameOverGO.SetActive(true);
                Invoke("ChangeToOpeningState", 5f);
                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}