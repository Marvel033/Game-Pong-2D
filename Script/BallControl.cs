using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); //mengambil rigidbody component dari sebuah bole
        rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        Invoke("GoBall", 2); //memanggil function GoBall dlm 2 detik
    }
    void GoBall()
    {
        float rand = Random.Range(0, 2); //akan random nilai diantara 0-1
        if (rand < 1)
        {
            rb2d.AddForce(new Vector2(20, -15)); //add force memberikan tenaga
        }
        else
        {
            rb2d.AddForce(new Vector2(-20, -15));
        }
    }

    void ResetBall() //ini kita buat nilai transform jadi 0
    {
        rb2d.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    void RestartGame()
    {
        //Debug.Log("Restart!");
        ResetBall();
        Invoke("GoBall", 1);
    }

    [SerializeField] private int wallCollisionCount;
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            if (coll.gameObject.name == "Racket Left") //jika terkena player
            {
                Debug.Log("Racket Left Punch!");
                rb2d.AddForce(new Vector2(20, -15));
                wallCollisionCount = 0;

            }
            else if (coll.gameObject.name == "Racket Right") //jika terkena enemy
            {
                Debug.Log("Racket Right Punch!");
                rb2d.AddForce(new Vector2(-20, -15));
                wallCollisionCount = 0;
            }
        }
        else //jika terkena wall
        {
            wallCollisionCount = wallCollisionCount + 1;
            Debug.Log("Wall Collision! = " + wallCollisionCount);
            if (wallCollisionCount > 6) GoBall();
        }
    }

}