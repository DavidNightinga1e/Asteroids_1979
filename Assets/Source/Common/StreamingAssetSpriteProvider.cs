using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Common
{
    public class StreamingAssetSpriteProvider : MonoBehaviour
    {
        private static StreamingAssetSpriteProvider s_instance;

        public static StreamingAssetSpriteProvider Instance
        {
            get
            {
                if (s_instance)
                    return s_instance;

                s_instance = FindObjectOfType<StreamingAssetSpriteProvider>();

                if (s_instance)
                    return s_instance;

                var gameObject = new GameObject(nameof(StreamingAssetSpriteProvider));
                s_instance = gameObject.AddComponent<StreamingAssetSpriteProvider>();
                return s_instance;
            }
        }

        private void Awake()
        {
            var isNullOrThis = ReferenceEquals(s_instance, null) || ReferenceEquals(this, s_instance);
            if (!isNullOrThis)
                DestroyImmediate(this);
        }

        private const int DefaultAssetNumber = 10;

        private readonly Dictionary<string, Sprite> _loadedSprites = new Dictionary<string, Sprite>(DefaultAssetNumber);

        // This dictionary provides two things:
        // 1. We can decide if the path is already being loaded 
        // 2. Coroutine doesn't lose requested callbacks 
        private readonly Dictionary<string, List<Action<Sprite>>> _resultCallbacks =
            new Dictionary<string, List<Action<Sprite>>>();

        /// <summary>
        /// If file at path is loaded then method calls result immediately
        /// Otherwise it will create coroutine and the result will be called after file is loaded
        /// </summary>
        /// <param name="path"></param>
        /// <param name="onLoad"></param>
        public void LoadSpriteAsync(string path, Action<Sprite> onLoad)
        {
            if (_loadedSprites.ContainsKey(path))
            {
                onLoad?.Invoke(_loadedSprites[path]);
                return;
            }

            if (_resultCallbacks.ContainsKey(path))
            {
                _resultCallbacks[path].Add(onLoad);
                return;
            }

            if (!File.Exists(path))
                throw new ArgumentException("File at path doesn't exist", path);
            _resultCallbacks[path] = new List<Action<Sprite>> {onLoad};
            StartCoroutine(LoadSpriteCoroutine()); // one path - one coroutine

            IEnumerator LoadSpriteCoroutine()
            {
                yield return new WaitForSeconds(Random.Range(1, 3)); // fake step 1

                var fileInfo = new FileInfo(path);

                var fileInfoLength = (int) fileInfo.Length;
                // that's a tight spot, yet I don't care that much,
                // because int.MaxValue bytes is 2 Gb, and in general texture doesn't
                // take that much space

                var bytesBuffer = new byte[fileInfoLength];
                using var fileStream = new FileStream(path, FileMode.Open);
                yield return fileStream.ReadAsync(bytesBuffer, 0, fileInfoLength);

                var texture = new Texture2D(2, 2)
                {
                    name = $"Loaded Texture [{path}]"
                };

                yield return new WaitForSeconds(Random.Range(1, 3)); // fake step 2

                texture.LoadImage(bytesBuffer); // actually that is the slowest part,
                // but I don't see any way of loading this async

                var sprite = Sprite.Create(texture,
                    new Rect(0.0f, 0.0f, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f),
                    100f);

                _loadedSprites.Add(path, sprite);

                foreach (var callback in _resultCallbacks[path])
                    callback?.Invoke(sprite);
                _resultCallbacks.Remove(path); // path is loaded, remove all callbacks for this path
            }
        }
    }
}