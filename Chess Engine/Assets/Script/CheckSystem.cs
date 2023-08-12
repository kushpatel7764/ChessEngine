using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSystem : MonoBehaviour
{
    GameManager gameManager;

    void Update(){
        IsInCheck();
    }

    private GameObject GetKing() {
       //find out which king is to be checked by the check system
       GameObject wKing = GameObject.FindGameObjectWithTag("WhiteKing");
       GameObject bKing = GameObject.FindGameObjectWithTag("BlackKing");
        //Make better Shiv!
       if (wKing.GetComponent<CircleCollider2D>().enabled == true){
        return wKing;
       } else{
        return bKing; 
       }

    }

    void IsInCheck(){
        Vector3 kingPos = GetKing().transform.position;

        if (GetKing().tag[0] == 'W'){
        //I have white king 
            foreach(GameObject gObj in GameObject.FindObjectsOfType<GameObject>()){
                //get only black chess pieces
                if (gObj.tag[0] == 'B'){
                    //get all the positions that the black piece can goto
                    print("Error entering function");
                    List<Vector3> blackPieceSpots = LegalPieceMoves(gObj);
                    //compare the black piece spots with the king's spot to see if he is in check or not
                    foreach (Vector3 spot in blackPieceSpots){
                        if (spot == kingPos){
                            print("Check");
                        }
                       
                    }
                }

            }
        } else{
        //I have black king 

        }
    }

  


    List<Vector3> LegalPieceMoves(GameObject piece){
      print("Error with initiation");
      List<Vector3> toReturnSpots = new List<Vector3>();
      gameManager.selectedGameObject = piece; 
      print("Error with spotsArray");
      GameObject[] spotsArray = GameObject.FindGameObjectsWithTag("GreenSpot");
      if (spotsArray[0] == null){
        print("Spots Array is empty.");
      }else{
      foreach (GameObject spot in spotsArray){
        toReturnSpots.Add(spot.transform.position);
      }
      }

     return toReturnSpots;
    }
}

/*
 * After a piece has moved. Make that piece the selected object and check if it has given a check
 */
