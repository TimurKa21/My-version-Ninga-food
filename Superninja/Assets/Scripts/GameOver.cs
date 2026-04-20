using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using YG;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Animator _gameOverPanelAnimation;
    [SerializeField] private Slider _adsTimerSlider;
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Spawner _spawner;

    [SerializeField] private TMP_Text _totalScore;
    [SerializeField] private TMP_Text _gameScore;

    private float _adsTimerDuration = 5f;
    private string _animatorTriggerName = "in";

    // Подписываемся на событие открытия рекламы в OnEnable
    private void OnEnable() => YandexGame.RewardVideoEvent += AdReward;

    // Отписываемся от события открытия рекламы в OnDisable
    private void OnDisable() => YandexGame.RewardVideoEvent -= AdReward;


    public void ShowGameOverAnimation()
    {
        _gameOverPanel.SetActive(true);
        _gameOverPanelAnimation.SetTrigger(_animatorTriggerName);
        _endPanel.SetActive(false);
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        _adsTimerDuration = 5f;
        float elapsedTime = 0f;
        _adsTimerSlider.value = 1f;

        while (elapsedTime < _adsTimerDuration)
        {
            elapsedTime += Time.deltaTime;
            _adsTimerSlider.value = Mathf.Lerp(1f, 0f, elapsedTime/_adsTimerDuration);
            yield return null;
        }

        _adsTimerSlider.value = 0f;
        ActivateGameOverPanel();
    }

    private void ActivateGameOverPanel()
    {
        _endPanel.SetActive(true);
        _totalScore.text = "счет " + _gameScore.text;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowAd(int id)
    {
        YandexGame.RewVideoShow(0);
    }

    public void AdReward(int id)
    {
        _gameOverPanel.SetActive(false);
        _spawner.enabled = true;
        StopAllCoroutines();
    }
}