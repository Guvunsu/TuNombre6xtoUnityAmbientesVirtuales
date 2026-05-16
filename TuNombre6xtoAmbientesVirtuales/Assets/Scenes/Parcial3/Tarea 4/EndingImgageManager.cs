using UnityEngine;

public class EndingImageManager : MonoBehaviour
{
    [Header("XR Camera")]
    [SerializeField] private Transform xrCamera;

    [Header("Panels de Finales")]
    [SerializeField] private GameObject goodEndingPanel;
    [SerializeField] private GameObject badEndingPanel;
    [SerializeField] private GameObject neutralEndingPanel;

    [Header("Distancia del Canvas")]
    [SerializeField] private float distancePlayerFromCanvas = 2f;

    private void Start()
    {
        DisableAllEndings();
    }

    private void DisableAllEndings()
    {
        goodEndingPanel.SetActive(false);
        badEndingPanel.SetActive(false);
        neutralEndingPanel.SetActive(false);
    }

    public void ShowGoodEnding()
    {
        ShowEnding(goodEndingPanel);
    }

    public void ShowBadEnding()
    {
        ShowEnding(badEndingPanel);
    }

    public void ShowNeutralEnding()
    {
        ShowEnding(neutralEndingPanel);
    }

    private void ShowEnding(GameObject endingPanel)
    {
        DisableAllEndings();
        MovePanelInFrontOfPlayer(endingPanel);
        endingPanel.SetActive(true);
    }

    private void MovePanelInFrontOfPlayer(GameObject panel)
    {
        if (xrCamera == null) return;

        Transform panelTransform = panel.transform;
        panelTransform.position = xrCamera.position + xrCamera.forward * distancePlayerFromCanvas;
        panelTransform.LookAt(xrCamera);
        panelTransform.Rotate(0f, 180f, 0f); // Corregir orientación si este no sale como ustedes quieren chicos :3
    }
}