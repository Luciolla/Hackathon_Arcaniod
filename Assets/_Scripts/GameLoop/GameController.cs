using _Scripts.Field;
using _Scripts.Other;
using _Scripts.UI;
using UnityEditor;
using UnityEngine;

namespace _Scripts.GameLoop
{
	public class GameController : MonoBehaviour
	{
		[SerializeField] private UIView _view;
		[SerializeField] private FieldGenerator _fieldGenerator;
		[SerializeField] private BallComponent _ball;
		[SerializeField] private GameObject _menuPanel;
		[SerializeField] private WallTransparencyOnOff _walls;
		[SerializeField] private HealthController _health;

		private UIModel _model;
		private UIPresenter _presenter;
		private int _winCount = 0;

		private void Start()
		{
			_model = new UIModel();
			_presenter = new UIPresenter(_model, _view);

			_model.OnGameStarted += HandleGameStarted;
			_model.OnMenuEnter += HandleGameStopped;
			_model.OnGameStopped += HandleGameExit;

			EventBus.HealthLose += HealthLose;
			EventBus.BallsDeath += PermanentDeath;
			EventBus.ExplodeBlock += WinCount;
		}

		private void HealthLose()
		{
			if(_health.Health > 1)
				_health.LoseHealth();
			else
				EventBus.OnBallsDeath();
		}

		private void PermanentDeath()
		{
			_model.EnterMenu();
		}

		private void HandleGameStarted()
		{
			Debug.Log("Start");
			_fieldGenerator.RestartField();
			_health.RestoreHealth();

			RefreshScene(true);
		}

		private void HandleGameStopped()
		{
			Debug.Log("Stop");
			_menuPanel.SetActive(!_menuPanel.activeSelf);
			_ball.IsBallInGame = !_menuPanel.activeSelf;
			_walls.ChangeTransparency();
		}

		private void HandleGameExit()
		{
#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
#endif
			Application.Quit();

		}

		private void WinCount()
		{
			_winCount++;
			
			if(_winCount==_fieldGenerator.GetMaxBlock)
				PermanentDeath();
			
			Debug.Log(_fieldGenerator.GetMaxBlock-_winCount);
		}

		private void RefreshScene(bool value)
		{
			_ball.IsBallInGame = value;
			_menuPanel.SetActive(!value);
			_ball.BallRestart();
			_ball.BallUnlock();
			_walls.ChangeTransparency();
		}
	}

}