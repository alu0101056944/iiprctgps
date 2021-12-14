/// Marcos Barrios
/// Interfaces Inteligentes
/// Práctica GPS, Brújula y Acelerómetro
/// Destroy bowling balls that touch the trigger this gameobject has

using UnityEngine;

public class GameObjectDestroyOnTouch : MonoBehaviour {

  void OnTriggerEnter(Collider collider) {
    if (collider.gameObject.tag == "BowlingBall") {
      Destroy(collider.gameObject);
    }
  }

}
