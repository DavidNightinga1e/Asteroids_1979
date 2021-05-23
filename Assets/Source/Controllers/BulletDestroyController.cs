using System;
using Source.Components;
using Source.Events;
using UnityEngine;

namespace Source.Controllers
{
    public class BulletDestroyController : MonoBehaviour
    {
        private void Awake()
        {
            EventPool.OnBulletHit.AddListener(OnBulletHit);
        }

        private void OnBulletHit(BulletComponent arg0)
        {
            Destroy(arg0.gameObject);
        }
    }
}