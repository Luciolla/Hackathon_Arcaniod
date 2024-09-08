using System;
using UnityEngine;

namespace _Scripts
{
	public class PlayerMotion : MonoBehaviour
	{
		[SerializeField] private int _speedMultiplier = 1;
		[SerializeField] private float _moveLimit = 10;
		[SerializeField] private float _mouseSpeedDivider = 90;

		private InputSystem_Actions _controls;
		private Vector2 _moveInput;

		private float _halfPaddleWidth;
		private Vector3 _scale;

		private void OnEnable() =>
			_controls.Player.Enable();

		private void Awake()
		{
			_controls = new InputSystem_Actions();

			_controls.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
			_controls.Player.Move.canceled += ctx => _moveInput = Vector2.zero;

			_halfPaddleWidth = GetComponent<Renderer>().bounds.extents.x;

			_scale = this.transform.localScale;
			EventBus.HealthLose += ResetScale;
		}

		private void FixedUpdate()
		{
			MouseMove();
			KeyboardMove();
		}

		private void MouseMove()
		{
			var moveAmount = Input.GetAxis("Mouse X");
			var position = transform.position;
			position.x += moveAmount * _speedMultiplier/_mouseSpeedDivider;
			position.x = Mathf.Clamp(position.x, -_moveLimit + _halfPaddleWidth, _moveLimit - _halfPaddleWidth);
			transform.position = position;
		}

		private void KeyboardMove()
		{
			var moveAmount = _moveInput.x * (_speedMultiplier * Time.deltaTime);

			transform.Translate(0, -moveAmount, 0);

			var position = transform.position;
			position.x = Mathf.Clamp(position.x, -_moveLimit + _halfPaddleWidth, _moveLimit - _halfPaddleWidth);
			transform.position = position;
			
		}

		private void ResetScale() =>
			transform.localScale = _scale;

		private void OnDisable() =>
			_controls.Player.Disable();
	}
}