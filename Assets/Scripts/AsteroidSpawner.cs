using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
	public Asteroid asteroidPrefab;

	//public int spawnAmount = 7;
	public float spawnDistance = 15.0f;
	public float trajectoryVariance = 15.0f;
	//public int maxNumberOfAsteroids = 10;

	public void Spawn(int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
			Vector3 spawnPoint = this.transform.position = spawnDirection;

			float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
			Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

			Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);

			asteroid.size = Random.Range(asteroid.largeSize, asteroid.largeSize);
			asteroid.SetTrajectory(rotation * -spawnDirection);
		}
	}
}
