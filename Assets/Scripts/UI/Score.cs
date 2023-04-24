using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public delegate void LevelUpDelegate(int level);
    public event LevelUpDelegate OnLevelUp;
    public delegate void FillProgressBar();
    public event FillProgressBar OnProgressBarFilled;
    

    public int Highscore => _highscore;

    [SerializeField]
    private Slider _progressBar;
    
    [SerializeField]
    private TextMeshProUGUI _highScoreText;

    [SerializeField]
    private TextMeshProUGUI _scoreText;

    [SerializeField]
    private Image _tropheyImage;

    [SerializeField]
    private int _baseScoreToNextLevel = 20;

    [SerializeField]
    private int _maxDifficultyLevel = 5;

    private int _difficultyLevel = 1;
    private int _highscore = 0;
    private float _score;
    
    private bool _isHighscoreFilled = false;
    private int _scoreToNextLevel;
    
    public void Init(int highscore)
    {
        _highscore = highscore;
        _score = 0;
        _difficultyLevel = 1;
        _progressBar.value = 0;
        _isHighscoreFilled = false;
        
        DisplayHighScore(highscore);
        DisplayScore((int)_score);

    }

    public void StartGame()
    {
        _scoreToNextLevel = _baseScoreToNextLevel;
        _tropheyImage.transform.localScale = new Vector3(1,1,1);
        _isHighscoreFilled = false;
        _progressBar.value = 0;
        _score = 0;
        DisplayScore((int)_score);
        _difficultyLevel = 1;
    }

    private void CalculateProgress()
    {
        if (_isHighscoreFilled)
            return;
        
        var ratio = _score / _highscore;
        if (ratio <= 1f)
        {
            _progressBar.value = ratio;
        }
        else
        {
            _isHighscoreFilled = true;
            _tropheyImage.transform.localScale = new Vector3(2,2,1);
            OnProgressBarFilled?.Invoke();
        }
    }
    
    private void Update()
    {
        if (_score > _scoreToNextLevel)
        {
            LevelUp();
        }

        _score += Time.deltaTime;

        CalculateProgress();

        DisplayScore((int) _score);
    }

    public void OnPlayerDied()
    {
        _highscore = (int)_score;
        DisplayHighScore(_highscore);
    }

    private void DisplayHighScore(int highscore)
    {
        _highScoreText.text = highscore.ToString();
    }
    
    private void DisplayScore(int score)
    {
        _scoreText.text = score.ToString();
    }

    private void LevelUp()
    {
        if (_difficultyLevel == _maxDifficultyLevel)
            return;
        
        _scoreToNextLevel *= 2;
        _difficultyLevel++;
        OnLevelUp?.Invoke(_difficultyLevel);
    }
}
