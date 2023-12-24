using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingsManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private SoundsManager soundsManager;
    [SerializeField] private Image soundsButtonImage;
    [SerializeField] private Sprite optionsOnSprite;
    [SerializeField] private Sprite optionsOffSprite;

    [Header(" Settings ")]
    private bool soundsState = true;

    private void Awake()
    {
        soundsState = PlayerPrefs.GetInt("sounds", 1) == 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Setup()
    {
        if (soundsState)
            EnableSounds();
        else
            DisableSounds();    
    }

    public void ChangeSoundsState()
    {
        if (soundsState)
            DisableSounds();
        else
            EnableSounds();

        soundsState = !soundsState;

        PlayerPrefs.SetInt("sounds", soundsState ? 1 : 0);
    }

    private void EnableSounds()
    {

        soundsManager.EnableSounds();

        // change the sprite of the button
        soundsButtonImage.sprite = optionsOnSprite;
    }

    private void DisableSounds()
    {
        // tell the sound manager to disable the sounds
        soundsManager.DisableSounds(); 
        // change the sprite of the button
        soundsButtonImage.sprite = optionsOffSprite;
    }
}
