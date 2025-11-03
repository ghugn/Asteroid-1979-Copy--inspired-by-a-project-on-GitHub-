using UnityEngine;

public class Missile : MonoBehaviour
{
	public float maxLifetime;
    public float speed;

	private Rigidbody2D _rigidbody;
	private SpriteRenderer _spriteRenderer;
	private Camera _camera;

	private void Awake()
	{
        _rigidbody = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_camera = FindObjectOfType<Camera>();
	}

	private void Update()
	{
		KeepWithinScreenBounds();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(this.gameObject);
	}

	private void KeepWithinScreenBounds()
	{
		float height = _camera.orthographicSize * 2;
		float width = height * _camera.aspect;

		float playerWidth = _spriteRenderer.bounds.size.x / 2;
		float playerHeight = _spriteRenderer.bounds.size.y / 2;

		/* X bounds */
		if (transform.position.x > (width / 2) + playerWidth)
		{
			transform.position = new Vector2(0 - (width / 2) - playerWidth, transform.position.y);
		}
		else if (transform.position.x < (-width / 2) - playerWidth)
		{
			transform.position = new Vector2(0 + (width / 2) + playerWidth, transform.position.y);
		}

		/* Y bounds */
		if (transform.position.y > (height / 2) + playerHeight)
		{
			transform.position = new Vector2(transform.position.x, 0 - (height / 2) - playerHeight);
		}
		else if (transform.position.y < (-height / 2) - playerHeight)
		{
			transform.position = new Vector2(transform.position.x, 0 + (height / 2) + playerHeight);
		}
	}

	public void Fire(Vector2 direction, Vector2 shipVelocity)
	{
		_rigidbody.velocity = direction * speed;
		_rigidbody.AddForce(shipVelocity, ForceMode2D.Impulse);
		Destroy(this.gameObject, maxLifetime);
	}
}
