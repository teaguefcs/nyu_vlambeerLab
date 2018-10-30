using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathmaker : MonoBehaviour
{
   
    private int lifeSpan, counter;
    private float leftProb, rightProb, spawnProb;

    public static int tileCount = 0;

    public Transform floorPrefab;
    public Transform[] types;
    public Transform pathmakerSpherePrefab;
    public CameraManager main;

    void Start()
    {
        main = Camera.main.GetComponent<CameraManager>();
        counter = 0;
        lifeSpan = Random.Range(25, 50);
        leftProb = Random.Range(.2f, .3f);
        rightProb = leftProb + Random.Range(.2f, .3f);
        spawnProb = Random.Range(.9f, .95f);
    }

    void Update()
    {
        if (transform.position.z > main.furthestNorth)
        {
            main.furthestNorth = transform.position.z;
        }
        if (transform.position.x > main.furthestEast)
        {
            main.furthestEast = transform.position.x;
        }
        if (transform.position.x < main.furthestWest)
        {
            main.furthestWest = transform.position.x;
        }
        if (transform.position.z < main.furthestSouth)
        {
            main.furthestSouth = transform.position.z;
        }

        if (counter < lifeSpan && tileCount < 500)
        {
            float randNum = Random.value;
            if (randNum < leftProb)
            {
                transform.Rotate(new Vector3(0, 90, 0));
            }
            else if (randNum < rightProb)
            {
                transform.Rotate(new Vector3(0, -90, 0));
            }
            else if (randNum >= spawnProb)
            {
                Instantiate(pathmakerSpherePrefab, transform.position, transform.rotation);
            }

            Ray checker = new Ray(transform.position, Vector3.down);

            if (!Physics.Raycast(checker, 1.5f))
            {
                Vector3 dest = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
                main.floors.Add(Instantiate(types[Random.Range(0,2)], dest + Vector3.down, Quaternion.Euler(Vector3.zero)));
                tileCount++;
            }

            transform.position = Vector3.MoveTowards(transform.position, transform.position + (transform.forward.normalized * 5f), 5f);

            counter++;
        }
        else
        {
            if (GameObject.FindGameObjectsWithTag("Pathmaker").Length <= 1)
            {
                main.finished = true;
            }
            Destroy(gameObject);
        }
    }
}

// STEP 6:  =====================================================================================
// art pass, usability pass

// - add more detail to your original floorTile placeholder -- and let it randomly pick one of 3 different floorTile models, etc. so for example, it could randomly pick a "normal" floor tile, or a cactus, or a rock, or a skull
//		- MODEL 3 DIFFERENT TILES IN MAYA! DON'T STOP USING MAYA OR YOU'LL FORGET IT ALL
//		- add a simple in-game restart button; let us press [R] to reload the scene and see a new level generation
// - with Text UI, name your proc generation system ("AwesomeGen", "RobertGen", etc.) and display Text UI that tells us we can press [R]



// OPTIONAL EXTRA TASKS TO DO, IF YOU WANT: ===================================================

// DYNAMIC CAMERA:
// position the camera to center itself based on your generated world...
// 1. keep a list of all your spawned tiles
// 2. then calculate the average position of all of them (use a for() loop to go through the whole list) 
// 3. then move your camera to that averaged center and make sure fieldOfView is wide enough?

// BETTER UI:
// learn how to use UI Sliders (https://unity3d.com/learn/tutorials/topics/user-interface-ui/ui-slider) 
// let us tweak various parameters and settings of our tech demo
// let us click a UI Button to reload the scene, so we don't even need the keyboard anymore!

// WALL GENERATION
// add a "wall pass" to your proc gen after it generates all the floors
// 1. raycast downwards around each floor tile (that'd be 8 raycasts per floor tile, in a square "ring" around each tile?)
// 2. if the raycast "fails" that means there's empty void there, so then instantiate a Wall tile prefab
// 3. ... repeat until walls surround your entire floorplan
// (technically, you will end up raycasting the same spot over and over... but the "proper" way to do this would involve keeping more lists and arrays to track all this data)
