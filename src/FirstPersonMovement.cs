/// Vitefait modified by Marcos Barrios
/// https://assetstore.unity.com/packages/tools/input-management/mini-first-person-controller-174710#content
/// Move the player using the phone's accelerometer. A world player movement
/// restrictor is used to restrict the movement to a parallell to the ball
/// throwers.

using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour {

  [SerializeField]
  private float runSpeed = 30;

  private Rigidbody rigidbody_;
  private Transform playerRails; // World player movement restrictor

  void Start() {
    rigidbody_ = GetComponent<Rigidbody>();
    playerRails = GameObject.FindWithTag("PlayerMovementRails").transform;
  }

  void FixedUpdate() {
    float horizontalMovement = Input.acceleration.x * runSpeed;
    // Only parallel to the ball throwers. On the current scene it means it only
    // moves on the z axis.
    Vector3 targetVelocity =
          new Vector3(0, 0, horizontalMovement);
    rigidbody_.velocity = targetVelocity;
  }
}