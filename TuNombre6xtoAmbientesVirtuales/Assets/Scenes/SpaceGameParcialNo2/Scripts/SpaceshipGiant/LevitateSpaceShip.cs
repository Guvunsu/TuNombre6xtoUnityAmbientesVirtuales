using UnityEngine;

public class LevitateSpaceShip : MonoBehaviour
{
    [Header("levitar")]
    public Transform target; 

    [Header("Parametros")]
    public float amplitude = 0.5f; 
    public float speed = 1f;  

    private Vector3 startPos;

    void Start()
    {
        if (target == null)
            target = transform;

        startPos = target.position;
    }
    void Update()
    {
        // PingPong oscila entre 0 y amplitude
        // Movimiento arriba y abajo
        float offset = Mathf.PingPong(Time.time * speed, amplitude);
        target.position = new Vector3(startPos.x, startPos.y + offset, startPos.z);
    }
}
