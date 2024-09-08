using System;

namespace _Scripts
{
	public static class EventBus
	{
		public static event Action HealthLose;
		public static event Action BallsDeath;
		public static event Action ExplodeBlock;

		public static void OnHealthLose() =>
			HealthLose?.Invoke();

		public static void OnBallsDeath() =>
			BallsDeath?.Invoke();

		public static void OnExplodeBlock() =>
			ExplodeBlock?.Invoke();
	}
}