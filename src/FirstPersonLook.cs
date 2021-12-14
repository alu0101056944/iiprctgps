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

  private float deltaRotation = 0f;

  void Reset() {
    // Get the character from the FirstPersonMovement in parents.
    character = GetComponentInParent<FirstPersonMovement>().transform;
  }

  void Start() {
    // Lock the mouse cursor to the game screen.
    Input.compass.enabled = true;
    Cursor.lockState = CursorLockMode.Locked;
  }

  void Update() {
    deltaRotation =
        (Input.compass.trueHeading / 90) * (50 /* otherwise too slow */) - 
            180 /* degres correction for the current scene */;
    // Rotate camera using the phone's compass
    character.localRotation =
        Quaternion.Euler(0, character.localRotation.y + deltaRotation, 0);
  }

  void OnGUI() {
    int halfScreenWidth = Screen.width / 2;
    int halfScreenHeight = Screen.height / 2;

    var compassRect = 
        new Rect(halfScreenWidth - 100, halfScreenHeight - 40, 150, 50);
    var text = "deltaRotation: " + deltaRotation;
    GUI.Label(compassRect, text);
  }
}
