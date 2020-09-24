using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField] private InputController inputController;
	[SerializeField] private Rigidbody playerRigidbody;
	[SerializeField] private Animator playerAnimator;

	[Range(1, 50)]
	[SerializeField] private float playerMoveSpeed = 5;

	private void OnEnable() {
		inputController.OnMoveStickEvent += MovePlayer;
		inputController.OnMoveStickEvent += UpdatePlayerView;
	}

	private void OnDisable() {
		inputController.OnMoveStickEvent -= MovePlayer;
		inputController.OnMoveStickEvent -= UpdatePlayerView;
	}
	Vector3 velocityVector;
	private void MovePlayer(Vector2 moveVector) {
		velocityVector.x = moveVector.x;
		velocityVector.z = moveVector.y;
		velocityVector.y = 0;
		playerRigidbody.velocity = velocityVector * playerMoveSpeed;
		if (moveVector.magnitude > 0.01f) { 
			playerAnimator.transform.forward = velocityVector;
		}
	}

	private void UpdatePlayerView(Vector2 direction) {
		playerAnimator.SetFloat("moveBlend", direction.magnitude);
	}
}
