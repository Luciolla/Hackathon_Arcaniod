namespace _Scripts.UI
{
	public class UIPresenter
	{
		private UIModel _model;
		private UIView _view;

		public UIPresenter(UIModel model, UIView view)
		{
			_model = model;
			_view = view;

			_view.OnPlayButtonClicked += HandlePlayButton;
			_view.OnMenuButtonClicked += HandleMenuButton;
			_view.OnExitButtonClicked += HandleExitButton;
		}

		private void HandlePlayButton()
		{
			if(!_model.IsGameRunning)
			{
				_model.StartGame();
			}

			_view.DisplayGameRunningState(_model.IsGameRunning);
		}

		private void HandleMenuButton()
		{
			_model.EnterMenu();

			_view.DisplayGameRunningState(_model.IsGameRunning);
		}

		private void HandleExitButton()
		{
			_model.StopGame();

			_view.DisplayGameRunningState(_model.IsGameRunning);
		}
	}
}