using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoard : MonoBehaviour
{
    public GameObject chessSpot;
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
                      chessSpot.GetComponent<SpriteRenderer>().color = Color.black;
                    }
                } else{
                    if (k % 2 == 0){
                      chessSpot.GetComponent<SpriteRenderer>().color = Color.black;
                    }else{
                      chessSpot.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }
                
               
                Instantiate(chessSpot, new Vector2(k,i), Quaternion.identity, this.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
