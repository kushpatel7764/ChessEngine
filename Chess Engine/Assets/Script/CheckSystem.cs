using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSystem : MonoBehaviour
{
    GameManager gameManager;

    private GameObject GetKing() {
        return null;
    }

    void IsInCheck() {
        GameObject[] greenSpots = GameObject.FindGameObjectsWithTag("GreenSpot");

    }
}

/*
 * After a piece has moved. Make that piece the selected object and check if it has given a check
 */
