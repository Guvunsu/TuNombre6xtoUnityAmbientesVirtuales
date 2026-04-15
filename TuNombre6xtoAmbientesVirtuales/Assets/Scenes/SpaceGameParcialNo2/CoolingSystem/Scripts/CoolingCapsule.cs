using UnityEngine;

public class CoolingCapsule : MonoBehaviour
{
    public int personalHeatLevel = 100;
    private Material capsuleMaterial;

    private Color coldColor = new Color32(165, 198, 255, 255); 
    private Color hotColor = new Color32(248, 0, 0, 255);      

    public void Start()
    {
        capsuleMaterial = GetComponent<Renderer>().material;
    }

    private void Update()
    {
    }

    public void decreaseHeatLevel()
    {
        personalHeatLevel--;
        UpdateColor();
    }

    private void UpdateColor()
    {
        float t = 1f - (float)personalHeatLevel / 100;
        t = Mathf.Clamp01(t);

        capsuleMaterial.color = Color.Lerp(coldColor, hotColor, t);
    }
}
