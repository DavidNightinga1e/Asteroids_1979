using Source.Interfaces;
using Source.Models;
using Source.Views;
using UnityEngine;

namespace ServiceLocators
{
	public class PlayerLaserService : Service, IUpdatable, IStartable
	{
		private const int MaxCharges = 3;
		private const float ChargeReloadTimeSeconds = 5f;
		private const float LaserFadeDelay = 0.5f;

		private float _laserFadeTime;

		private readonly PlayerModel _playerModel;
		private readonly IPlayerInputProvider _playerInputProvider;
		private readonly IMovable _playerMovable;
		private readonly IRotatable _playerRotatable;
		private readonly ILaserView _laserView;
		
		public PlayerLaserService(PlayerModel model, IPlayerInputProvider inputProvider, IMovable playerMovable,
			IRotatable playerRotatable, ILaserView laserView)
		{
			_playerInputProvider = inputProvider;
			_playerModel = model;
			_playerMovable = playerMovable;
			_playerRotatable = playerRotatable;
			_laserView = laserView;
		}

		public void Update()
		{
			UpdateShooting();
			UpdateReloading();
			UpdateLaserFade();
		}

		private void UpdateLaserFade()
		{
			if (Time.time > _laserFadeTime)
				_laserView.SetVisible(false);
		}

		public void Start()
		{
			_playerModel.LaserCharges = MaxCharges;
			_laserView.SetVisible(false);
		}

		private void UpdateShooting()
		{
			if (_playerInputProvider.IsLaserFire &&
			    _playerModel.LaserCharges > 0)
			{
				_playerModel.LaserCharges--;
				SetLaserVisible();
				if (Time.time > _playerModel.NextLaserChargeTime)
					ResetReloadingTime();
			}

			if (Time.time < _laserFadeTime)
			{
				Shoot();
			}
		}

		private void Shoot()
		{
			// ReSharper disable once Unity.PreferNonAllocApi
			// https://docs.unity3d.com/ScriptReference/Physics2D.RaycastNonAlloc.html
			// will be deprecated
			RaycastHit2D[] hits = Physics2D.RaycastAll(
				_playerMovable.GetPosition(),
				_playerRotatable.GetUp(),
				float.PositiveInfinity,
				LayerMask.GetMask("Enemy"));

			if (hits.Length == 0)
				return;

			foreach (RaycastHit2D hit in hits)
			{
				Collider2D collider = hit.collider;
				var enemyView = collider.GetComponentInParent<EnemyView>();
				Object.Destroy(enemyView.gameObject);
			}
		}

		private void UpdateReloading()
		{
			if (_playerModel.LaserCharges < MaxCharges &&
			    Time.time > _playerModel.NextLaserChargeTime)
			{
				_playerModel.LaserCharges++;

				if (_playerModel.LaserCharges < MaxCharges)
					ResetReloadingTime();
			}
		}

		private void ResetReloadingTime()
		{
			_playerModel.NextLaserChargeTime = Time.time + ChargeReloadTimeSeconds;
		}

		private void SetLaserVisible()
		{
			_laserView.SetVisible(true);
			_laserFadeTime = Time.time + LaserFadeDelay;
		}
	}
}