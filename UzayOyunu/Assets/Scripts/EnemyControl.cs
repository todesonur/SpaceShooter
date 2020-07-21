using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    GameObject scoreUITextGO;
    public GameObject ExplosionGO; //Patlama prefabi
    float speed; // Dusman hizi

	// Use this for initialization
	void Start ()
    {
        speed = 2f;
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Dusmanin suanki pozisyonunu al
        Vector2 position = transform.position;

        //Dusmanin yeni pozisyonunu hesapla 
        position = new Vector2 (position.x, position.y - speed * Time.deltaTime);

        //Dusmanin pozisyonunu guncelle
        transform.position = position;
        //Bu ekranin sol-alt (kose) noktasidir.
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //Dusman ekranin alt tarafından disari ciktiginda, dusmani yok et
        if(transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        //Dusman gemisinin Player gemisiyle ya da Player'in mermisiyle carpistigini tespit etmek
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
            PlayExplosion();
            scoreUITextGO.GetComponent<GameScore>().Score += 100;
            //Dusman gemisini yok et
            Destroy(gameObject);
        }
    }

    //Patlamayi ortaya cikarmak icin 
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        //Patlamanin pozisyonunu ayarla
        explosion.transform.position = transform.position;
    }
}
