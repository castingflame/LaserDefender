using UnityEngine;
using System.Collections;

public class FormationController : MonoBehaviour
{
    //Declarations
    public GameObject enemyPrefab;          //Drop the Enemy Prefab here in the Unity Inspector   
    public float formWidth;                 //Enemy formation width - NOTE! set in inspector
    public float formHeight;                //Enemy formation height - NOTE! set in inspector
    public bool movingRight = true;         //Enemy movement direction flag
    public float speed = 5f;                //Enemy movement speed
    public float spawnDelay = 0.5f;         //Enemy spawn creation delay

    private float xmax;                     //Left boundry of enemy formation
    private float xmin;                     //Right boundry of enemy formation


    void Start() {

        //Edge of screen stuff
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundry = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightBoundry = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xmax = rightBoundry.x;
        xmin = leftBoundry.x;
        //Edge of screen stuff - end

        SpawnUntilFull();  //Populate with enemies

    }

    //Draw a gizmo on the screen so that the enemy positions can me dragged around at design time
    public void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position, new Vector3(formWidth, formHeight));
    }//Draw a gizmo -end

    //void Start() -end


    void Update(){

        //Enemy movement section
        if (movingRight) {
            transform.position += new Vector3(speed * Time.deltaTime, 0);
        } else {
            transform.position += new Vector3(-speed * Time.deltaTime, 0);
        }

        float rightEdgeOfFormation = transform.position.x + (0.5f * formWidth);  
        float leftEdgeOfFormation = transform.position.x - (0.5f * formWidth);   

        if (leftEdgeOfFormation < xmin){  //
            movingRight = true;
        } else if (rightEdgeOfFormation > xmax){
            movingRight = false;
        }
        //Enemy movement section -end


        if (AllMembersDead()) {    //Are all the enemies dead?
            Debug.Log("Empty formation - All the Enemies are dead");

            SpawnUntilFull();

        }

    } //void Update -end




    bool AllMembersDead() {     //Are the enemies all dead?

        foreach (Transform childPositionGameObject in transform) {
            if (childPositionGameObject.childCount > 0) {
                return false;   //still enemies left
            }
        }
        return true;            //no enemies left
    } //AllMembersDead() -end




    void SpawnEnemies() {    //Enemy Spawner function
        //Spawn an enemy ship at each EnemyFormation 'position'
        foreach (Transform child in transform) {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        } //Spawn an enemy ship at each EnemyFormation 'position' -end

    }



    void SpawnUntilFull() {
        Transform freePosition = NextFreePosition();  //
        if (freePosition) {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
            if (NextFreePosition()) {
                Invoke("SpawnUntilFull", spawnDelay);    //Invokes the function again after period of time held in spawnDelay
            }
        }




    Transform NextFreePosition() {   //Single enemy spawner function

        foreach (Transform childPositionGameObject in transform) {
            if (childPositionGameObject.childCount == 0) {   //child Position no enemy in it?
                return childPositionGameObject;  
            }
        }
        return null;            


    }


}

	