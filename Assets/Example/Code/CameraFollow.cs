using UnityEngine;

namespace Platformer2DSystem.Example
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed;

        private void Update()
        {
            Vector3 position = transform.position;
            position.x = Maths.Damp(transform.position.x, target.position.x, speed, Time.deltaTime);
            transform.position = position;
        }
    }
}
