using System;
using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [NonSerialized] public GameManager GameManager;
    [NonSerialized] public bool LevelStarted;

    private bool _levelCompleted;


    private void Awake() {
        GameManager = FindObjectOfType<GameManager>();
        SetLevel();
    }

    // TODO: When the level is completed: Call _gameManager.LevelCompleted()
    public void LevelCompleted() {
        _levelCompleted = true;
        GameManager.LevelCompleted();
    }

    // TODO: When the level is failed: Call _gameManager.LevelFailed()
    public IEnumerator LevelFailed() {
        yield return new WaitForSeconds(0.5f);
        if (!_levelCompleted) {
            GameManager.LevelFailed();
        }
    }

    // ******************** //

    public void StartLevel() {
        LevelStarted = true;
    }

    public void StopLevel() {
        LevelStarted = false;
    }

    private void SetLevel() {
    }
}