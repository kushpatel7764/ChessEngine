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
        Vector3 kingPos = GetKing().transform.position;
        
        //if (GetKing().tag[0] == 'W'){
        //I have white king 
            //foreach(GameObject gObj in GameObject.FindObjectsOfType<GameObject>()){
                //get only black chess pieces
               // if (gObj.tag[0] == 'B'){
                  //  print(gObj.name + "-----------");
                    //get all the positions that the black piece can goto
                    List<Vector2> blackPieceSpots = test.Bishop(GameObject.FindGameObjectWithTag("BlackBishop"));
                    //compare the black piece spots with the king's spot to see if he is in check or not
                    foreach (Vector3 spot in blackPieceSpots){
                        //print("(" + spot.x + "," + spot.y + ")" + ":" + "(" + kingPos.x + "," + kingPos.y + ")");
                        if (spot == kingPos){
                            
                            print("Check");
                        }
                       
                    }
               // }

            }
       // } else{
        //I have black king 

       // }
    }
   

/*
 * After a piece has moved. Make that piece the selected object and check if it has given a check
 */
