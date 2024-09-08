using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Other
{
    public class WallTransparencyOnOff : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _walls = new List<GameObject>();

        private bool _isTransparent;

        public void ChangeTransparency()
        {
            _isTransparent = !_isTransparent;
            foreach (var wall in _walls)
            {
                wall.layer = _isTransparent ? 0 : 1;
            }
        }
    }
}