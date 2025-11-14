using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerScript : MonoBehaviour
{
    public Slider timerSlider;
    private float remainingTime;
    public void ResetTimer()
    {
        remainingTime = GameManager.Instance.TimetoAct;
        timerSlider.maxValue = remainingTime;
        timerSlider.value = remainingTime;
    }

    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        if (timerSlider.value > 0f)
        {
            timerSlider.value -= Time.deltaTime;
        }
        else
        {
            enabled = false;
        }

    }
}