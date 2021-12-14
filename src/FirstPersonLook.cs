/// Vitefait modified by Marcos Barrios
/// https://assetstore.unity.com/packages/tools/input-management/mini-first-person-controller-174710#content
/// Move the camera using the phone's compass.

using UnityEngine;

public class FirstPersonLook : MonoBehaviour {

  [SerializeField]
  private Transform character;
  public float sensitivity = 2;
  public float smoothing = 1.5f;

  private Vector2 velocity;
  private Vector2 frameVelocity;

  public float angle = 0f;

  void Reset() {
    // Get the character from the FirstPersonMovement in parents.
    character = GetComponentInParent<FirstPersonMovement>().transform;
  }

  void Start() {
    // Lock the mouse cursor to the game screen.
    Cursor.lockState = CursorLockMode.Locked;
    velocity.x -= 90; /// To start facing the right direction.
  }

  void Update() {
    // Rotate camera using the phone's compass
    character.localRotation = Quaternion.Euler(0, Input.compass.trueHeading * 3 - 90, 0);
  }
}
