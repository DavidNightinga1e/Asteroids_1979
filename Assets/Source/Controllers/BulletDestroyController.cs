using Source.Components;
using Source.Events;

namespace Source.Controllers
{
    public class BulletDestroyController : IController, IAwakable
    {
        public void Awake()
        {
            EventPool.OnBulletHit.AddListener(OnBulletHit);
        }

        private void OnBulletHit(BulletComponent arg0)
        {
            UnityEngine.Object.Destroy(arg0.gameObject);
        }
    }
}