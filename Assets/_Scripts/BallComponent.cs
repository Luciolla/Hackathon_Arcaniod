using System;
using _Scripts.Field;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Scripts
{
	public class BallComponent : MonoBehaviour
	{
		[SerializeField] private AudioService _service;
		[SerializeField] private AudioClip _contactClip;
		[SerializeField] private AudioClip _deathClip;
		
		[Tooltip("Начальная скорость движения шара"), SerializeField, Range(1, 10)]
		private float _startSpeed = 1f;
		[Tooltip("Максимальная скорость движения шара"), SerializeField, Range(0, 10)]
		private float _speedMaxLimit = 10f;
		[Tooltip("Коэффициент прирощения скорости"), SerializeField, Range(0, 3)]
		private float _speedAccelerator = 0.25f;

		private Vector2 _startPosition;
		private Quaternion _startRotation;

		private float _moveSpeed;
		private float _timeDelay = 1f;

		private bool _isBallInGame = true;
		private bool _multiDeathLock;

		public bool IsBallInGame {get => _isBallInGame; set => _isBallInGame = value;}

		private void Awake()
		{
			_multiDeathLock = true;
			_moveSpeed = _startSpeed;
			_startPosition = transform.position;
			_startRotation = transform.rotation;
		}

		private void FixedUpdate()
		{
			if(_isBallInGame)
				StartMotion();
		}

		public void BallUnlock() =>
			_multiDeathLock = !_multiDeathLock;

		private void ResetSpeed() =>
			_moveSpeed = _startSpeed;

		private void ResetPosition() =>
			transform.position = _startPosition;

		private void ResetRotation() =>
			transform.rotation = _startRotation;

		private void StartMotion() =>
			transform.position += (_moveSpeed * Time.deltaTime) * transform.up;

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if(collision.gameObject.layer == 8) return;
			_service.PlaySound(_contactClip);
			
			var contact = collision.GetContact(0);

			if(collision.gameObject.layer == 7)
				PlayerPlatformRotate(contact, collision);
			else
				NormalRotate(contact);

			if(collision.gameObject.GetComponent<IDamageable>() == null)
				return;

			IncreaseSpeed();
		}

		private void NormalRotate(ContactPoint2D contact)
		{
			var startVector = transform.up;

			var endVector = Vector2.Reflect(startVector, contact.normal);

			var angle = Mathf.Atan2(endVector.x, endVector.y) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0, 0, -angle);
		}

		private void PlayerPlatformRotate(ContactPoint2D contact, Collision2D collision)
		{
			var platformCenter = collision.transform.position;

			var offset = contact.point.x - platformCenter.x;

			var normalizedOffset = offset / (collision.collider.bounds.size.x / 2);

			var bounceAngle = normalizedOffset * 45f;
			transform.rotation = Quaternion.Euler(0, 0, -bounceAngle);
		}

		private async void OnTriggerStay2D(Collider2D other)
		{
			if(other.gameObject.layer == 8) return;
			
			BallRestart();
			
			if(_multiDeathLock) return;
			_service.PlaySound(_deathClip);
			_multiDeathLock = true;
			EventBus.OnHealthLose();
			await UniTask.Delay(TimeSpan.FromSeconds(1f));
			_multiDeathLock = false;
		}

		public void BallRestart()
		{
			ResetSpeed();
			ResetPosition();
			ResetRotation();
		}

		private void IncreaseSpeed()
		{
			if(_moveSpeed < _speedMaxLimit)
				_moveSpeed += _speedAccelerator;

			Debug.Log(_moveSpeed);
		}
	}
}