using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    #region Field Declarations

    [Header("UI Components")]
    [Space]
    [SerializeField] Button _startButton;
    [SerializeField] Button _quitButton;

    #endregion
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _startButton.onClick.AddListener(HandleStartClicked);
        _quitButton.onClick.AddListener(HandleQuitClicked);
    }

    #region Events clicked
    void HandleQuitClicked()
    {
        GameManager.Instance.QuitGame();
    }
    void HandleStartClicked()
    {
        StartCoroutine(LoadGame());
    }
    IEnumerator LoadGame()
    {
        SceneManager.LoadScene(2);
        yield return new WaitForSeconds(0f);
        GameManager.Instance.StartGame();
        gameObject.SetActive(false);
        HUDController.Instance.ScoreText.gameObject.SetActive(true);
    }

    #endregion
}