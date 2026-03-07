using UnityEngine;
public class ShootCanon : MonoBehaviour
{
    public GameObject bullet_prefab;
    public Transform pointShoot;
    public float velocity = 20f;
    public float intervalo = 3f;
    private bool amI_Activated = false;
    void Start()
    {
        InvokeRepeating(nameof(Shooting), intervalo, intervalo);
    }
    void Shooting()
    {
        if (!amI_Activated) return;

        GameObject bullet = Instantiate(bullet_prefab, pointShoot.position, pointShoot.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = pointShoot.forward * velocity;
    }
    public void Activation()
    {
        amI_Activated = true;
    }
}