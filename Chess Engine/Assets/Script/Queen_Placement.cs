using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen_Placement : MonoBehaviour
{
   
    [SerializeField] GameObject movementSpot;
    [SerializeField] GameManager gameManager;

    GameObject currentlySelectedObject;

    private bool IsInMap(Vector3 spotPosition) { // Checks if the position given is inside the board
        return spotPosition.x >= 0 && spotPosition.x <= 7 && spotPosition.y >= 0 && spotPosition.y <= 7;
    }

    List<Vector3> CalculateQueenMoves(string direction) {

        List<Vector3> queenMoves = new List<Vector3>();
        string queenTag = currentlySelectedObject.tag;

        Vector3 queenPos = currentlySelectedObject.transform.position;
        Vector2Int directionVector = Vector2Int.zero;

        switch (direction) {
            case "topRight": directionVector = Vector2Int.up + Vector2Int.right; break;
            case "topLeft": directionVector = Vector2Int.up + Vector2Int.left; break;
            case "bottomRight": directionVector = Vector2Int.down + Vector2Int.right; break;
            case "bottomLeft": directionVector = Vector2Int.down + Vector2Int.left; break;
            case "Up": directionVector = Vector2Int.up; break;
            case "Down": directionVector = Vector2Int.down; break;
            case "Right": directionVector = Vector2Int.right; break;
            case "Left": directionVector = Vector2Int.left; break;
        }

        const int maxDiagonalLength = 7;

        for (var i = 1; i <= maxDiagonalLength; i++) {
            Vector3 nextPosition = queenPos + new Vector3(directionVector.x * i, directionVector.y * i, 0);

            if (!IsInMap(nextPosition)) { break; }

            GameObject blockingPiece = gameManager.LocateChessPieceAt(nextPosition);

            if (blockingPiece != null ) {
                print(blockingPiece.tag[0] == queenTag[0]);
                if (blockingPiece.tag[0] == queenTag[0]) break;

                GameObject killSpot = Instantiate(movementSpot, nextPosition, Quaternion.identity);

                killSpot.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            }

            queenMoves.Add(nextPosition);
        }

        return queenMoves;
    }

    void CreateMovementSpots(List<Vector3> positionsList) {
        print(positionsList.Count);
        foreach (Vector3 position in positionsList) { // loop through every vector3 position inside the four arrays that are created in the MovesArray() Method
            Instantiate(movementSpot, position, Quaternion.identity);
        }

    }

    void Update()
    {
        currentlySelectedObject = gameManager.selectedGameObject;
        
        if (gameManager.selectedGameObject != null) {
            
            bool isQueen = currentlySelectedObject.tag == "WhiteQueen" || currentlySelectedObject.tag == "BlackQueen";
            
            if (isQueen) {
                gameManager.DestroyGreenSpots(); // Destroy previous green Spots

                CreateMovementSpots(CalculateQueenMoves("topRight")); // create green spots in each direction
                CreateMovementSpots(CalculateQueenMoves("topLeft"));
                CreateMovementSpots(CalculateQueenMoves("bottomRight"));
                CreateMovementSpots(CalculateQueenMoves("bottomLeft"));
                CreateMovementSpots(CalculateQueenMoves("Left"));
                CreateMovementSpots(CalculateQueenMoves("Right"));
                CreateMovementSpots(CalculateQueenMoves("Up"));
                CreateMovementSpots(CalculateQueenMoves("Down"));

            }
        }
    }
}
