using System;
using UnityEngine;

namespace _Scripts
{
	public class Bonus : MonoBehaviour
	{
		[field: SerializeField] public BonusType GetBonusType {get; private set;}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if(other.gameObject.layer != 7) return;

			switch (GetBonusType)
			{
				case BonusType.None:
					break;
				case BonusType.Plus:
					ExpandScale(other, true);
					break;
				case BonusType.Minus:
					ExpandScale(other, false);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			
			Destroy(this.gameObject);
		}

		private void ExpandScale(Component obj, bool expand)
		{
			var newScale = obj.gameObject.transform.localScale;
			newScale.y *= expand? 1.1f : 0.9f; 
			obj.transform.localScale = newScale;
			
			Debug.Log("New scale" + newScale);
		}
	}
}