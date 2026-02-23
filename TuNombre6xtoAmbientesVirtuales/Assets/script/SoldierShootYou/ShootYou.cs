using UnityEngine;

public class ShootYou : MonoBehaviour
{
    public bool initializeGame = false;

    [Header("Bullet things")]
    [SerializeField] GameObject prefabs_Bullet;
    [SerializeField] int shootYouTime;
    [SerializeField] float time;

    void Start()
    {
        shootYouTime = Random.Range(0, 6);
    }
    void Update()
    {
        time += Time.deltaTime;
        if (initializeGame)
        {
            prefabs_Bullet.SetActive(true);
            if (time >= shootYouTime)
            {
                prefabs_Bullet.transform.Translate(0, 0, 1);
            }
        }
    }
}
