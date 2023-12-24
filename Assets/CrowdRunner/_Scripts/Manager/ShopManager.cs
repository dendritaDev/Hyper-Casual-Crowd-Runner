using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header(" Skins ")]
    [SerializeField] private Sprite[] skins;

    [Header(" Elements ")]
    [SerializeField] private Button purchaseButton;
    [SerializeField] private SkinButton[] skinButtons;

    [Header(" Pricing ")]
    [SerializeField] private int skinPrice;
    [SerializeField] private Text priceText;

    [Header(" Events ")]
    public static Action<int> onSkinSelected;

    private void Awake()
    {
        UnlockSkin(0);

        priceText.text = skinPrice.ToString();
    }


    // Start is called before the first frame update
    IEnumerator Start()
    {
        ConfigureButtons();

        UpdatePurchaseButton();

        yield return null; // Wait for one frame to let the buttons configure 

        SelectSkin(GetLastSelectedSkin());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UnlockSkin(3);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    private void ConfigureButtons()
    {
        for (int i = 0; i < skinButtons.Length; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("skinButton" + i) == 1;

            skinButtons[i].Configure(skins[i], unlocked);

            int skinIndex = i;

            skinButtons[i].GetButton().onClick.AddListener(() => SelectSkin(skinIndex)); 
        }
    }

    public void UnlockSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("skinButton" + skinIndex, 1);
        skinButtons[skinIndex].Unlock();
    }

    private void UnlockSkin(SkinButton skinButton)
    {
        int skinIndex = skinButton.transform.GetSiblingIndex(); // Get the index of the skinButton in the hierarchy
        UnlockSkin(skinIndex);
    }

    private void SelectSkin(int skindIndex)
    {
        for(int i = 0;i < skinButtons.Length;i++)
        {
            if(skindIndex == i)
            {
                skinButtons[i].Select();
            }
            else
            {
                skinButtons[i].Deselect();
            }
        }

        onSkinSelected?.Invoke(skindIndex);

        SaveLastSelectedSkin(skindIndex);
    }

    public void PurchaseSkin()
    {
        List<SkinButton> skinButtonsList = new List<SkinButton>();

        for(int i = 0;i < skinButtons.Length;i++)
        {
            if (!skinButtons[i].IsUnlocked())
            {
                skinButtonsList.Add(skinButtons[i]);
            }
        }


        if (skinButtonsList.Count <= 0)
            return;

        SkinButton randomSkinButton = skinButtonsList[UnityEngine.Random.Range(0, skinButtonsList.Count)];

        UnlockSkin(randomSkinButton);
        SelectSkin(randomSkinButton.transform.GetSiblingIndex());

        DataManager.instance.RemoveCoins(skinPrice);
     
        UpdatePurchaseButton();
    }

    public void UpdatePurchaseButton()
    {
        if(DataManager.instance.GetCoins() < skinPrice)
        {
            purchaseButton.interactable = false;
        }
        else
        {
            purchaseButton.interactable = true;
        }
    }

    private int GetLastSelectedSkin()
    {
        return PlayerPrefs.GetInt("lastSelectedSkin", 0);
    }

    private void SaveLastSelectedSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("lastSelectedSkin", skinIndex);
    }
}
