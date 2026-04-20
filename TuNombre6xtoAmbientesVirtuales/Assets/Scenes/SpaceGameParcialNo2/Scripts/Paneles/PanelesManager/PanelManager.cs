using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum GameStatefsm
{
    WIN,
    LOSE,
    OPTIONS,
    PLAYING
}
public class PanelManager : MonoBehaviour
{
    public GameStatefsm gameFSM;
    [SerializeField] GameObject panelWin;
    [SerializeField] GameObject panelLose;
    [SerializeField] GameObject panelOptions;
    private PlayerInput playerInput;
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        gameFSM = GameStatefsm.PLAYING;

        playerInput.actions["OpenOptions"].performed += ctx => OptionsPanel();
        playerInput.actions["OpenWin"].performed += ctx => VictoryPanel();
        playerInput.actions["OpenLose"].performed += ctx => LosePanel();
    }
    public void LoadMenuPrincipal()
    {
        SceneManager.LoadScene("MenuScene");
    }  
    public void Restart()
    {
        SceneManager.LoadScene("SpaceGameParcialNo2");
    }
    public void QuitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    public void OptionsPanel()
    {
        if (gameFSM != GameStatefsm.PLAYING)
        {
            return;
        }
        gameFSM = GameStatefsm.OPTIONS;
        panelOptions.SetActive(true);
        panelWin.SetActive(false);
        panelLose.SetActive(false);
    }
    public void VictoryPanel()
    {
        if (gameFSM != GameStatefsm.PLAYING)
        {
            return;
        }
        gameFSM = GameStatefsm.WIN;
        panelWin.SetActive(true);
        panelLose.SetActive(false);
        panelOptions.SetActive(false);
    }
    public void LosePanel()
    {
        if (gameFSM != GameStatefsm.PLAYING)
        {
            return;
        }
        gameFSM = GameStatefsm.LOSE;
        panelLose.SetActive(true);
        panelWin.SetActive(false);
        panelOptions.SetActive(false);
    }
}