using System;
using System.Collections;
using System.Collections.Generic;
using Source.Components;
using Source.Events;
using Source.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Controllers
{
    public class FlyingPlateBehaviourController : MonoBehaviour
    {
        private EnemySpawnSettingsComponent _settings;
        private PlayerComponent _playerComponent;
        private Camera _camera;

        private readonly List<FlyingPlateComponent> _flyingPlateComponents = new List<FlyingPlateComponent>();

        private (float min, float max) WaitTimeRange =>
        (
            _settings.currentFlyingPlateSpawnCooldown - _settings.FlyingPlateSpawnDispersion,
            _settings.currentFlyingPlateSpawnCooldown + _settings.FlyingPlateSpawnDispersion);

        private void Awake()
        {
            this.AutoFindComponent(out _settings);
            this.AutoFindComponent(out _playerComponent);
            this.AutoFindComponent(out _camera);
            
            EventPool.OnEnemyHit.AddListener(OnEnemyHit);
            EventPool.OnEnemySpawned.AddListener(OnEnemySpawned);
            EventPool.OnEnemyDestroyed.AddListener(OnEnemyDestroyed);
            StartCoroutine(EnemySpawnEnumerator());
        }

        private void OnEnemyDestroyed(EnemyComponent arg0)
        {
            if (arg0 is FlyingPlateComponent flyingPlateComponent)
            {
                _flyingPlateComponents.Remove(flyingPlateComponent);
            }
        }

        private void OnEnemySpawned(EnemyComponent arg0)
        {
            if (arg0 is FlyingPlateComponent flyingPlateComponent)
            {
                _flyingPlateComponents.Add(flyingPlateComponent);
            }
        }

        private void Update()
        {
            foreach (var flyingPlateComponent in _flyingPlateComponents)
            {
                flyingPlateComponent.transform.Translate(
                    (_playerComponent.transform.position - flyingPlateComponent.transform.position).normalized *
                    (_settings.flyingPlateSpeed * Time.deltaTime));
            }
        }

        IEnumerator EnemySpawnEnumerator()
        {
            while (_settings.spawn)
            {
                var (min, max) = WaitTimeRange;
                var waitTime = Random.Range(min, max);
                yield return new WaitForSeconds(waitTime);

                var point = RandomPointOnBounds();
                var i = Random.Range(0, _settings.flyingPlatePrefabs.Count);
                var instance = Instantiate(
                    _settings.flyingPlatePrefabs[i].gameObject,
                    point,
                    Quaternion.identity);
                var flyingPlateComponent = instance.GetComponent<FlyingPlateComponent>();
                EventPool.OnEnemySpawned.Invoke(flyingPlateComponent);
            }
        }

        private Vector2 RandomPointOnBounds()
        {
            Vector2 spawnExtents = _camera.GetOrthographicExtents();
            Vector2 random = Random.insideUnitCircle.normalized;
            return new Vector2(spawnExtents.x * random.x, spawnExtents.y * random.y);
        }

        private void OnEnemyHit(EnemyComponent arg0)
        {
            if (!(arg0 is FlyingPlateComponent flyingPlateComponent))
                return;

            Destroy(flyingPlateComponent.gameObject);
        }
    }
}
