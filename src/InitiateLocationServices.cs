/// Marcos Barrios
/// Interfaces Inteligentes
/// Práctica GPS, Brújula y Acelerómetro
/// Wait until location services has been successfuly ran, keep the game on
/// pause (no Time is passing) otherwise. Call an event once it is running.

using UnityEngine.Android;
using UnityEngine;

public class InitiateLocationServices : MonoBehaviour {
  
  public delegate void LocationServicesRunningDelegate();
  public static event LocationServicesRunningDelegate LocationServicesRunningEvent;

  private GameOver gameOverScript;

  private bool isReadyToLocate = false;
  private bool isAFailure = false;
  private bool locationServicesFirstRunning = false;

  /// Awake so that it is called before any "Start()" which might need location
  /// services.
  void Start() {
    locationServicesFirstRunning = false;
    Time.timeScale = 1f;
    obtainUserPermissions();
    Input.location.Start(1, 1);
    if (Input.location.isEnabledByUser) {
      Debug.Log("Location services Start() called.");
    }
  }

  private void obtainUserPermissions() {
    if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation)) {
      Permission.RequestUserPermission(Permission.FineLocation);
      Debug.Log("Fine Location permission granted by the user.");
    } else {
      Debug.Log("Fine Location permission has already been granted by the user.");
    }
    if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation)) {
      Permission.RequestUserPermission(Permission.CoarseLocation);
      Debug.Log("Coarse Location permission granted by the user.");
    } else {
      Debug.Log("Coarse Location permission has already been granted by the user.");
    }
  }

  void Update() {
    Debug.Log(Input.location.status);
    if (Input.location.status == LocationServiceStatus.Running && !locationServicesFirstRunning) {
      isReadyToLocate = true;
      locationServicesFirstRunning = true; /// to call the event only once activated
      if (LocationServicesRunningEvent != null) {
        LocationServicesRunningEvent();
      }
    } else if (Input.location.status == LocationServiceStatus.Failed) {
      isAFailure = true; 
    }
  }

  void OnGUI() {
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
