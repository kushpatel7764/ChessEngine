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

    public GameObject greenSpot;

    GameManager gameManager;
    [SerializeField] GameObject gameManagerObject;

    public void DestroyGreenSpots() { //Destroys all the green Spots in the scene
        allGameObjects = GameObject.FindObjectsOfType<GameObject>();

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

    private GameObject ChessPieceIdentifier(Vector3 location) {
        foreach (GameObject obj in allGameObjects) {
            if ((obj.name != "ChessSpot(Clone)") && (obj.name != "GreenSpot(Clone)")) {
                if (obj.transform.position == location) {
                    return obj;
                }
            }
        }

        return null;
    }

    private void CreateGreenSpots(string type, int moves, Vector2 pawnPosition) { // Creates the green Spots for the movement options of the pawn
        if (type == "Black") {
            
            for (var i = 0; i < moves; i++) {
                Instantiate(greenSpot, new Vector2(pawnPosition.x, pawnPosition.y - i), Quaternion.identity);
            }

            if (ChessPieceIdentifier(new Vector3(pawnPosition.x + 1, pawnPosition.y - 1)) != null) {
                var redSpot = Instantiate(greenSpot, new Vector3(pawnPosition.x + 1, pawnPosition.y - 1, 0), Quaternion.identity);

                redSpot.GetComponent<SpriteRenderer>().color = Color.red;
            }
            if (ChessPieceIdentifier(new Vector3(pawnPosition.x - 1, pawnPosition.y - 1)) != null) {
                var redSpot = Instantiate(greenSpot, new Vector3(pawnPosition.x - 1, pawnPosition.y - 1, 0), Quaternion.identity);

                redSpot.GetComponent<SpriteRenderer>().color = Color.red;
            }
        } 

        if (type == "White") {
            
            for (var i = 0; i < moves; i++) {
                Instantiate(greenSpot, new Vector2(pawnPosition.x, pawnPosition.y + i), Quaternion.identity);
            }

            if (ChessPieceIdentifier(new Vector3(pawnPosition.x + 1, pawnPosition.y + 1)) != null) {
                var redSpot = Instantiate(greenSpot, new Vector3(pawnPosition.x + 1, pawnPosition.y + 1, 0), Quaternion.identity);

                redSpot.GetComponent<SpriteRenderer>().color = Color.red;
            }
            if (ChessPieceIdentifier(new Vector3(pawnPosition.x - 1, pawnPosition.y + 1)) != null) {
                var redSpot = Instantiate(greenSpot, new Vector3(pawnPosition.x - 1, pawnPosition.y + 1, 0), Quaternion.identity);

                redSpot.GetComponent<SpriteRenderer>().color = Color.red;
            }

        } 
        
    }

    private void Awake() { //Assign the GameManager Componenet and Object
        gameManagerObject = GameObject.Find("GameManager");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update() {

        if (gameManager.selectedObject != null) {
            if (gameManager.selectedObject.tag == "BlackPawn" || gameManager.selectedObject.tag == "WhitePawn") { // Make sure it is a pawn
                Vector2 pawnPosition = gameManager.selectedObject.transform.position; // The position of the selected Pawn

                if (gameManager.selectedObject.tag == "BlackPawn") {
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

                if (gameManager.selectedObject.tag == "WhitePawn") {
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
