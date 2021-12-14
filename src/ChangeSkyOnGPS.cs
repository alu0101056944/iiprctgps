/// Marcos Barrios
/// Interfaces Inteligentes
/// Práctica GPS, Brújula y Acelerómetro
/// Change Sky horizon line's color depending on the longitude, the sun halo
/// intensity depending on latitude and finally the Sky Gradient's exponent
/// depending on the altitude to emulate the intensity of the space relative
/// to the earth; the more far away from earth the darker is the atmosphere.

using UnityEngine;

public class ChangeSkyOnGPS : MonoBehaviour {
  
  void Start() {
    InitiateLocationServices.LocationServicesRunningEvent += changeSky;
  }

  void OnDestroy() {
    InitiateLocationServices.LocationServicesRunningEvent -= changeSky;
  }

  void changeSky() {
    var latitude = Input.location.lastData.latitude;
    var longitude = Input.location.lastData.longitude;
    var altitude = Input.location.lastData.altitude;

    // To obtain a positive value closer to the range [0, 1]
    var latitudeNormalized = Mathf.Abs(latitude / 90f);
    var longitudeNormalized = Mathf.Abs(longitude / 90f);
    var altitudeNormalized = Mathf.Abs(altitude / 90f);
    Debug.Log(latitudeNormalized);
    Debug.Log(longitudeNormalized);
    Debug.Log(altitudeNormalized);

    // Because I assume if different than 0 then it is working. Don't change
    // default Skybox otherwise.
    if (latitudeNormalized != 0 && longitudeNormalized != 0 &&
        altitudeNormalized != 0) {
      Material skyboxMaterial = RenderSettings.skybox;
      skyboxMaterial.SetFloat("_SkyGradientExponent", altitudeNormalized);

      // Bluer the further away from equator (0 degrees) because it's colder.
      Color horizonColor =
          new Color(255 - (latitudeNormalized * 255), 255 * latitudeNormalized, 0);
      skyboxMaterial.SetColor("_HorizonLineColor", horizonColor);

      // For longitude just get an arbitrary sky color for uniqueness of the scene depending
      // on where you execute it at. Preferably Orangeish's colors.
      Color skyGradientTop =
          new Color(longitudeNormalized * 255, longitudeNormalized * 90, 0);
      skyboxMaterial.SetColor("_SkyGradientTop", skyGradientTop);
      Color skyGradientBottom =
          new Color(longitudeNormalized * 200, longitudeNormalized * 150, 0);
      skyboxMaterial.SetColor("_SkyGradientBottom", skyGradientBottom);
    }
  }
}
