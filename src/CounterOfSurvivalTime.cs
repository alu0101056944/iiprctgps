/// Marcos Barrios
/// Interfaces Inteligentes
/// Práctica GPS, Brújula y Acelerómetro
/// Count the amount of seconds alive and display it on screen as a label.

using UnityEngine;

public class CounterOfSurvivalTime : MonoBehaviour {

  private float amountOfTotalSecondsAlive = 0f;
  private string textOfLabel;

  void Start() {
    textOfLabel = "Tiempo total sobrevivido: ";
  }

  void Update() {
    amountOfTotalSecondsAlive += Time.deltaTime;
  }

  void OnGUI() {
    GUI.Label(new Rect(5, 0, 250, 50), textOfLabel + amountOfTotalSecondsAlive);
  }
}
