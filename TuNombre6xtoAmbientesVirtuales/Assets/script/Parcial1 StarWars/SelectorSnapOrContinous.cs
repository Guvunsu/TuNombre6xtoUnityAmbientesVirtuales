using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
public class SelectorSnapOrContinous : MonoBehaviour
{
    public SnapTurnProvider snapTurn; //movement 
    public ContinuousMoveProvider continousMove; //turning
    public GameObject panelDesactivation;     
    public void SnapActivation()
    {
        snapTurn.enabled = true;
        continousMove.enabled = false;
        gameObject.SetActive(false);
        panelDesactivation.SetActive(false);
    }
    public void ContinuosActivation()
    {
        snapTurn.enabled = false;
        continousMove.enabled = true;
        gameObject.SetActive(false);
        panelDesactivation.SetActive(false);
    }
}