using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header( " Elements ")]
    [SerializeField] private CrowdSystem crowdSystem;
    [SerializeField] private PlayerAnimator playerAnimator;

    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float roadWidth = 10;
    private bool canMove;

    [Header(" Control ")]
    [SerializeField] private float slideSpeed;
    private Vector3 clickedScreenPosition;
    private Vector3 clickedPlayerPosition;


    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }


    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            MoveForward();
            ManageControl();
        }
        
    }


    private void GameStateChangedCallback(GameManager.GameState state)
    {
        if(state == GameManager.GameState.Game)
        {
            StartMoving();
        }
        else if (state == GameManager.GameState.Gameover || state == GameManager.GameState.LevelComplete)
        {
            StopMoving();
        }
    }


    private void StartMoving()
    {
        canMove = true;
        playerAnimator.Run();
    }

    private void StopMoving()
    {
        canMove = false;
        playerAnimator.Idle();
    }

    private void ManageControl()
    {
        if (Input.GetMouseButtonDown(0)) //pulsamos pantalla
        {
            clickedScreenPosition = Input.mousePosition;
            clickedPlayerPosition = transform.position;
        }
        else if (Input.GetMouseButton(0)) //mantenemos pulsado
        {
            //calcular la diferencia entre la posicion del raton y la posicion del raton cuando pulsamos
            //lo normalizamos segun el ancho de la pantalla y lo multiplicamos por la velocidad de deslizamiento
            float xScreenDifference = Input.mousePosition.x - clickedScreenPosition.x;
            xScreenDifference /= Screen.width;
            xScreenDifference *= slideSpeed;

            //nos aseguramos de que esto solo modifique la posicion en el eje x
            Vector3 position = transform.position;
            position.x = clickedPlayerPosition.x + xScreenDifference;

            position.x = Mathf.Clamp(position.x, -roadWidth / 2 + crowdSystem.GetCrowdRadius(), roadWidth / 2-crowdSystem.GetCrowdRadius());

            transform.position = position;

        }
    }

    private void MoveForward()
    {
        transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
    }

}

