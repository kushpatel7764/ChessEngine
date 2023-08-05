using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bishop_Placemen : MonoBehaviour {
    
    [SerializeField] GameObject movementSpot;
    [SerializeField] GameManager gameManager;

    GameObject currentlySelectedObject;

    private bool IsInMap(Vector3 spotPosition) { // Checks if the position given is inside the board
        return spotPosition.x >= 0 && spotPosition.x <= 7 && spotPosition.y >= 0 && spotPosition.y <= 7;
    }

    List<Vector3> CalculateDiagonalMoves(string direction) {

        List<Vector3> diagonalMoves = new List<Vector3>();
        string bishopTag = currentlySelectedObject.tag;

        Vector3 bishopPos = currentlySelectedObject.transform.position;
        Vector2Int directionVector = Vector2Int.zero;

        switch (direction) {
            case "topRight": directionVector = Vector2Int.up + Vector2Int.right; break;
            case "topLeft": directionVector = Vector2Int.up + Vector2Int.left; break;
            case "bottomRight": directionVector = Vector2Int.down + Vector2Int.right; break;
            case "bottomLeft": directionVector = Vector2Int.down + Vector2Int.left; break;
        }

        const int maxDiagonalLength = 7;

        for (var i = 1; i <= maxDiagonalLength; i++) {
            Vector3 nextPosition = bishopPos + new Vector3(directionVector.x * i, directionVector.y * i, 0);

            if (!IsInMap(nextPosition)) { break; }

            GameObject blockingPiece = gameManager.LocateChessPieceAt(nextPosition);

            if (blockingPiece != null ) {
                print(blockingPiece.tag[0] == bishopTag[0]);
                if (blockingPiece.tag[0] == bishopTag[0]) break;

                GameObject killSpot = Instantiate(movementSpot, nextPosition, Quaternion.identity);

                killSpot.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            }

            diagonalMoves.Add(nextPosition);
        }

        return diagonalMoves;
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
            
            bool isBishop = currentlySelectedObject.tag == "WhiteBishop" || currentlySelectedObject.tag == "BlackBishop";
            
            if (isBishop) {
                gameManager.DestroyGreenSpots(); // Destroy previous green Spots

                CreateMovementSpots(CalculateDiagonalMoves("topRight")); // create green spots in each direction
                CreateMovementSpots(CalculateDiagonalMoves("topLeft"));
                CreateMovementSpots(CalculateDiagonalMoves("bottomRight"));
                CreateMovementSpots(CalculateDiagonalMoves("bottomLeft"));
            }
        }
    }
}
