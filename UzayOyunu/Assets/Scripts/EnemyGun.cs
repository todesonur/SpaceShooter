using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour {

    public GameObject EnemyBulletGO; //Dusman mermimizin prefabi
    public float fire2 = 0f;

    // Use this for initialization
    void Start ()
    {
        //1 saniye sonra bir dusman mermisi ates
        Invoke("FireEnemyBullet", 1f);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Dusman mermi ates islevi
    void FireEnemyBullet()
    {
        //PlayerShipi referans olarak al
        GameObject playerShip = GameObject.Find("PlayerGO");

        if (playerShip != null && Time.time > fire2) //Eger Player olmezse 
        {
                fire2 = Time.time + 0.1f;
                //Dusman mermisini baslatmak
                GameObject bullet = (GameObject)Instantiate(EnemyBulletGO);

                //Merminin baslangic ​​pozisyonunu ayarla   
                bullet.transform.position = transform.position;

                //Merminin yonunu oyuncunun gemisine dogru hesaplamak
                Vector2 direction = playerShip.transform.position - bullet.transform.position;

                //Merminin pozisyonunu ayarla
                bullet.GetComponent<EnemyBullet>().SetDirection(direction);
           
        }
    }
}
