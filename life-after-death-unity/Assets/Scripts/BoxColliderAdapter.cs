using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderAdapter : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    BoxCollider2D boxCollider;

    public void AdaptColldier()
    {
        Vector2 size = spriteRenderer.size;
        boxCollider.size = size;
    }
}
