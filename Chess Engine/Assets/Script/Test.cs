using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Test : MonoBehaviour
{

    [SerializeField] GameManager gameManager;
    GameObject[] allGameObjects;

    public List<Vector2> Bishop(GameObject obj) {
        allGameObjects = GameObject.FindObjectsOfType<GameObject>();

        int maxLimt = 8;
        List<Vector2> bishopSpots = new List<Vector2>();
        Vector2 bishopTransform = obj.transform.position;

        //Diagonal top right
        for (int i = 1; i <= maxLimt; i++) {
            Vector2 spot = new Vector2(bishopTransform.x + i, bishopTransform.y + i);

            //check if spot is in border
            if (((spot.x < 0 || spot.x > 7) == true || (spot.y < 0 || spot.y > 7) == true)) {
                bishopSpots.Add(spot);
                break;
            }

            //check for blocking pieces
            if (gameManager.LocateChessPieceAt(spot) == true) {
                break;
            }

            bishopSpots.Add(spot);

        }
        //Diagonal top left
        for (int i = 1; i <= maxLimt; i++) {

            Vector2 spot = new Vector2(bishopTransform.x - i, bishopTransform.y + i);

            //check if spot is in border
            if ((spot.x < 0 || spot.x > 7) == true || (spot.y < 0 || spot.y > 7) == true) {
                bishopSpots.Add(spot);
                break;
            }

            //check for blocking pieces
            if (gameManager.LocateChessPieceAt(spot) == true) {
                break;
            }

            bishopSpots.Add(spot);

        }
        //Diagonal bottom left
        for (int i = 1; i <= maxLimt; i++) {

            Vector2 spot = new Vector2(bishopTransform.x - i, bishopTransform.y - i);

            //check if spot is in border
            if ((spot.x < 0 || spot.x > 7) == true || (spot.y < 0 || spot.y > 7) == true) {
                bishopSpots.Add(spot);
                break;
            }

            //check for blocking pieces
            if (gameManager.LocateChessPieceAt(spot) == true) {
                bishopSpots.Add(spot);
                break;
            }

            bishopSpots.Add(spot);

        }
        //Diagonal bottom right
        for (int i = 1; i <= maxLimt; i++) {

            Vector2 spot = new Vector2(bishopTransform.x + i, bishopTransform.y - i);

            //check if spot is in border
            if ((spot.x < 0 || spot.x > 7) == true || (spot.y < 0 || spot.y > 7) == true) {
                bishopSpots.Add(spot);
                break;
            }

            //check for blocking pieces
            if (gameManager.LocateChessPieceAt(spot) == true) {
                break;
            }

            bishopSpots.Add(spot);

        }

        foreach (Vector2 spot1 in bishopSpots) {
            print(spot1.ToString());
            print(bishopSpots[(bishopSpots.Count) - 1] + "Last Index");
        }
        return bishopSpots;
    }

    private List<Vector2> Pawn(GameObject gObj) {

        string pawnTeam = gObj.tag.StartsWith("W") ? "White" : "Black";
        Vector2 pawnPosition = gObj.transform.position;

        List<Vector2> positions = new List<Vector2>();

        if (pawnTeam == "White") {

            int WpawnMoves = pawnPosition.y == 1 ? 3 : 2;
            Vector2 basicWhiteMov = Vector2.up;
            Vector2[] whiteKill = { Vector2.up + Vector2.right, Vector2.up + Vector2.left };

            for (var i = 1; i < WpawnMoves; i++) {

                if (gameManager.LocateChessPieceAt((basicWhiteMov * i) + pawnPosition)) { // detect a piece in front of pawn
                    break;
                }

                positions.Add((basicWhiteMov * i) + pawnPosition);
            }

            foreach (Vector2 killpos in whiteKill) {
                if (gameManager.LocateChessPieceAt(pawnPosition + killpos) && !gameManager.LocateChessPieceAt(pawnPosition + killpos).tag.StartsWith("W")) { // check for kill spots and check if the piece is not the same team
                    positions.Add(pawnPosition + killpos);
                }
            }
        }

        if (pawnTeam == "Black") {

            int BpawnMoves = pawnPosition.y == 6 ? 3 : 2;
            Vector2 basicBlackMov = Vector2.down;
            Vector2[] blackKill = { Vector2.down + Vector2.right, Vector2.down + Vector2.left };

            for (var i = 1; i < BpawnMoves; i++) {

                if (gameManager.LocateChessPieceAt((basicBlackMov * i) + pawnPosition)) {
                    break;
                }

                positions.Add((basicBlackMov * i) + pawnPosition);
            }

            foreach (Vector2 killpos in blackKill) {
                if (gameManager.LocateChessPieceAt(pawnPosition + killpos) && !gameManager.LocateChessPieceAt(pawnPosition + killpos).tag.StartsWith("B")) {
                    positions.Add(pawnPosition + killpos);
                }
            }
        }

        return positions;
    }
}
