/// Marcos Barrios
/// Interfaces Inteligentes
/// Práctica GPS, Brújula y Acelerómetro
/// Freeze game time and unfreeze using it's methods.

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

  private bool isGameOver = false;

  void OnGUI() {
    if (isGameOver) {
      int halfScreenWidth = Screen.width / 2;
      int halfScreenHeight = Screen.height / 2;

      var gameOverRect = 
          new Rect(halfScreenWidth - 50, halfScreenHeight, 150, 50);
      GUI.Label(gameOverRect, "GameOver!");

      var tryAgainRect = 
          new Rect(halfScreenWidth - 100, halfScreenHeight + 20, 450, 50);
      GUI.Label(tryAgainRect, "Tocar la pantalla para reintentarlo.");
    }
  }

  void Update() {
    if (isGameOver && Input.GetMouseButtonDown(0)) {
      restart();
    }
  }

  /// So that other scripts can restart the game by calling this method.
  public void restart() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    Time.timeScale = 1f;
    isGameOver = false;
  }

  /// So that other scripts can gameover the game by calling this method.
  public void gameover() {
    Time.timeScale = 0f;
    isGameOver = true;
  }
}
