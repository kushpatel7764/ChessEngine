using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSystem : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    public Test test; 

    private GameObject GetKing() {
       //find out which king is to be checked by the check system
       GameObject wKing = GameObject.FindGameObjectWithTag("WhiteKing");
       GameObject bKing = GameObject.FindGameObjectWithTag("BlackKing");
        //Make better Shiv!
        if (wKing.GetComponent<CircleCollider2D>().enabled == true && bKing.GetComponent<CircleCollider2D>().enabled == true){
            return null; 
        } else {
        if (wKing.GetComponent<CircleCollider2D>().enabled == true){
        return wKing;
       } else{
        return bKing; 
       }
        }
       

    }

    public void IsInCheck(){
        if (GetKing() == null){
            return; 
        }
        Vector2 kingPos = GetKing().transform.position;
        
        if (GetKing().tag[0] == 'W'){
        //I have white king 
            //foreach(GameObject gObj in GameObject.FindObjectsOfType<GameObject>()){
                //get only black chess pieces
                //if (gObj.tag == "BlackBishop"){
                    //print(gObj.name + "-----------");
                    //get all the positions that the black piece can goto
                    //List<Vector2> blackPieceSpots = gameManager.ReturnNextMoves(gObj);

                    //Testing Bishop
                    List<Vector2> bishopSpots = test.Bishop(GameObject.Find("B_Bishop(Clone)")); 

                    //compare the black piece spots with the king's spot to see if he is in check or not
                    foreach (Vector2 spot in bishopSpots){
                      //  print("(" + spot.x + "," + spot.y + ")" + ":" + "(" + kingPos.x + "," + kingPos.y + ")");
                        if (spot == kingPos){
                            
                            print("Check!!!!");
                        }
                       
                    }
                //}

           // }
        } else{
        //I have black king 

        }
    }
   



    List<Vector3> LegalPieceMoves(GameObject piece){
        GameObject tempStore = gameManager.selectedGameObject;
      List<Vector3> toReturnSpots = new List<Vector3>();
      
      //Error in line below 
      gameManager.selectedGameObject = piece; 
      
      GameObject[] spotsArray = GameObject.FindGameObjectsWithTag("GreenSpot");
      
      foreach (GameObject spot in spotsArray){
        toReturnSpots.Add(spot.transform.position);
      
      }
        gameManager.selectedGameObject = tempStore;
     return toReturnSpots;
    }
}

/*
 * After a piece has moved. Make that piece the selected object and check if it has given a check
 */
