using System.Collections.Generic;
using UnityEngine;

namespace _Scripts
{
    public class MenuElementsRotation : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _objectList = new List<GameObject>();
        [SerializeField, Range(0.1f, 5f)] private float _rotationSpeed;

        private void FixedUpdate()
        {
            foreach (GameObject obj in _objectList)
                obj.transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime * 100f);
        }
    }
}