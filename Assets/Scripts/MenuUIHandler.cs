using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public GameObject leaderBoard;
    public TMP_InputField nameInput;
    public TMP_Text bestText;
    public void StartGame()
    {
        if (nameInput.text != "")
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            nameInput.ActivateInputField();
        }
    }

    public void ShowLB()
    {
        TMP_Text text = leaderBoard.GetComponentInChildren<TMP_Text>();
        GameManager.Instance.ReturnAllBest(ref text);
        leaderBoard.SetActive(!leaderBoard.activeSelf);
    }

    public void ExitGame()
    {

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void GetName()
    {   if (nameInput.text != "")
        {
            GameManager.Instance.playerName = nameInput.text;
            bestText.text = "Best: " + GameManager.Instance.CheckBest();
        }
    }
}
