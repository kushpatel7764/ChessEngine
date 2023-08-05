using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject greenSpotPrefab;
    private GameObject greenSpot;

    Pawn_Placement pawn_Placement;
    GameObject pawnPlacementObject;

    private GameObject[] allGameObjects;

    public GameObject selectedGameObject;

    string selectionSpotName = "SelectionSpot";
    string greenSpotName = "GreenSpot(Clone)";

    public void DestroyGreenSpots() { //Destroys all the green Spots in the scene
        allGameObjects = GameObject.FindObjectsOfType<GameObject>();

        if (GameObject.Find("GreenSpot(Clone)")) {
            foreach (GameObject obj in allGameObjects) {
                if (obj.tag == "GreenSpot") {
                    Destroy(obj);
                }
                if (!GameObject.Find("GreenSpot(Clone)")){
                    break;
                }
            }
        }
    }

    private bool IsChessPiece(GameObject obj) {

        if (obj.tag.StartsWith("B") == true || obj.tag.StartsWith("W") == true) {
            return true;
        } else { return false; }
    }

    private void TurnSystem(char alternate) {
        allGameObjects = GameObject.FindObjectsOfType<GameObject>();

        //Alternate is either B(Black) or W(White)
        foreach (GameObject obj in allGameObjects) {
            if (obj.GetComponent<CircleCollider2D>()) {
                //disable blackPieces first 
                string pieceColor = obj.tag;  
                if (pieceColor[0] == alternate){ // if black (could be white as well) 
                    obj.GetComponent<CircleCollider2D>().enabled = false;
                }else{
                    obj.GetComponent<CircleCollider2D>().enabled = true; 
                }
               
                

            } else { continue; }
        } 
    }

    public GameObject LocateChessPieceAt(Vector3 location) {
        allGameObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allGameObjects) {
            if (IsChessPiece(obj)) { 
                if (obj.transform.position == location) { 
                    return obj;
                }
            }
        }

        return null;
    }

    private void Awake() {
        pawnPlacementObject = GameObject.Find("GameManager");
        pawn_Placement = pawnPlacementObject.GetComponent<Pawn_Placement>();

        greenSpot = Instantiate(greenSpotPrefab, new Vector2(20, 20), Quaternion.identity);
        greenSpot.name = selectionSpotName;
        greenSpot.tag = selectionSpotName;
    }

    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hitByRay = Physics2D.Raycast(ray.origin, ray.direction);
        
        if (Input.GetMouseButtonDown(0)) {

            GameObject lastChessPieceClicked = selectedGameObject;
            
            if (hitByRay.collider != null) {   // Detect the ray hit a object with a collider

                var ColliderhitByRay = hitByRay.collider;

                selectedGameObject = ColliderhitByRay.gameObject; // The object that the ray hits
                greenSpot.transform.position = selectedGameObject.transform.position; // creates the green block under the chess piece

                bool isKillSpot = ColliderhitByRay.GetComponent<SpriteRenderer>().color == Color.red;

                if (isKillSpot) { //Killing the pieces
                    GameObject killSpot = ColliderhitByRay.gameObject;
                    GameObject pieceToKill = LocateChessPieceAt(killSpot.transform.position);

                    Destroy(pieceToKill);
                }

                if (IsChessPiece(ColliderhitByRay.gameObject)) {
                    lastChessPieceClicked = ColliderhitByRay.gameObject; 
                }

                if (ColliderhitByRay.name == greenSpotName) {
                    // If the ray is a greenSpot
                    
                    lastChessPieceClicked.transform.position = ColliderhitByRay.transform.position; // Moves the chesspiece to the spot that is clicked

                    pawn_Placement.DestroyGreenSpots(); 
                    GameObject.Find(selectionSpotName).transform.position = new Vector2(20, 20); //Move the selection spot out of the scene

                    //Change Turns depending on the starter player. First to move a piece is first and then eveything alternates from there. 
                    string tagOfClickedPiece = lastChessPieceClicked.tag;
                    if (tagOfClickedPiece[0] == 'B') {
                        //Alternate gets disables so if black moves then black should be the one to be disabled 
                        TurnSystem('B');

                    } else {
                        TurnSystem('W');
                    }
                }
            }
        }
    }
}
