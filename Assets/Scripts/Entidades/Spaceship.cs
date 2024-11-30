using UnityEngine;

public class Spaceship : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Bounds _cameraBounds;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        var height = Camera.main.orthographicSize * 2f;
        var width = height * Camera.main.aspect;
        _cameraBounds = new Bounds(Vector3.zero, new Vector3(width, height));
    }

    void LateUpdate()
    {
        var spriteWidth = _renderer.sprite.bounds.extents.x;
        var spriteHeight = _renderer.sprite.bounds.extents.y;

        var newPositon = transform.position;
        newPositon.x = Mathf.Clamp(newPositon.x, _cameraBounds.min.x + spriteWidth, _cameraBounds.max.x - spriteWidth);
        newPositon.y = Mathf.Clamp(newPositon.y, _cameraBounds.min.y + spriteHeight, _cameraBounds.max.y - spriteHeight);
        transform.position = newPositon;
    }
}
