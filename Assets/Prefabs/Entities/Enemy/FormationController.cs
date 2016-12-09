using UnityEngine;
using System.Collections;

public class FormationController : MonoBehaviour
{
    //Declarations
    public GameObject enemyPrefab;
    public float formWidth = 10f;
    public float formHeight = 10f;
    public bool movingRight = true;
    public float speed = 5f;

    private float xmax;
    private float xmin;




    // Use this for initialization
    void Start() {

        //edge of screen stuff
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundry = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightBoundry = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xmax = rightBoundry.x;
        xmin = leftBoundry.x;
        //edge of screen stuff - end


            foreach (Transform child in transform) {

            GameObject enemy = Instantiate(enemyPrefab, child.transform.position , Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }

    }


    public void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position, new Vector3(formWidth, formHeight));
    }



    void Update()
    {

        if (movingRight) {
            transform.position += new Vector3(speed * Time.deltaTime, 0);

        } else {
            transform.position += new Vector3(-speed * Time.deltaTime, 0);

        }

        float rightEdgeOfFormation = transform.position.x + (0.5f * formWidth);
        float leftEdgeOfFormation = transform.position.x - (0.5f * formWidth);

        if (leftEdgeOfFormation < xmin)
        {
            movingRight = true;
        }
        else if (rightEdgeOfFormation > xmax)
        {

            movingRight = false;
        }

    
    }





}

	