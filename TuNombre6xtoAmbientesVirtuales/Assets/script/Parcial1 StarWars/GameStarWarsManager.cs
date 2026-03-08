using UnityEngine;
using System.Collections;
public class GameStarWarsManager : MonoBehaviour
{
    public ShootCanon[] canones;
    public GameObject[] pisosColapsables;
    public GameObject greenSable;
    public GameObject blueSable;
    public AudioSource alarma;
    float tiempoEntreColapso = 9.10f;
    public void ActivarEventoFinal()
    {
        foreach (var c in canones)
            c.Activation();

        greenSable.SetActive(true);
        blueSable.SetActive(true);
        alarma.Play();
        StartCoroutine(ColapsarPisos());
    }
    IEnumerator ColapsarPisos()
    {
        foreach (GameObject piso in pisosColapsables)
        {
            yield return new WaitForSeconds(tiempoEntreColapso); 
            piso.SetActive(false);
        }
    }
}