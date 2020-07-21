using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    float speed; //Merminin hizi
    Vector2 _direction; //Merminin yonu
    bool isReady; //Mermi yonunun ne zaman ayarlandigini bilmek

    //Awake fonksiyonunda varsayilan degerleri girmek
    void Awake()
    {
        speed = 8f;
        isReady = false;
    }

    // Use this for initialization
    void Start ()
    {
		
	}

    //Merminin yonunu ayarlama islevi
    public void SetDirection(Vector2 direction)
    {
        //Bir unit vektoru almak icin, normalize yonunu ayarla
        _direction = direction.normalized;
        isReady = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isReady)
        {
            //Merminin şu anki pozisyonunu al
            Vector2 position = transform.position;

            //Merminin yeni pozisyonunu hesapla
            position += _direction * speed * Time.deltaTime;

            //Merminin pozisyonunu guncelle
            transform.position = position;

            //Sonraki mermiyi oyunumuzdan cikarmamiz gerekiyor
            //Eger mermi ekranin disina cikarsa

            //Bu ekranin sol-alt (kose) noktasidir.
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

            //Bu ekranin sag-ust (kose) noktasidir.
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            //Eğer mermi ekranin disina giderse, yok et
            if((transform.position.x<min.x)||(transform.position.x>max.x)||
                (transform.position.y < min.y) || (transform.position.y > max.y))
            {
                Destroy(gameObject);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        //Dusman mermisinin oyuncu gemisiyle carpistigini tespit etmek
        if (col.tag == "PlayerShipTag")
        {
            //Dusman mermisini yok et 
            Destroy(gameObject);
        }
    }
}
