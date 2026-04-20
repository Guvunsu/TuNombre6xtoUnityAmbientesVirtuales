using System.Collections;
using UnityEngine;
public enum DieOrAliveFSM
{
    ACTIVATE_WINPANEL,
    ACTIVATE_LOSEPANEL,
    PLAYING
}
public class WinOrLooseCondition : MonoBehaviour
{
    public PanelManager panelManager_script;
    public DieOrAliveFSM _fsm = DieOrAliveFSM.PLAYING;

    void Start()
    {
        StartCoroutine(TimeToLose(210f));
    }
    void Update()
    {
        VerifyingWinOrLose();
    }
    void VerifyingWinOrLose()
    {
        switch (_fsm)
        {
            case DieOrAliveFSM.PLAYING:
                break;

            case DieOrAliveFSM.ACTIVATE_WINPANEL:
                break;

            case DieOrAliveFSM.ACTIVATE_LOSEPANEL:
                break;
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && gameObject.CompareTag("Earth"))
        {
            if (_fsm == DieOrAliveFSM.PLAYING)
            {
                _fsm = DieOrAliveFSM.ACTIVATE_WINPANEL;
                panelManager_script.VictoryPanel();

                gameObject.SetActive(false);
                collision.gameObject.SetActive(false);

            }
        }
    }
    public IEnumerator TimeToLose(float timer)
    {
        yield return new WaitForSeconds(timer);

        if (_fsm == DieOrAliveFSM.PLAYING)
        {
            _fsm = DieOrAliveFSM.ACTIVATE_LOSEPANEL;
            panelManager_script.LosePanel();
        }
    }
}