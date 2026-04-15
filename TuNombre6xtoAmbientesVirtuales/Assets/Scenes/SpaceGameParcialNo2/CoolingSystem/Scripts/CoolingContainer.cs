using UnityEngine;

public class CoolingContainer : MonoBehaviour
{
    public CoolingCapsule currentCapsule;

    private void OnTriggerEnter(Collider other)
    {
        CoolingCapsule capsule = other.GetComponent<CoolingCapsule>();

        if (capsule != null)
        {
            currentCapsule = capsule;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CoolingCapsule capsule = other.GetComponent<CoolingCapsule>();

        if (capsule != null && capsule == currentCapsule)
        {
            currentCapsule = null;
        }
    }
}