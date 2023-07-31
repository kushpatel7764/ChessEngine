using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using TMPro;
using UnityEngine;

public class Pawn_Placement : MonoBehaviour
{
    public GameObject greenTiles;
    private string selectedPawnName;

    GameManager gameManager;
    [SerializeField] GameObject gameManagerObject;

    private void Start() {
        
    }

    private void Update() {
        gameManagerObject = GameObject.Find("GameManager");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (Input.GetMouseButtonDown(0) && gameManager.selectedObject == true && selectedPawnName != null) {
            if (hit.collider != null && hit.collider.tag == "ChessSpot") {
                Vector2 piecePosition = GameObject.Find(selectedPawnName).transform.position;
                Vector2 chessSpotPosition = hit.collider.transform.position;

                print(GameObject.Find(selectedPawnName));
                print(piecePosition);
                print(chessSpotPosition);
            }
        }
    }

    private void OnMouseDown() {

        var clickedObject = gameObject;

        if (GameObject.Find("GreenSpot(Clone)")) {
            Destroy(GameObject.Find("GreenSpot(Clone)"));
            Instantiate(greenTiles, clickedObject.transform.position, Quaternion.identity);
            gameManager.selectedObject = true;
            selectedPawnName = gameObject.name;
        } else {
            Instantiate(greenTiles, clickedObject.transform.position, Quaternion.identity);
            gameManager.selectedObject = true;
            selectedPawnName = gameObject.name;
        }
    }
}
