using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour {

    public float furthestNorth, furthestEast, furthestWest, furthestSouth;
    private float prevFurthestN, prevFurthestE, prevFurthestW, prevFurthestS;
    public List<Transform> floors;
    public bool finished = false;
    public Transform tileChecker;

    private int tileIndex = 0;

	// Use this for initialization
	void Start () {
        furthestNorth = 0;
        furthestSouth = 0;
        furthestEast = 0;
        furthestWest = 0;
        floors = new List<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (furthestNorth != prevFurthestN || furthestEast != prevFurthestE || furthestSouth != prevFurthestS || furthestWest != prevFurthestW) {
        Vector3 newPos = new Vector3((furthestEast + furthestWest) / 2, 0, (furthestNorth + furthestSouth) / 2);
        float height = 0;
        if (Mathf.Abs(furthestEast - furthestWest) * 9 >= Mathf.Abs(furthestNorth - furthestSouth) * 16)
        {
            height = Mathf.Cos(Mathf.Deg2Rad * 30) / (Mathf.Sin(Mathf.Deg2Rad * 30) / (Mathf.Abs(furthestEast - furthestWest) / 2));
            newPos.y = height;
        }
        else
        {
            height = Mathf.Cos(Mathf.Deg2Rad * 25) / (Mathf.Sin(Mathf.Deg2Rad * 25) / (Mathf.Abs(furthestNorth - furthestSouth) / 2));
            newPos.y = height;
        }
        transform.position = newPos;

        /*
        if (finished && tileIndex < floors.Count)
        {
            Transform checker = Instantiate(tileChecker, floors[tileIndex]);
            checker.localPosition = Vector3.up * 10;
            tileIndex++;
        }
        */

        if (Input.GetKeyDown(KeyCode.R))
        {
            Pathmaker.tileCount = 0;
            SceneManager.LoadScene("mainLabScene");
        }
	}
}
