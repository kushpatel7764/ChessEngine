using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse_Placement : MonoBehaviour
{
    [SerializeField] GameObject movementSpot;
    [SerializeField] GameManager gameManager;

    GameObject currentlySelectedObject;

    private bool IsInMap(Vector3 spotPosition) { // Checks if the position given is inside the board
        return spotPosition.x >= 0 && spotPosition.x <= 7 && spotPosition.y >= 0 && spotPosition.y <= 7;
    }

     List<Vector3> CalculateHorseMoves(string direction) {

        List<Vector3> horseMoves = new List<Vector3>();
        string horseTag = currentlySelectedObject.tag;

        Vector3 horsePos = currentlySelectedObject.transform.position;
        Vector2Int directionVector = Vector2Int.zero;

        switch (direction) {
            case "topRight": directionVector = (Vector2Int.up*2) + Vector2Int.right; break;
            case "topLeft": directionVector = (Vector2Int.up*2) + Vector2Int.left; break;
            case "bottomRight": directionVector = (Vector2Int.down*2) + Vector2Int.right; break;
            case "bottomLeft": directionVector = (Vector2Int.down*2) + Vector2Int.left; break;
            case "leftUp": directionVector = (Vector2Int.left*2) + Vector2Int.up; break; 

            case "rightUp": directionVector = (Vector2Int.right*2) + Vector2Int.up; break;

            case "leftDown":  directionVector = (Vector2Int.left*2) + Vector2Int.down; break;

            case "rightDown":  directionVector = (Vector2Int.right*2) + Vector2Int.down; break;

        }

        const int maxDiagonalLength = 1;

        for (var i = 1; i <= maxDiagonalLength; i++) {
            Vector3 nextPosition = horsePos + new Vector3(directionVector.x * i, directionVector.y * i, 0);

            if (!IsInMap(nextPosition)) { break; }

            GameObject blockingPiece = gameManager.LocateChessPieceAt(nextPosition);

            if (blockingPiece != null ) {
                print(blockingPiece.tag[0] == horseTag[0]);
                if (blockingPiece.tag[0] == horseTag[0]) break;

                GameObject killSpot = Instantiate(movementSpot, nextPosition, Quaternion.identity);

                killSpot.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            }

            horseMoves.Add(nextPosition);
        }

        return horseMoves;
    }

    void CreateMovementSpots(List<Vector3> positionsList) {
        print(positionsList.Count);
        foreach (Vector3 position in positionsList) { 
            //Instantiate Horse move spots from calculatedHorseMoves's positionsList. 
            Instantiate(movementSpot, position, Quaternion.identity);
        }

    }
    // Update is called once per frame
    void Update()
    {
         currentlySelectedObject = gameManager.selectedGameObject;
        
        if (gameManager.selectedGameObject != null) {
            
            bool isHorse = currentlySelectedObject.tag == "WhiteHorse" || currentlySelectedObject.tag == "BlackHorse";
            
            if (isHorse) {
                gameManager.DestroyGreenSpots(); // Destroy previous green Spots

                CreateMovementSpots(CalculateHorseMoves("topRight")); // create green spots in each direction
                CreateMovementSpots(CalculateHorseMoves("topLeft"));
                CreateMovementSpots(CalculateHorseMoves("bottomRight"));
                CreateMovementSpots(CalculateHorseMoves("bottomLeft"));
                CreateMovementSpots(CalculateHorseMoves("leftUp"));
                CreateMovementSpots(CalculateHorseMoves("leftDown"));
                CreateMovementSpots(CalculateHorseMoves("rightUp"));
                CreateMovementSpots(CalculateHorseMoves("rightDown"));

            }
        }
    }
}
