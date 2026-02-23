using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] ShootYou script_ShootYou;

    public void Fire()
    {
        script_ShootYou.initializeGame = true;
    }
}
