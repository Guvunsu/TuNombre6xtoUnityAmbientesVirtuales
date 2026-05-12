using UnityEngine;

namespace TPDev
{
    public class itemSpawner : MonoBehaviour
    {
        public float spawnInterval = 2f;

        private float timer;

        void Update()
        {
            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                SpawnFromPool();
                timer = 0f;
            }
        }

        void SpawnFromPool()
        {
            Transform item = GetInactiveChild();

            if (item == null)
            {
                Debug.LogWarning("No more tlayudas in pool");
                return;
            }

            item.gameObject.SetActive(true);
            item.position = transform.position;
            item.rotation = transform.rotation;
        }

        Transform GetInactiveChild()
        {
            foreach (Transform child in transform)
            {
                if (!child.gameObject.activeSelf)
                    return child;
            }

            return null;
        }
    }
}