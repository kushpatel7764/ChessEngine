using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoard : MonoBehaviour
{
    public GameObject chessSpot;

    public GameObject b_Horse;
    public GameObject b_Rook;
    public GameObject b_Queen;
    public GameObject b_King;
    public GameObject b_Pawn;
    public GameObject b_Bishop;

    public GameObject w_Horse;
    public GameObject w_Rook;
    public GameObject w_Queen;
    public GameObject w_King;
    public GameObject w_Pawn;
    public GameObject w_Bishop;
        
    int heightPiece = 1;
    int widthPiece = 1; 
    int heightBoard = 1*8;
    int widthBoard = 1*8;
    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < heightBoard; i = i + heightPiece ){
            for (int k = 0; k < widthBoard; k = k + widthPiece ){
                
                if (i % 2 == 0){
                    if (k % 2 == 0){
                      chessSpot.GetComponent<SpriteRenderer>().color = Color.white;

                    }else{
                      chessSpot.GetComponent<SpriteRenderer>().color = Color.gray;
                    }
                } else{
                    if (k % 2 == 0){
                      chessSpot.GetComponent<SpriteRenderer>().color = Color.gray;
                    }else{
                      chessSpot.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }

                Instantiate(chessSpot, new Vector2(k,i), Quaternion.identity, this.transform);
            }
        }

        Instantiate(w_Rook, new Vector2(0, 0), Quaternion.identity, this.transform);
        Instantiate(w_Horse, new Vector2(1, 0), Quaternion.identity, this.transform);
        Instantiate(w_Bishop, new Vector2(2, 0), Quaternion.identity, this.transform);
        Instantiate(w_Queen, new Vector2(3, 0), Quaternion.identity, this.transform);
        Instantiate(w_King, new Vector2(4, 0), Quaternion.identity, this.transform);
        var wBishop2 = Instantiate(w_Bishop, new Vector2(5, 0), Quaternion.identity, this.transform);
        var wHorse2 = Instantiate(w_Horse, new Vector2(6, 0), Quaternion.identity, this.transform);
        var wRooke2 = Instantiate(w_Rook, new Vector2(7, 0), Quaternion.identity, this.transform);

        wBishop2.name = "WBishop2";
        wHorse2.name = "WHorse2";
        wRooke2.name = "WRooke2";

        Instantiate(b_Rook, new Vector2(0, 7), Quaternion.identity, this.transform);
        Instantiate(b_Horse, new Vector2(1, 7), Quaternion.identity, this.transform);
        Instantiate(b_Bishop, new Vector2(2, 7), Quaternion.identity, this.transform);
        Instantiate(b_Queen, new Vector2(3, 7), Quaternion.identity, this.transform);
        Instantiate(b_King, new Vector2(4, 7), Quaternion.identity, this.transform);
        var Bbishop2 = Instantiate(b_Bishop, new Vector2(5, 7), Quaternion.identity, this.transform);
        var Bhorse2 = Instantiate(b_Horse, new Vector2(6, 7), Quaternion.identity, this.transform);
        var bRook2 = Instantiate(b_Rook, new Vector2(7, 7), Quaternion.identity, this.transform);

        Bbishop2.name = "BBishop2";
        Bhorse2.name = "Bhorse2";
        bRook2.name = "BRook2";

        for (var i = 0; i < 8; i++) {
            var newWPawn = Instantiate(w_Pawn, new Vector2(i, 1), Quaternion.identity, this.transform);
            var newBPawn = Instantiate(b_Pawn, new Vector2(i,6), Quaternion.identity, this.transform);

            newBPawn.AddComponent<CircleCollider2D>();
            newWPawn.AddComponent<CircleCollider2D>();

            newBPawn.name = "BPawn" + i;
            newWPawn.name = "WPawn" + i;
        }
    
    }
}
