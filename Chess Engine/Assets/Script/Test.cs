using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Test : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    GameObject[] allGameObjects;

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
