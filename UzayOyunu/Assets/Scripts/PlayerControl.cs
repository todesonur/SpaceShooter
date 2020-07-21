using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerControl : MonoBehaviour
{
    public GameObject GameManagerGO;

    public GameObject PlayerBulletGO; //Oyuncumuzun mermi prefabi
    public GameObject BulletPosition01;
    public float speed;
    public float fire = 0f;
    public GameObject ExplosionGO; //Patlama prefabi

    public Text LivesUI;
    const int MaxLives = 3;
    int lives;

    public void Init()
    {
        lives = MaxLives;
        LivesUI.text = lives.ToString();
        transform.position = new Vector2(0, 0);
        gameObject.SetActive(true);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Bosluk tusuna basildiginda oyuncu ates eder
        if(Time.time>fire)
        {
            GetComponent<AudioSource>().Play();
            fire = Time.time + 0.3f;
            //Mermiyi somutlastirmak
            GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
            bullet01.transform.position = BulletPosition01.transform.position; //Mermi baslangic pozisyonunu ayarla
        }

        float x = Input.acceleration.x;
        float y = Input.acceleration.y;

        Vector2 direction = new Vector2(x, y);

        if (direction.sqrMagnitude > 1)
            direction.Normalize();

        //Simdi oyuncunun pozisyonunu hesaplayan ve ayarlayan fonksiyonu cagiriyoruz
        Move(direction);
    }

    void Move(Vector2 direction)
    {
        //Oyuncunun hareketi icin ekran sinirlarini bul (ekranin sol, sag, ust ve alt kenarlari)
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); //Bu ekranin sol-alt (kose) noktasidir.
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //Bu ekranin sag-ust (kose) noktasidir.

        max.x = max.x - 0.255f; //Oyuncunun yari genisligini cikariyoruz
        min.x = min.x + 0.255f; //Oyuncunun yari genisligini ekliyoruz

        max.y = max.y - 0.285f; //Oyuncunun yari yuksekligini cikariyoruz
        min.y = min.y + 0.285f; //Oyuncunun yari yuksekligini ekliyoruz

        //Oyuncunun suanki pozisyonunu almak
        Vector2 pos = transform.position;

        //Yeni pozisyonu hesapla
        pos += direction * speed * Time.deltaTime;

        //Yeni konumun ekranin disinda olmadigindan emin olun
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        //Oyuncunun pozisyonunu guncelle
        transform.position = pos;

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Oyuncu gemisinin bir dusman gemisiyle ya da dusman mermisiyle carpistigini tespit etmek
        if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag"))
        {
            PlayExplosion();
            lives--;
            LivesUI.text = lives.ToString();
            if (lives == 0)
            {
                GameManagerGO.GetComponent<GameManagerr>().SetGameManagerState(GameManagerr.GameManagerState.Gameover);
                //Destroy(gameObject); //Player Ship'i yok et 
                gameObject.SetActive(false);
            }
        }
    }

    //Bir patlamayi ortaya cikarmak icin 
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        //Patlamanin pozisyonunu ayarla
        explosion.transform.position = transform.position;
    }
}