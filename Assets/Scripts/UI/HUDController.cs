using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDController : Singleton<HUDController>
{
    #region Field Declarations

    [Header("HUD Components")]
    [Space]
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _statusText;
    public TextMeshProUGUI ScoreText { get => _scoreText; private set => _scoreText = value; }

    #endregion
    void Start()
    {
        ScoreText.text = "Score: " + 0.ToString("D5");
        ScoreText.gameObject.SetActive(false);
        DontDestroyOnLoad(gameObject);
        _statusText.GetComponent<CanvasRenderer>().SetAlpha(0);
        _statusText.gameObject.SetActive(false);
    }
    public void UpdateScore(int score)
    {
        ScoreText.text = "Score: " + score.ToString("D5");
    }

    #region Show status
    public void ShowStatus(string newStatus)
    {
        _statusText.gameObject.SetActive(true);
        StartCoroutine(ChangeStatus(newStatus));
    }

    IEnumerator ChangeStatus(string displayText)
    {
        _statusText.text = displayText;
        ScoreText.rectTransform.anchoredPosition = new Vector3(-960f, -588f, 0);
        _statusText.CrossFadeAlpha(1f, 1f, false);
        yield return new WaitForSeconds(1.01f);
        _statusText.CrossFadeAlpha(0, 1f, false);
        yield return new WaitForSeconds(1.01f);
        UpdateScore(0);
        ScoreText.rectTransform.anchoredPosition = new Vector3(-208f, -52f, 0);
        ScoreText.gameObject.SetActive(false);
        SceneManager.LoadScene(1);
        UIManager.Instance.gameObject.SetActive(true);
        yield break;
    }

    #endregion
}
