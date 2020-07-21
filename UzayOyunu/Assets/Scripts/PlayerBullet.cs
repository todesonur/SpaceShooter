using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    float hiz;

	// Use this for initialization
	void Start ()
    {
        hiz = 8f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Merminin suanki pozisyonunu al
        Vector2 position = transform.position;

        //Merminin yeni pozisyonunu hesapla
        position = new Vector2(position.x, position.y + hiz * Time.deltaTime);

        //Merminin pozisyonunu guncelle
        transform.position = position;

        //Bu ekranin sag-ust (kose) noktasidir.
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //Mermi ekranın ust tarafından disari ciktiginda mermiyi yok et
        if(transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        //Oyuncu mermisinin bir dusman gemisiyle carpistigini tespit etmek
        if (col.tag == "EnemyShipTag")
        {
            //Oyuncu gemisinin mermisini yok et 
            Destroy(gameObject);
        }
    }
}
