/// Marcos Barrios
/// Interfaces Inteligentes
/// Práctica GPS, Brújula y Acelerómetro
/// Wait until location services has been successfuly ran, keep the game on
/// pause (no Time is passing) otherwise.

using UnityEngine;

public class InitiateLocationServices : MonoBehaviour {
  
  private GameOver gameOverScript;

  private bool isReadyToLocate = false;
  private bool isAFailure = false;

  void Start() {
    Time.timeScale = 0f;
    if (Input.location.isEnabledByUser) {
      Input.location.Start();
    }
  }

  void Update() {
    if (Input.location.status == LocationServiceStatus.Running) {
      Time.timeScale = 1f;
      isReadyToLocate = true;
    } else if (Input.location.status == LocationServiceStatus.Failed) {
      isAFailure = true; 
      isReadyToLocate = false;     
    }
  }

  void OnGui() {
    if (!isReadyToLocate) {
      int halfScreenWidth = Screen.width / 2;
      int halfScreenHeight = Screen.height / 2;

      var waitingForServiceRect = 
          new Rect(halfScreenWidth - 100, halfScreenHeight, 150, 50);
      var text = "Waiting for location services to start.";
      GUI.Label(waitingForServiceRect, text);
    }

    if (isAFailure) {
      int halfScreenWidth = Screen.width / 2;
      int halfScreenHeight = Screen.height / 2;

      var failureRect = 
          new Rect(halfScreenWidth - 100, halfScreenHeight, 150, 50);
      var text = "Couldn`t start location services. Failure.";
      GUI.Label(failureRect, text);
    }
  }

  void OnDestroy() {
    if (Input.location.status == LocationServiceStatus.Running) {
      Input.location.Stop();
    }
  }
}
