using TMPro;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Spawner _spawner;

    private int _score;

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        _score = 0;
        _scoreText.text = _score.ToString();
    }

    public void AddScore(int amount)
    {
        _score += amount;
        _scoreText.text = _score.ToString();
    }
}
