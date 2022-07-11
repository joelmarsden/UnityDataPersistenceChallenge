
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{

    public TextMeshProUGUI BestScoreText;
    public TextMeshProUGUI UserNameText;

    private void Start()
    {
        BestScoreText.text = PersistenceManager.Instance.getBestScoreText();
    }

    public void StartGame()
    {
        Debug.Log("Starting game with: " + UserNameText.text);
        if (UserNameText.text.Trim().Length > 0)
        {
            PersistenceManager.Instance.setCurrentPlayer(UserNameText.text.Trim());
            SceneManager.LoadScene(1);
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
