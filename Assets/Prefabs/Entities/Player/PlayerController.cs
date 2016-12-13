using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {

    private float speed = 15.0f;
    public float padding = 0.5f;
    public GameObject projectile;
    public float projectileSpeed;
    public float firingRate = 0.2f;
    public float playerHealth = 20050f;
    

    public AudioClip hitSound;     //drag and drop the clip into the inspector    
    public AudioClip fireSound;    //drag and drop the clip into the inspector

    public LevelManager levelManager;

    float xmin;
    float xmax;



    void Start() {

        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;

        
    }


    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, 1, 0);
        GameObject beam = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);

        AudioSource.PlayClipAtPoint(fireSound, transform.position);  //Play fire SFX


    }


    void Update () {
        
        //Projectile
        if (Input.GetKeyDown(KeyCode.Space)){
            InvokeRepeating("Fire", 0.000001f, firingRate);
        }

        if (Input.GetKeyUp(KeyCode.Space)){
            CancelInvoke("Fire");
        }//Projectile -end



        //Player movement
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        //restrict the player to the gamespace
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        
        //Player movement -end
    }


    void OnTriggerEnter2D(Collider2D collider) {
        Projectile missile = collider.gameObject.GetComponent<Projectile>();

        if (missile) {
            playerHealth -= missile.GetDamage();
            missile.Hit();

            AudioSource.PlayClipAtPoint(hitSound, transform.position);  //Play hit SFX

            if (playerHealth <= 0) {
                Die();
            }

        }


    }



    void Die() {

        Destroy(gameObject);
        
        LevelManager levMan = GameObject.Find("LevelManager").GetComponent<LevelManager>(); //Get the LevelManager.sc attached to the LevelManager game object 
        levMan.LoadLevel("Loose");

    }


}
 

