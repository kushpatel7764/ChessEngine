using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook_Placement : MonoBehaviour
{
    [SerializeField] GameObject movementSpot;
    [SerializeField] GameManager gameManager;

    GameObject currentlySelectedObject;


    private bool IsInMap(Vector3 spotPosition) { // Checks if the position given is inside the board
        return spotPosition.x >= 0 && spotPosition.x <= 7 && spotPosition.y >= 0 && spotPosition.y <= 7;
    }

    List<Vector3> CalculateDiagonalMoves(string direction) {

        List<Vector3> directionMoves = new List<Vector3>();
        string rookTag = currentlySelectedObject.tag;

        Vector3 rookPos = currentlySelectedObject.transform.position;
        Vector2Int directionVector = Vector2Int.zero;

        switch (direction) {
            case "Up": directionVector = Vector2Int.up; break;
            case "Down": directionVector = Vector2Int.down; break;
            case "Right": directionVector = Vector2Int.right; break;
            case "Left": directionVector = Vector2Int.left; break;
        }

        const int maxBoardLength = 7;

        for (var i = 1; i <= maxBoardLength; i++) {
            Vector3 nextPosition = rookPos + new Vector3(directionVector.x * i, directionVector.y * i, 0);

            if (!IsInMap(nextPosition)) { break; }

            GameObject blockingPiece = gameManager.LocateChessPieceAt(nextPosition);

            if (blockingPiece != null) {
                print(blockingPiece.tag[0] == rookTag[0]);
                if (blockingPiece.tag[0] == rookTag[0]) break;

                GameObject killSpot = Instantiate(movementSpot, nextPosition, Quaternion.identity);

                killSpot.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            }

            directionMoves.Add(nextPosition);
        }

        return directionMoves;
    }

    void CreateMovementSpots(List<Vector3> positionsList) {
        print(positionsList.Count);
        foreach (Vector3 position in positionsList) { // loop through every vector3 position inside the four arrays that are created in the MovesArray() Method
            Instantiate(movementSpot, position, Quaternion.identity);
        }

    }

    void Update() {
        currentlySelectedObject = gameManager.selectedGameObject;

        if (currentlySelectedObject != null) {

            bool isRook = currentlySelectedObject.tag == "WhiteRook" || currentlySelectedObject.tag == "BlackRook";

            if (isRook) {
                gameManager.DestroyGreenSpots(); // Destroy previous green Spots

                CreateMovementSpots(CalculateDiagonalMoves("Up")); // create green spots in each direction
                CreateMovementSpots(CalculateDiagonalMoves("Down"));
                CreateMovementSpots(CalculateDiagonalMoves("Right"));
                CreateMovementSpots(CalculateDiagonalMoves("Left"));
            }
        }
    }
}
