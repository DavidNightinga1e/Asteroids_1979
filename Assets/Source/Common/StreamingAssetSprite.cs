using System.IO;
using UnityEngine;

namespace Source.Common
{
    public class StreamingAssetSprite : MonoBehaviour
    {
        public string relativePath;

        private SpriteRenderer _spriteRenderer;
        private bool _isDestroyed;

        private void Awake()
        {
            this.AutoGetChildComponent(out _spriteRenderer);
        }

        private void Start()
        {
            var path = Path.Combine(Application.streamingAssetsPath, relativePath);
            StreamingAssetSpriteProvider.Instance.LoadSpriteAsync(path, OnLoad);
        }

        private void OnDestroy()
        {
            _isDestroyed = true;
        }

        private void OnLoad(Sprite obj)
        {
            if (_isDestroyed) // thing that you can't unsubscribe from asset provider makes garbage, cause 
                return; // this MonoBehaviour isn't removed till invocation

            _spriteRenderer.sprite = obj;
        }
    }
}