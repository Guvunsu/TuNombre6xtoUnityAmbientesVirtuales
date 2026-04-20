using UnityEngine;
using Unity.XR;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.InputSystem;
using System.Collections;

public class TurretVR : MonoBehaviour
{
    [SerializeField] XRRayInteractor _playerRayInterector;
    [SerializeField] Transform turret;
    [SerializeField] Transform _rightHand;
    
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed;
    
    RaycastHit hit;

    [SerializeField] bool isInTurret = false;
    [SerializeField] bool isReloded = false;
    [SerializeField] bool hasMeteorPos = false;
    [SerializeField] bool isShot = false;

    Quaternion _turretRotation;
    Vector3 turretDirection;

    private void Update()
    {
        if (isInTurret)
        {
            TurretMode();
        }
        if(isShot && isReloded)
        {
            Shot();
        }
    }

    public void TurretMode()
    {
        if (!hasMeteorPos)
        {
            Debug.Log("Torreta" + " isInTurret: " + isInTurret);
            _turretRotation = _rightHand.rotation;
            turret.rotation = _turretRotation;
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Dispare");
                Ray ray = new Ray(_rightHand.position, _rightHand.forward);
                RaycastHit _hit;
                if(Physics.Raycast(ray, out _hit))
                {
                    if (_hit.collider.CompareTag("Earth"))
                    {
                        hasMeteorPos = true;
                        turretDirection = _hit.transform.position - turret.position;
                        _turretRotation = Quaternion.LookRotation(turretDirection);
                        turret.rotation = _turretRotation;
                    }
                }
            }
        }
        else
        {
            turret.rotation = _turretRotation;
            isShot = true;
        }
    }
    
    void Shot()
    {
        hasMeteorPos = false;
        bullet.transform.position += turretDirection.normalized * bulletSpeed * Time.deltaTime;
        StartCoroutine(shotTime());
    }

    public void Reloded()
    {
        isReloded = true;
    }

    public void IsInTurret()
    {
        isInTurret = true;
    }

    IEnumerator shotTime()
    {
        yield return new WaitForSeconds(3f);
        isReloded = false;
        isShot = false;
    }

}
