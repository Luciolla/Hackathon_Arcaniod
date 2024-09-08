using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
	public class UIView : MonoBehaviour
	{
		public event Action OnPlayButtonClicked;
		public event Action OnMenuButtonClicked;
		public event Action OnExitButtonClicked;
		
		[SerializeField] private Button _playButton;
		[SerializeField] private Button _menuButton;
		[SerializeField] private Button _resumeButton;
		[SerializeField] private Button _exitButton;

		private void Start()
		{
			_playButton.onClick.AddListener(() => OnPlayButtonClicked?.Invoke());
			_playButton.onClick.AddListener(ShowMenuButton);
			_resumeButton.onClick.AddListener(() => OnMenuButtonClicked?.Invoke());
			_resumeButton.onClick.AddListener(ShowMenuButton);
			_menuButton.onClick.AddListener(() => OnMenuButtonClicked?.Invoke());
			_menuButton.onClick.AddListener(ShowMenuButton);
			_exitButton.onClick.AddListener(() => OnExitButtonClicked?.Invoke());

			EventBus.BallsDeath += HideMenuButton;
		}

		private void ShowMenuButton()
		{
			GameObject obj;
			
			(obj = _menuButton.gameObject).SetActive(!_menuButton.gameObject.activeSelf);
			_resumeButton.gameObject.SetActive(!obj.activeSelf);
		}

		private void HideMenuButton() =>
			_menuButton.gameObject.SetActive(false);

		public void DisplayGameRunningState(bool isRunning) =>
			Debug.Log(isRunning ? "Game is running" : "Game is stopped");
	}
}