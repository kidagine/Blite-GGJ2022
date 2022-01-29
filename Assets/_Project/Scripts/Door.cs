using Cinemachine;
using Demonics.Utility;
using UnityEngine;

public class Door : MonoBehaviour
{
	[SerializeField] Transform _enterPoint = default;
	[SerializeField] private bool _isHorizontal = default;
	[SerializeField] private CinemachineVirtualCamera _horizontalCamera = default;
	[SerializeField] private CinemachineVirtualCamera _verticalCamera = default;
	[SerializeField] private bool _isLocked = false;
	[SerializeField] private Sprite _lockedDoorSprite = default;
	[SerializeField] private Sprite _openDoorSprite = default;
	private SpriteRenderer _spriteRenderer;
	private BoxCollider2D _boxCollider;


	private void Start()
	{
		_boxCollider = GetComponent<BoxCollider2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		if (_isLocked)
		{
			_boxCollider.enabled = false;
			_spriteRenderer.sprite = _lockedDoorSprite;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out Player player))
		{
			BliteManager.Instance.SwapToWhiteWorld();
			if (_isHorizontal)
			{
				_verticalCamera.gameObject.SetActive(false);
				_horizontalCamera.gameObject.SetActive(true);
			}
			else
			{
				_horizontalCamera.gameObject.SetActive(false);
				_verticalCamera.gameObject.SetActive(true);
			}
			player.transform.position = _enterPoint.position;
		}
	}

	public async void Unlock()
	{
		await UpdateTimer.WaitFor(0.2f);
		_boxCollider.enabled = true;
		_spriteRenderer.sprite = _openDoorSprite;
	}
}
