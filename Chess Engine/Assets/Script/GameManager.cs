using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject greenSpotPrefab;
    private GameObject greenSpot;

    Pawn_Placement pawn_Placement;
    GameObject pawnPlacementObject;

    private GameObject[] allGameObjects;
    private List<GameObject> chessSpotArray = new List<GameObject>();

    public GameObject selectedObject;
    public string selectedObjectName;

    private GameObject ChessSpot(int x, int y) { // returns the exact chessSpot that is in the given coordinates
        foreach (GameObject obj in chessSpotArray) {
            if (obj.transform.position.x == x && obj.transform.position.y == y) {
                return obj;
            } else { continue; }
        }
        return null; 
    }

    private void Awake() {
        pawnPlacementObject = GameObject.Find("GameManager");
        pawn_Placement = pawnPlacementObject.GetComponent<Pawn_Placement>();
    }

    private void Start() {
        greenSpot = Instantiate(greenSpotPrefab, new Vector2(20, 20), Quaternion.identity);
        greenSpot.name = "selectionSpot";


    }

    void Update()
    {
        /*
         * Create a array with every single chessSpot
         * Creates a ray that will detect what gameObject is pressed and returns the name of the Object
         * Manages the selection of Game Pieces
         */

        allGameObjects = GameObject.FindObjectsOfType<GameObject>();

        //Creating a array of all the ChessSpots
        foreach (GameObject obj in allGameObjects) {
            
            if (obj.name == "ChessSpot(Clone)") {
                chessSpotArray.Add(obj); 
                if (chessSpotArray.Count == 64) {
                    break;
                }
            }
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        
        if (Input.GetMouseButtonDown(0)) {
            GameObject chessPieceClicked = selectedObject;

            if (hit.collider != null && hit.collider.name != "GreenSpot(Clone)" && hit.collider.name != "ChessSpot(Clone)") {
                chessPieceClicked = hit.collider.gameObject; // stores the last chess piece clicked into a variable
            }

            if (hit.collider != null) { 
                selectedObjectName = hit.collider.name; 
                selectedObject = hit.collider.gameObject; // store the object that the ray hits into a variable

                greenSpot.transform.position = selectedObject.transform.position;
            }

            if (hit.collider != null && hit.collider.name == "GreenSpot(Clone)") {
                chessPieceClicked.transform.position = hit.collider.transform.position; // Moves the chesspiece to the spot that is clicked

                pawn_Placement.DestroyGreenSpots();
                GameObject.Find("selectionSpot").transform.position = new Vector2(20, 20);
            }
        }
    }
}
