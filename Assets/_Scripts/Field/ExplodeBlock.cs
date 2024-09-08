using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace _Scripts.Field
{
    public class ExplodeBlock : MonoBehaviour, IDamageable
    {
        [SerializeField] private List<GameObject> _bonus;

        private Random _rand = new Random();
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.GetComponent<IDamageable>() != null)
                return;

            InstantiateBonus();
            this.gameObject.SetActive(false);
            EventBus.OnExplodeBlock();
        }

        private void InstantiateBonus()
        {
            var value = _rand.Next(0, 101);

            if(value is <= 23 or >= 43)
                return;

            var obj = _bonus[_rand.Next(0, _bonus.Count)];
            Instantiate(obj, this.transform.position, obj.transform.rotation);
        }
    }
}
