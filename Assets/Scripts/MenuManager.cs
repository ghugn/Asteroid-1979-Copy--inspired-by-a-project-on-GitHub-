using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private AsteroidSpawner _asteroidSpawner;

	[SerializeField] private int _asteroidSpawnAmount;

	// Start is called before the first frame update
	void Start()
    {
        _asteroidSpawner.Spawn(_asteroidSpawnAmount);
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.Space))
        {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}

		if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
