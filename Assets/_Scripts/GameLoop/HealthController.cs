using TMPro;
using UnityEngine;

namespace _Scripts.GameLoop
{
	public class HealthController : MonoBehaviour
	{
		[SerializeField] private TMP_Text _health;

		public int Health {get; private set;} = 3;

		public void LoseHealth()
		{
			Health--;
			TestUpdate();
		}

		public void GetHealth()
		{
			Health++;
			TestUpdate();
		}

		public void RestoreHealth()
		{
			Health = 3;
			TestUpdate();
		}

		private void TestUpdate() =>
			_health.text = Health.ToString();

	}
}