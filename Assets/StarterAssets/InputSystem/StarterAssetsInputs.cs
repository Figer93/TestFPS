using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 _move;
		public Vector2 _look;
		public bool _jump;
		public bool _sprint;
		public bool _aim;
		public bool _shoot;
		public bool _reloadRiffle;

		[Header("Movement Settings")]
		public bool _analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool _cursorLocked = true;
		public bool _cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(_cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnAim(InputValue value)
		{
			AimInput(value.isPressed);
		}
		
		public void OnShoot(InputValue value)
		{
			ShootInput(value.isPressed);
		}
		public void OnReloadRiffle(InputValue value)
		{
			ReloadRiffleInput(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			_move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			_look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			_jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			_sprint = newSprintState;
		}
		
		public void AimInput(bool newAimState)
		{
			_aim = newAimState;
		}
		
		public void ShootInput(bool newShootState)
		{
			_shoot = newShootState;
		}
		
		public void ReloadRiffleInput(bool newReloadRiffleState)
		{
			_reloadRiffle = newReloadRiffleState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(_cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}