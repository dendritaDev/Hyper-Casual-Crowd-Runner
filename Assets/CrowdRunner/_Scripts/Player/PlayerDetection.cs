using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerDetection : MonoBehaviour
{

    [Header(" Elements ")]
    [SerializeField] private CrowdSystem crowdSystem;

    [Header("Events")]
    public static Action onDoorsHit;
    public static Action onCoinHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.IsGameState())
        {
            DetectColliders();
        }
        
    }

    private void DetectColliders()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, crowdSystem.GetCrowdRadius());

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i].TryGetComponent(out Doors door)) //si detecta el componente Doors podemos llamarlo desde la referencia door
            {

                int bonusAmount = door.GetBonusAmount(transform.position.x);
                BonusType bonusType = door.GetBonusType(transform.position.x);

                door.Disable();

                onDoorsHit?.Invoke();

                crowdSystem.ApplyBonus(bonusType, bonusAmount);

            }
            else if (detectedColliders[i].CompareTag("Finish"))
            {
                PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);

                GameManager.instance.SetGameState(GameManager.GameState.LevelComplete);
            }
            else if (detectedColliders[i].CompareTag("Coin"))
            {
                Destroy(detectedColliders[i].gameObject);

                DataManager.instance.AddCoins(1);

                onCoinHit?.Invoke();
            }
        }
    }
}
