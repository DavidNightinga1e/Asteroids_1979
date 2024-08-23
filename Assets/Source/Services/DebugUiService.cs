using Source.Models;
using Source.Views;

namespace ServiceLocators
{
	public class DebugUiService : Service, IUpdatable
	{
		private readonly PlayerModel _playerModel;
		private readonly DebugUiView _view;
		
		public DebugUiService(DebugUiView view, PlayerModel playerModel)
		{
			_playerModel = playerModel;
			_view = view;
		}
		
		public void Update()
		{
			_view.Text.text = $"position:{_playerModel.GetPosition()}\n" +
			                  $"rotation:{_playerModel.GetRotation()}\n" +
			                  $"velocity:{_playerModel.Speed.magnitude}\n" +
			                  $"angularvelocity:{_playerModel.AngularSpeed}";
		}
	}
}