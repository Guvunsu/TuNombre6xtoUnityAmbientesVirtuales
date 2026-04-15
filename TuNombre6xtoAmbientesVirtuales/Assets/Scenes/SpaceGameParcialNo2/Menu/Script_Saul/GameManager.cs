using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;
    private Coroutine gameState;

    public float gameTime = 0f;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        ChangeState(GameState.Menu);
    }

    void Update()
    {
        if (currentState == GameState.Alive)
        {
            gameTime += Time.deltaTime;
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;

        if (gameState != null)
        {
            StopCoroutine(gameState);
        }
        gameState = StartCoroutine(StateRoutine(newState));
    }
    IEnumerator StateRoutine(GameState state)
    {
        switch (state)
        {
            case GameState.None:
                break;
            case GameState.Menu:

                break;
            case GameState.Alive:
                Debug.Log("Jugador vivo");


                //ToDo: Logica del player vivo jejesoyelsori
                yield return null;
                break;

            case GameState.Dead:
                Debug.Log("Jeje se murio el player");

                yield return null;

                //ToDo: Llamar al ScreenManager pa avisarle que estamos moridos y probablemente a los otros managers
                break;
            case GameState.Win:
                Debug.Log("Jeje ganamos");

                yield return null;

                //ToDo: Llamar al ScreenManager pa avisarle que estamos moridos y probablemente a los otros managers
                break;
        }
    }

}
public enum GameState
{
    //ToDo: Añadir más estados o algo
    None,
    Menu,
    Alive,
    Dead,
    Win
}
