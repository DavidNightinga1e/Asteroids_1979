using Source.Models;
using Source.Views;
using UnityEngine;

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
			float laserCooldown = _playerModel.NextLaserChargeTime - Time.time > 0
				? _playerModel.NextLaserChargeTime - Time.time
				: 0;

			_view.Text.text = $"position:{_playerModel.GetPosition()}\n" +
			                  $"rotation:{_playerModel.GetRotation()}\n" +
			                  $"velocity:{_playerModel.Speed.magnitude}\n" +
			                  $"angularvelocity:{_playerModel.AngularSpeed}\n" +
			                  $"laserCharges:{_playerModel.LaserCharges}\n" +
			                  $"laserCooldown:{laserCooldown}";
		}
	}
}