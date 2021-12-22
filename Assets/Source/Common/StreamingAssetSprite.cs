using System.IO;
using UnityEngine;

namespace Source.Common
{
    public class StreamingAssetSprite : MonoBehaviour
    {
        public string relativePath;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            this.AutoGetChildComponent(out _spriteRenderer);
        }

        private void Start()
        {
            var path = Path.Combine(Application.streamingAssetsPath, relativePath);
            StreamingAssetSpriteProvider.Instance.LoadSpriteAsync(path, OnLoad);
        }

        private void OnLoad(Sprite obj)
        {
            if (_spriteRenderer)
                _spriteRenderer.sprite = obj;
        }
    }
}