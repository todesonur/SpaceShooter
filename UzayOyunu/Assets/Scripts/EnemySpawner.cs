using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject EnemyGO; //Dusmanimizin prefabi 
    float maxSpawnRateInSeconds = 5f;

	// Use this for initialization
	void Start ()
    {
       
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void SpawnEnemy()
    {
        //Bu ekranin sol-alt (kose) noktasidir.
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //Bu ekranin sag-ust (kose) noktasidir.
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //Bir dusman somutlastirmak
        GameObject anEnemy = (GameObject)Instantiate(EnemyGO);
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        //Bir sonraki dusmanin ne zaman uretilecegini planla
        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInSeconds;
        if (maxSpawnRateInSeconds > 1f)
        {
            //1 ile maxSpawnRateInSeconds arasında bir sayi secmek
            spawnInSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
            spawnInSeconds = 1f;

        Invoke("SpawnEnemy", spawnInSeconds);
    }

    //Oyunun zorluklarini arttirmak icin fonksiyon
    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
            maxSpawnRateInSeconds--;

        if (maxSpawnRateInSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }

    public void ScheduleEnemySpawner()
    {
        maxSpawnRateInSeconds = 5f;

        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        //Her 30 saniyede spawn oranini arttir
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    public void UnscheduleEnemySpawner()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }
}
