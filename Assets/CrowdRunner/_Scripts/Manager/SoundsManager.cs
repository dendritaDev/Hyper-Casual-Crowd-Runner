using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioSource doorHitSound;
    [SerializeField] private AudioSource coinHitSound;
    [SerializeField] private AudioSource runnerDieSound;
    [SerializeField] private AudioSource levelCompleteSound;
    [SerializeField] private AudioSource gameOverSound;


    private void Start()
    {
        PlayerDetection.onDoorsHit += PlayDoorHitSound;
        PlayerDetection.onCoinHit += PlayCoinHitSound;

        GameManager.onGameStateChanged += GameStateChangedCallback;

        Enemy.onRunnerDied += PlayRunnerDieSound;
    }

    private void OnDestroy()
    {
        PlayerDetection.onDoorsHit -= PlayDoorHitSound;
        PlayerDetection.onCoinHit -= PlayCoinHitSound;

        GameManager.onGameStateChanged -= GameStateChangedCallback;

        Enemy.onRunnerDied -= PlayRunnerDieSound;

    }

    private void PlayDoorHitSound()
    {
        doorHitSound.Play();
    }

    private void PlayCoinHitSound()
    {
        coinHitSound.Play();
    }


    private void GameStateChangedCallback(GameManager.GameState state)
    {
        if(state == GameManager.GameState.Gameover)
        {
            gameOverSound.Play();
        }
        else if(state == GameManager.GameState.LevelComplete)
        {
            levelCompleteSound.Play();
        }   
    }

    private void PlayRunnerDieSound()
    {
        runnerDieSound.Play();
    }

    public void EnableSounds()
    {
        doorHitSound.volume = 1;
        runnerDieSound.volume = 1;
        levelCompleteSound.volume = 1;
        gameOverSound.volume = 1;
        buttonSound.volume = 1;
    }

    public void DisableSounds()
    {
        doorHitSound.volume = 0;
        runnerDieSound.volume = 0;
        levelCompleteSound.volume = 0;
        gameOverSound.volume = 0;
        buttonSound.volume = 0;
    }
}
