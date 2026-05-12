using UnityEngine;

namespace TPDev
{
    public class Belt : MonoBehaviour
    {
        private ConveyorBelt conveyor;

        #region UnityMethods
        void Start()
        {
            conveyor = GetComponentInParent<ConveyorBelt>();

            if (conveyor == null)
            {
                Debug.LogError("no hay conveyorBelt");
            }
        }

        void OnCollisionStay(Collision collision)
        {
            if (conveyor == null) return;

            Rigidbody rb = collision.rigidbody;
            if (rb != null)
            {
                Vector3 dir = (conveyor.conveyorEnd.position - conveyor.conveyorStart.position).normalized;

                rb.linearVelocity = new Vector3(
                    dir.x * conveyor.speed,
                    rb.linearVelocity.y,
                    dir.z * conveyor.speed
                );
            }
        }

        #endregion UnityMethods
    }
}