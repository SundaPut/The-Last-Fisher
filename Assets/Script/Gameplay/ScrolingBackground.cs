using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrolingBackground : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform[] sprites; // isi 2 sprite per layer
        public float speed = 1f;    // kecepatan layer
        private float spriteWidth;

        public void Init()
        {
            if (sprites.Length > 0 && sprites[0] != null)
            {
                SpriteRenderer sr = sprites[0].GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    // lebar sprite (sudah termasuk scale)
                    spriteWidth = sr.bounds.size.x * sprites[0].localScale.x;
                }
            }
        }

        public void UpdateLayer()
        {
            foreach (var sprite in sprites)
            {
                // geser ke kiri
                sprite.Translate(Vector3.left * speed * Time.deltaTime);

                // jika sprite sudah melewati kamera ke kiri, pindah ke kanan sprite paling kanan
                float leftBound = Camera.main.transform.position.x - (Camera.main.orthographicSize * Camera.main.aspect) - spriteWidth;
                if (sprite.position.x < leftBound)
                {
                    float rightMost = GetRightMostX();
                    sprite.position = new Vector3(rightMost + spriteWidth, sprite.position.y, sprite.position.z);
                }
            }
        }

        private float GetRightMostX()
        {
            float maxX = float.MinValue;
            foreach (var s in sprites)
            {
                if (s.position.x > maxX)
                    maxX = s.position.x;
            }
            return maxX;
        }
    }

    [Header("Parallax Layers")]
    public ParallaxLayer[] layers;

    void Start()
    {
        foreach (var layer in layers)
            layer.Init();
    }

    void Update()
    {
        foreach (var layer in layers)
            layer.UpdateLayer();
    }
}
