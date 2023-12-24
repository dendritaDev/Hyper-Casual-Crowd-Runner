using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [Header(" Coin Texts ")]
    [SerializeField] private Text[] coinTexts;
    private int coins;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        coins = PlayerPrefs.GetInt("coins", 0);
    }

    private void Start()
    {
        AddCoins(5);
        UpdateCoinsTexts();
    }

    private void UpdateCoinsTexts()
    {
        foreach (Text coinText in coinTexts)
        {
            coinText.text = coins.ToString();
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;

        UpdateCoinsTexts();

        PlayerPrefs.SetInt("coins", coins);
    }

    public void RemoveCoins(int amount)
    {
           coins -= amount;

        UpdateCoinsTexts();

        PlayerPrefs.SetInt("coins", coins);
    }

    public int GetCoins()
    {
        return coins;
    }
}
