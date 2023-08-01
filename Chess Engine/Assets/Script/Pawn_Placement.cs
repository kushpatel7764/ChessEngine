using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using TMPro;
using UnityEngine;

public class Pawn_Placement : MonoBehaviour
{
    GameObject[] allGameObjects;

    public GameObject movementBlock;

    GameManager gameManager;
    [SerializeField] GameObject gameManagerObject;

    public void DestroyGreenSpots() { //Destroys all the green Spots in the scene
        allGameObjects = GameObject.FindObjectsOfType<GameObject>();
        
        /*
         * Loop through all game objects if there is a green spot in the scene.
         * when ever the object land on a object with the greenspot name it destroys the object 
         */

        if (GameObject.Find("GreenSpot(Clone)")) {
            foreach (GameObject obj in allGameObjects) {
                if (obj.name == "GreenSpot(Clone)") {
                    Destroy(obj);
                    if (!GameObject.Find("GreenSpot(Clone)")) {
                        break;
                    }
                }
            }
        } else { print("No green spots to destroy!"); }
    }



    private void CreateGreenSpots(string type, int moves, Vector2 pawnPosition) { // Creates the green Spots for the movement options of the pawn
        if (type == "Black") { // The Black Piece
            
            for (var i = 1; i < moves; i++) {
                
                if ((gameManager.ChessPieceIdentifier(new Vector3(pawnPosition.x, pawnPosition.y - i, 0)) != null) && (gameManager.ChessPieceIdentifier(new Vector3(pawnPosition.x, pawnPosition.y - i, 0)).tag.StartsWith("B") == true || gameManager.ChessPieceIdentifier(new Vector3(pawnPosition.x, pawnPosition.y - i, 0)).tag.StartsWith("W") == true)) {
                    break;
                }

                Instantiate(movementBlock, new Vector2(pawnPosition.x, pawnPosition.y - i), Quaternion.identity); //create the green spots that appear when you click on the pawn
            }

            if (gameManager.ChessPieceIdentifier(new Vector3(pawnPosition.x + 1, pawnPosition.y - 1)) != null) { // This is what creates the spots that indicate a kill
                var redSpot = Instantiate(movementBlock, new Vector3(pawnPosition.x + 1, pawnPosition.y - 1, 0), Quaternion.identity); 

                redSpot.GetComponent<SpriteRenderer>().color = Color.red;
            }
            if (gameManager.ChessPieceIdentifier(new Vector3(pawnPosition.x - 1, pawnPosition.y - 1)) != null) { 
                var redSpot = Instantiate(movementBlock, new Vector3(pawnPosition.x - 1, pawnPosition.y - 1, 0), Quaternion.identity);

                redSpot.GetComponent<SpriteRenderer>().color = Color.red; 
            }
        } 

        if (type == "White") { // The white pieces
            
            for (var i = 1; i < moves; i++) {
                Instantiate(movementBlock, new Vector2(pawnPosition.x, pawnPosition.y + i), Quaternion.identity);
            }

            if ((gameManager.ChessPieceIdentifier(new Vector3(pawnPosition.x + 1, pawnPosition.y + 1)) && (gameManager.ChessPieceIdentifier(new Vector3(pawnPosition.x + 1, pawnPosition.y + 1)).tag.StartsWith("W") != true))) {
                var redSpot = Instantiate(movementBlock, new Vector3(pawnPosition.x + 1, pawnPosition.y + 1, 0), Quaternion.identity);

                redSpot.GetComponent<SpriteRenderer>().color = Color.red;

            }
            if (gameManager.ChessPieceIdentifier(new Vector3(pawnPosition.x - 1, pawnPosition.y + 1)) != null) {
                var redSpot = Instantiate(movementBlock, new Vector3(pawnPosition.x - 1, pawnPosition.y + 1, 0), Quaternion.identity);

                redSpot.GetComponent<SpriteRenderer>().color = Color.red;
            }

        } 
        
    }



    private void Awake() { //Assign the GameManager Component and Object
        gameManagerObject = GameObject.Find("GameManager");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update() {
        
        if (gameManager.selectedObject != null) {
            if (gameManager.selectedObject.tag == "BlackPawn" || gameManager.selectedObject.tag == "WhitePawn") { // Make sure it is a pawn
                Vector2 pawnPosition = gameManager.selectedObject.transform.position; // The position of the selected Pawn(Always Updating)

                if (gameManager.selectedObject.tag == "BlackPawn") { // Black Piece
                    if (pawnPosition.y == 6) { // Move Twice
                        int numberOfMoves = 3;

                        DestroyGreenSpots();
                        CreateGreenSpots("Black", numberOfMoves, pawnPosition);
                    } else { // Move Once
                        int numberOfMoves = 2;

                        DestroyGreenSpots();
                        CreateGreenSpots("Black", numberOfMoves, pawnPosition);
                    }
                }

                if (gameManager.selectedObject.tag == "WhitePawn") { // White Piece
                    if (pawnPosition.y == 1) { // Move Twice
                        int numberOfMoves = 3;

                        DestroyGreenSpots();
                        CreateGreenSpots("White", numberOfMoves, pawnPosition);
                    } else { // Move Once
                        int numberOfMoves = 2;

                        DestroyGreenSpots();
                        CreateGreenSpots("White", numberOfMoves, pawnPosition);
                    }
                }
            }
        }
    }
}
