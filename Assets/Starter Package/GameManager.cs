using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private TMP_Text timeCount;
    [SerializeField]
    private GameObject DrivingSurfaceManager;
    [SerializeField]
    private TMP_Text remainingTimeText;
    [SerializeField]
    private int gameDuration = 120;
    [SerializeField]
    private GameObject scoreBoard;
    [SerializeField]
    private CarManager carManager;
    [SerializeField]
    private TMP_Text scoreText;


    private float remainingTime;
    private int intRemainingTime;
    private bool onGame;
    public int score = 0;
    private UnityEvent Launch;

    public static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            scoreText.text = "0";
            remainingTime = gameDuration;
            intRemainingTime = gameDuration;
            remainingTimeText.text = $"{intRemainingTime / 60}:{intRemainingTime % 60}";
            Launch = new();
            Launch.AddListener(StartGame);
        }
        else Destroy(this.gameObject);
    }

    public void LaunchGame()
    {
        startButton.gameObject.SetActive(false);
        StartCoroutine(CountDown(3));
    }

    private IEnumerator CountDown(int time)
    {
        timeCount.gameObject.SetActive(true);
        while (time > 1)
        {
            timeCount.text = time.ToString();
            yield return new WaitForSeconds(1);
            time--;
        }
        timeCount.text = 0.ToString();

        Launch.Invoke();    
    }

    private void StartGame()
    {
        timeCount.gameObject.SetActive(false);
        DrivingSurfaceManager.SetActive(true);
        carManager.SpawnCar();
        onGame = true;
    }

    public void SetupCar()
    {
        carManager.Car.Hit.AddListener(OnCarHit);
    }

    // Update is called once per frame
    void Update()
    {
        if (onGame)
        {
            if(intRemainingTime == 0)
            {
                onGame = false;
                ShowScore();
                return;
            }
            remainingTime -= Time.deltaTime;
            intRemainingTime = (int)remainingTime;
            remainingTimeText.text = $"{intRemainingTime/60}:{intRemainingTime%60}";
        }
    }

    public void OnCarHit()
    {
        score += 1;
        scoreText.text = score.ToString();
    }

    private void ShowScore()
    {
        DrivingSurfaceManager.SetActive(false);
        scoreBoard.SetActive(true);
    }
}
