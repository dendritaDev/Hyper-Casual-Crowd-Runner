using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{
    [Header(" Managers ")]
    [SerializeField] private ShopManager shopManager;

    [Header("Elements")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Text levelText;


    private void Start()
    {
        progressBar.value = 0;

        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        settingsPanel.SetActive(false);
        shopPanel.SetActive(false);

        levelText.text = "Level " + ChunkManager.instance.GetLevel();

        GameManager.onGameStateChanged += GameStateChangedCallback;
    }


    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void Update()
    {
        UpdateProgressBar();
    }


    private void GameStateChangedCallback(GameManager.GameState state)
    {
        if(state == GameManager.GameState.Gameover)
        {
            ShowGameOver();
        }
        if(state == GameManager.GameState.LevelComplete)
        {
            ShowLevelComplete();
        }
    }

    public void PlayButtonPressed()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Game);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void UpdateProgressBar()
    {
        if(!GameManager.instance.IsGameState())
        {
            return;
        }

        float progress = PlayerController.instance.transform.position.z / ChunkManager.instance.GetFinishZ();
        progressBar.value = progress;
    }

    public void RetryButtonPressed()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowGameOver()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    private void ShowLevelComplete()
    {   
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(true);

    }

    public void ShowSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }

    public void HideSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }

    public void ShowShop()
    {
        shopPanel.SetActive(true);
        shopManager.UpdatePurchaseButton();
    }

    public void HideShop()
    {
        shopPanel.SetActive(false);
    }
}
