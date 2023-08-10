using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSystem : MonoBehaviour {
    public GameManager gameManager;
    GameObject[] allGameObjects;
    public string currentTurn;

    public GameObject GetKing(string type) {
        switch (type) {
            case "Black": return GameObject.FindGameObjectWithTag("BlackKing");
            case "White": return GameObject.FindGameObjectWithTag("WhiteKing");
        }

        return null;
    }

    public List<GameObject> GetTeamObjects() {
        string enemyTag = currentTurn == "B" ? "W"
                                             : "B";

        foreach (GameObject obj in allGameObjects) {
            if (currentTurn != null) {

            }
        }

        return null;
    }

    private void Update() {
        allGameObjects = GameObject.FindObjectsOfType<GameObject>();
    }
}

/*
 * After a piece has moved. Make that piece the selected object and check if it has given a check
 */
