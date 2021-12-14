/// Marcos Barrios
/// Interfaces Inteligentes
/// Práctica GPS, Brújula y Acelerómetro
/// When the GameObject holding this script touches the player's collider
/// Game Over is triggered.

using UnityEngine;

public class GameOverOnPlayerTouch : MonoBehaviour {

  private GameOver gameOverScript;

  void Start() {
    var globalScriptsGameObject = GameObject.FindWithTag("GlobalScripts");
    gameOverScript = globalScriptsGameObject.GetComponent<GameOver>();
  }

  void OnCollisionEnter(Collision collider) {
    if (collider.gameObject.tag == "Player") {
      gameOverScript.gameover();
    }
  }
}
