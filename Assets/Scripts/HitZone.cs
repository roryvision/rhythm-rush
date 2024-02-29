using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HitZone : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color initialColor;
    public Color pressedColor = new Color(0.9411765f, 0.4235294f, 0f, 1f);
    public float fadeDuration = 0.3f;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        initialColor = spriteRenderer.color;
    }

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            if (gameObject.GetComponent<Collider2D>().bounds.Contains(touchPosition))
            {
                Sequence sequence = DOTween.Sequence();
                sequence.Append(spriteRenderer.DOColor(pressedColor, fadeDuration));
                sequence.Append(spriteRenderer.DOColor(initialColor, fadeDuration));
            }
        }
    }
}
