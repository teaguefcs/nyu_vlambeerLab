using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChecker : MonoBehaviour {

    public Transform[] floorTypes;
    public Transform tile;
    private int cardinal, diagonal;
    private bool north, south, east, west, northeast, northwest, southeast, southwest;

    private static Dictionary<Vector3, bool> checkedTiles;

    void Start () {
        tile = transform.parent;
        cardinal = 0;
        diagonal = 0;
        if (GameObject.FindGameObjectsWithTag("Checker").Length <= 1)
        {
            checkedTiles = new Dictionary<Vector3, bool>();
        }
        CheckTile(transform.position);
        north = CheckTile(transform.position + (Vector3.forward * 5));
        if (north) cardinal++;
        northeast = CheckTile(transform.position + (Vector3.forward * 5) + (Vector3.right * 5));
        if (northeast) diagonal++;
        east = CheckTile(transform.position + (Vector3.right * 5));
        if (east) cardinal++;
        southeast = CheckTile(transform.position + (Vector3.back * 5) + (Vector3.right * 5));
        if (southeast) diagonal++;
        south = CheckTile(transform.position + (Vector3.back * 5));
        if (south) cardinal++;
        southwest = CheckTile(transform.position + (Vector3.back * 5) + (Vector3.left * 5));
        if (southwest) diagonal++;
        west = CheckTile(transform.position + (Vector3.left * 5));
        if (west) cardinal++;
        northwest = CheckTile(transform.position + (Vector3.forward * 5) + (Vector3.left * 5));
        if (northwest) diagonal++;
        Transform created;
        Vector3 rotation = Vector3.zero;
        int floorType = 0;
        switch (cardinal){
            case 4:
                switch (diagonal)
                {
                    case 0:
                        floorType = 0;
                        created = Instantiate(floorTypes[floorType], tile.position, tile.rotation);
                        break;
                    case 1:
                        floorType = 1;
                        if (southeast)
                        {
                            rotation.y = 90;
                        }
                        else if (southwest)
                        {
                            rotation.y = 180;
                        }
                        else if (northwest)
                        {
                            rotation.y = 270;
                        }
                        created = Instantiate(floorTypes[floorType], tile.position, tile.rotation);
                        created.Rotate(rotation);
                        break;
                    case 2:
                        if (northeast)
                        {
                            if (southwest) floorType = 3;
                            else if (southeast) floorType = 2;
                            else { floorType = 2; rotation.y = 270; }
                        }
                        else if (southeast)
                        {
                            rotation.y = 90;
                            if (southwest) floorType = 2;
                            else floorType = 3;
                        }
                        else if (southwest)
                        {
                            rotation.y = 180;
                            floorType = 2;
                        }
                        created = Instantiate(floorTypes[floorType], tile.position, tile.rotation);
                        created.Rotate(rotation);
                        break;
                    case 3:
                        floorType = 4;
                        if (!southeast) rotation.y = 90;
                        else if (!southwest) rotation.y = 180;
                        else if (!northwest) rotation.y = 270;
                        created = Instantiate(floorTypes[floorType], tile.position, tile.rotation);
                        created.Rotate(rotation);
                        break;
                }
                break;
            default:
                break;
        }
    }

    private bool CheckTile(Vector3 position)
    {
        if (checkedTiles.ContainsKey(position))
        {
            return checkedTiles[position];
        }
        else
        {
            Ray ray = new Ray(position, Vector3.down);
            checkedTiles.Add(position, Physics.Raycast(ray, 15f));
            return checkedTiles[position];
        }
    }
}
