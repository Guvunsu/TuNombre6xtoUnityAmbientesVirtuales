         using UnityEngine;

namespace TPDev
{
    public class ConveyorBelt : MonoBehaviour
    {

        #region LocalVariables
        public Transform[] belts; 
        public Transform conveyorStart;
        public Transform conveyorEnd;

        public float speed = 2f;

        private float beltLength;
        private Vector3 direction;

        #endregion LocalVariables

        #region UnityMethods
        void Start()
        {
            direction = (conveyorEnd.position - conveyorStart.position).normalized;
            beltLength = Vector3.Distance(conveyorStart.position, conveyorEnd.position);
        }

        void Update()
        {
            foreach (Transform belt in belts)
            {
                float halfLength = GetHalfLengthAlongDirection(belt);

                belt.position += direction * speed * Time.deltaTime;

                float distance = Vector3.Dot(
                    (belt.position - conveyorStart.position),
                    direction
                );

                if (distance >= beltLength + halfLength)
                {
                    belt.position = conveyorStart.position - direction * halfLength;
                }
            }
        }

        float GetHalfLengthAlongDirection(Transform obj)
        {
            Renderer rend = obj.GetComponentInChildren<Renderer>();
            if (rend == null) return 0.5f;

            Vector3 size = rend.bounds.size;

            float projectedLength =
                Mathf.Abs(Vector3.Dot(direction, obj.right)) * size.x +
                Mathf.Abs(Vector3.Dot(direction, obj.up)) * size.y +
                Mathf.Abs(Vector3.Dot(direction, obj.forward)) * size.z;

            return projectedLength / 2f;
        }
    }

    #endregion UnityMethods
}
