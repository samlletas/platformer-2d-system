using UnityEngine;

namespace Platformer2DSystem.Example
{
    public class Quit : MonoBehaviour
    {
        [SerializeField] private KeyCode key = KeyCode.Escape;

        private void Update()
        {
            if (Input.GetKeyDown(key))
            {
                Application.Quit();
            }
        }
    }
}
