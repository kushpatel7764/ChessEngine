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

        if (GameObject.Find("GreenSpot(Clone)")) {
            foreach (GameObject obj in allGameObjects) {
                if (obj.tag == "GreenSpot") {
                    Destroy(obj);
                }
            }
        }
    }

    private void CreateGreenSpots(string type, int moves, Vector2 pawnPosition) { // Creates the green Spots for the movement options of the pawn
        if (type == "Black") { // The Black Piece
            
            for (var i = 1; i < moves; i++) {
                
                if ((gameManager.LocateChessPieceAt(new Vector3(pawnPosition.x, pawnPosition.y - i, 0)) != null) && (gameManager.LocateChessPieceAt(new Vector3(pawnPosition.x, pawnPosition.y - i, 0)).tag.StartsWith("B") == true || gameManager.LocateChessPieceAt(new Vector3(pawnPosition.x, pawnPosition.y - i, 0)).tag.StartsWith("W") == true)) {
                    break;
                }

                Instantiate(movementBlock, new Vector2(pawnPosition.x, pawnPosition.y - i), Quaternion.identity); //create the green spots that appear when you click on the pawn
            }

            if (gameManager.LocateChessPieceAt(new Vector3(pawnPosition.x + 1, pawnPosition.y - 1)) != null && !gameManager.LocateChessPieceAt(new Vector3(pawnPosition.x + 1, pawnPosition.y - 1)).tag.StartsWith("B")) { // This is what creates the spots that indicate a kill
                var redSpot = Instantiate(movementBlock, new Vector3(pawnPosition.x + 1, pawnPosition.y - 1, 0), Quaternion.identity); 

                redSpot.GetComponent<SpriteRenderer>().color = Color.red;
            }
            if (gameManager.LocateChessPieceAt(new Vector3(pawnPosition.x - 1, pawnPosition.y - 1)) != null && !gameManager.LocateChessPieceAt(new Vector3(pawnPosition.x - 1, pawnPosition.y - 1)).tag.StartsWith("B")) { 
                var redSpot = Instantiate(movementBlock, new Vector3(pawnPosition.x - 1, pawnPosition.y - 1, 0), Quaternion.identity);

                redSpot.GetComponent<SpriteRenderer>().color = Color.red;
            }
        } 

        if (type == "White") { // The white pieces
            
            for (var i = 1; i < moves; i++) {
                Instantiate(movementBlock, new Vector2(pawnPosition.x, pawnPosition.y + i), Quaternion.identity);
            }

            if ((gameManager.LocateChessPieceAt(new Vector3(pawnPosition.x + 1, pawnPosition.y + 1)) && (gameManager.LocateChessPieceAt(new Vector3(pawnPosition.x + 1, pawnPosition.y + 1)).tag.StartsWith("W") != true))) {
                var redSpot = Instantiate(movementBlock, new Vector3(pawnPosition.x + 1, pawnPosition.y + 1, 0), Quaternion.identity);

                redSpot.GetComponent<SpriteRenderer>().color = Color.red;
            }
            if (gameManager.LocateChessPieceAt(new Vector3(pawnPosition.x - 1, pawnPosition.y + 1)) != null && !gameManager.LocateChessPieceAt(new Vector3(pawnPosition.x - 1, pawnPosition.y + 1)).tag.StartsWith("W")) {
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
        
        if (gameManager.selectedGameObject != null) {
            bool isBlackPawn = gameManager.selectedGameObject.tag == "BlackPawn";
            bool isWhitePawn = gameManager.selectedGameObject.tag == "WhitePawn";


            if (isBlackPawn || isWhitePawn) { 
                Vector2 selectedPawnPosition = gameManager.selectedGameObject.transform.position; 

                if (gameManager.selectedGameObject.tag == "BlackPawn") { // Black Piece
                    if (selectedPawnPosition.y == 6) { // Move Twice

                        int numberOfMoves = 3;

                        DestroyGreenSpots();
                        CreateGreenSpots("Black", numberOfMoves, selectedPawnPosition);

                    } else { // Move Once

                        int numberOfMoves = 2;

                        DestroyGreenSpots();
                        CreateGreenSpots("Black", numberOfMoves, selectedPawnPosition);
                    }
                }

                if (gameManager.selectedGameObject.tag == "WhitePawn") { // White Piece
                    if (selectedPawnPosition.y == 1) { // Move Twice

                        int numberOfMoves = 3;

                        DestroyGreenSpots();
                        CreateGreenSpots("White", numberOfMoves, selectedPawnPosition);

                    } else { // Move Once

                        int numberOfMoves = 2;

                        DestroyGreenSpots();
                        CreateGreenSpots("White", numberOfMoves, selectedPawnPosition);
                    }
                }
            }
        }
    }
}
