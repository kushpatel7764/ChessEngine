using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameManager gameManager;
    public List<Vector2> Bishop(GameObject obj)
    {
        
        int maxLimt = 8;
        List<Vector2> bishopSpots = new List<Vector2>(); 
        Vector2 bishopTransform = obj.transform.position;
       
        //Diagonal top right
        for (int i = 1; i <= maxLimt; i++)
        {
            Vector2 spot = new Vector2(bishopTransform.x+i, bishopTransform.y+i);

            //check if spot is in border
            if (((spot.x < 0 || spot.x > 7) == true || (spot.y < 0 || spot.y > 7) == true) )
            {
                bishopSpots.Add(spot);
                break; 
            }

            //check for blocking pieces
            if (gameManager.LocateChessPieceAt(spot) == true )
            {
                break;
            }

            bishopSpots.Add(spot);
            
        }
        //Diagonal top left
        for (int i = 1; i <= maxLimt; i++)
        {
            
            Vector2 spot = new Vector2(bishopTransform.x - i, bishopTransform.y + i);

            //check if spot is in border
            if ((spot.x < 0 || spot.x > 7) == true || (spot.y < 0 || spot.y > 7) == true)
            {
                bishopSpots.Add(spot);
                break;
            }

            //check for blocking pieces
            if (gameManager.LocateChessPieceAt(spot) == true)
            {
                break;
            }

            bishopSpots.Add(spot);

        }
        //Diagonal bottom left
        for (int i = 1; i <= maxLimt; i++)
        {
         
            Vector2 spot = new Vector2(bishopTransform.x - i, bishopTransform.y - i);

            //check if spot is in border
            if ((spot.x < 0 || spot.x > 7) == true || (spot.y < 0 || spot.y > 7) == true)
            {
                bishopSpots.Add(spot);
                break;
            }

            //check for blocking pieces
            if (gameManager.LocateChessPieceAt(spot) == true)
            {
                bishopSpots.Add(spot);
                break;
            }

            bishopSpots.Add(spot);

        }
        //Diagonal bottom right
        for (int i = 1; i <= maxLimt; i++)
        {
            
            Vector2 spot = new Vector2(bishopTransform.x + i, bishopTransform.y - i);

            //check if spot is in border
            if ((spot.x < 0 || spot.x > 7) == true || (spot.y < 0 || spot.y > 7) == true)
            {
                bishopSpots.Add(spot);
                break;
            }

            //check for blocking pieces
            if (gameManager.LocateChessPieceAt(spot) == true)
            {
                break;
            }

            bishopSpots.Add(spot);

        }
        
        foreach (Vector2 spot1 in bishopSpots)
        {
            print(spot1.ToString());
            print(bishopSpots[(bishopSpots.Count)-1] + "Last Index");
        } 
        return bishopSpots; 
    }

 
}
