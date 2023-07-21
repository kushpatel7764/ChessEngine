using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamemodeSelected : MonoBehaviour
{
    public Button aiButton;

    void Start()
    {
        aiButton.onClick.AddListener(Clicked);
    }

    private void Clicked()
    {
        SceneManager.LoadScene(3);
    }

}
