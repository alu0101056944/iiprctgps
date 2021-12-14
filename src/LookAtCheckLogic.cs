/// Marcos Barrios
/// Interfaces Inteligentes
/// Práctica GPS, Brújula y Acelerómetro
/// GameObject that needs to be looked at by the player periodically to avoid
/// GameOver. Changes the color of the 3D Model assigned as component of the
/// GameObject with this script when it is activated (required to look at it,
/// that is)

using UnityEngine;

public class LookAtCheckLogic : MonoBehaviour {

  [SerializeField]
  private Color colorWhenActive;

  private Color colorWhenInactive;
  
  [SerializeField]
  private float secondsBetweenActivation = 10f;
  
  [SerializeField]
  private float secondsUntilGameOver = 15f;

  // To keep track of time until gameover when activated
  private float secondsSinceLastStateChange = 0f;

  // Current state
  private bool isToBeLookedAt = false;

  // To avoid getting subscribed more than once per activation
  private bool isSubscribedToPlayerRaycast = false;

  private Renderer rendererOfMesh;
  private GameOver gameOverScript;

  void Start() {
    rendererOfMesh = GetComponent<Renderer>();    
    colorWhenInactive = rendererOfMesh.material.color;
    gameOverScript = GameObject.FindWithTag("GlobalScripts")
        .GetComponent<GameOver>();
  }

  void Update() {
    secondsSinceLastStateChange += Time.deltaTime;
    if (isToBeLookedAt) {
      subscribeOnceToPlayerRaycast();
      if (secondsSinceLastStateChange >= secondsUntilGameOver) {
        isToBeLookedAt = false;
        secondsSinceLastStateChange = 0f;
        gameOverScript.gameover();
      }
    } else {
      if (secondsSinceLastStateChange >= secondsBetweenActivation) {
        isToBeLookedAt = true;
        secondsSinceLastStateChange = 0f;
      }
    }
  }

  // To avoid subscribing more than once. Separated function to improve
  // readability.
  private void subscribeOnceToPlayerRaycast() {
    if (!isSubscribedToPlayerRaycast) { 
      PlayerRaycast.RaycastCollisionEvent += reset;
      isSubscribedToPlayerRaycast = true;
      rendererOfMesh.material.color = colorWhenActive;
    }
  }

  // To be called when player's raycast collides with this script GameObject's
  // collider.
  private void reset(Collider raycastCollider_) {
    if (raycastCollider_.gameObject.name == gameObject.name) {
      isSubscribedToPlayerRaycast = false;
      isToBeLookedAt = false;
      secondsSinceLastStateChange = 0f;
      rendererOfMesh.material.color = colorWhenInactive;
    }
  }

  // To prevent bad deallocation of event subscription
  void OnDestroy() {
    PlayerRaycast.RaycastCollisionEvent -= reset;    
  }

}
