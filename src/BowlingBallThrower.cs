/// Marcos Barrios
/// Interfaces Inteligentes
/// Práctica GPS, Brújula y Acelerómetro
/// Attempt to throw a bowling ball forward by chance.

using UnityEngine;

public class BowlingBallThrower : MonoBehaviour {

  [SerializeField] // allow changes from editor
  private float chanceOfSpawnEachSecond = 0.3f;

  [SerializeField]
  private float secondsBetweenSpawns = 3f;
  private float secondsSinceLastSpawn = 0f;

  [SerializeField]
  private float forceToApplyOnBallSpawn = 200f;

  [SerializeField]
  private GameObject ballToInstanceSpawn;

  /// Because an empty child object's position is used for the throw direction
  [SerializeField]
  private GameObject throwDirectionGameObject;
  private Vector3 throwDirection;

  void Start() {
    throwDirection =
        (throwDirectionGameObject.transform.position - transform.position)
        .normalized;
  }

  void Update() {
    secondsSinceLastSpawn += Time.deltaTime;
    bool enoughTimePassed = secondsSinceLastSpawn >= secondsBetweenSpawns;
    if (enoughTimePassed && Random.Range(0, 1) <= chanceOfSpawnEachSecond) {
      secondsSinceLastSpawn = 0;
      var spawnedBall = Instantiate(ballToInstanceSpawn, transform.position, Quaternion.identity);
      var rigidBodyOfBall = spawnedBall.GetComponent<Rigidbody>();
      rigidBodyOfBall.AddForce(throwDirection * forceToApplyOnBallSpawn);
    }
  }
}
