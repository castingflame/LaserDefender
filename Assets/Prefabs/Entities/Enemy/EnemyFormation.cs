using UnityEngine;
using System.Collections;

public class EnemyFormation : MonoBehaviour {
    

    public GameObject projectile;
    public float health = 150f;
    public float projectileSpeed = 10;
    public float shotsPerSecond = 0.5f;
    public int scoreValue = 150;
    public AudioClip fireSound;
    public AudioClip deathSound;



    private ScoreKeeper scoreKeeper;



    private void Start() {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();   //get this at runtime
        
    }


    void Update() {
       
        float probablity = Time.deltaTime * shotsPerSecond;

        if (Random.value < probablity) {
            Fire();
        }

    }

    void Fire() {

        GameObject missile = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;


        missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);

        AudioSource.PlayClipAtPoint(fireSound, transform.position);  //Play fire SFX

    }


    void OnTriggerEnter2D(Collider2D collider) {
        Projectile missile = collider.gameObject.GetComponent<Projectile>();

        if (missile) {    //Enemy collided with a Projectile?
            health -= missile.GetDamage();
            missile.Hit();

            if (health <= 0) {    //Enemy ready to die?
                Die();
            }
         } // if (missile) -end
      } //OnTriggerEnter2D  -end


    void Die() {

        Destroy(gameObject);            //Destroy our enemy game object
        scoreKeeper.Score(scoreValue);  //Hit enemy. Pass 'scoreValue' to the ScoreKeeper
        AudioSource.PlayClipAtPoint(deathSound, transform.position);  //Play death SFX

    } //void Die -end




} //THE END



