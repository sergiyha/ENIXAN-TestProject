using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour {


    public RectTransform statusBarTransform;
    private float maxBarPosition;
    private float currentStatusBarPosition;
    private float minBarPosition;
    public float timerSize;
    private float currentTimerValue;
    public Text statusBarLevel;
    private int level;



    void Start()
    {
        maxBarPosition = statusBarTransform.anchoredPosition.x;
        minBarPosition = statusBarTransform.anchoredPosition.x - statusBarTransform.rect.width;
        currentTimerValue = 0.0f;
        timerSize = 60.0f;
        level = 0;
        statusBarLevel.text = level.ToString();


    }

    // Update is called once per frame
    void Update()
    {

        currentStatusBarPosition = SetStatusBarPositionAccordingToTimerValue(CurrentTimerValue(), 0.0f, timerSize, minBarPosition, maxBarPosition);
        statusBarTransform.anchoredPosition = new Vector2(currentStatusBarPosition, statusBarTransform.anchoredPosition.y);


    }

    private float CurrentTimerValue()
    {
        if (currentTimerValue > timerSize)
        {
            currentTimerValue = 0.0f;
            level += 1;
            statusBarLevel.text = level.ToString();
        }
        currentTimerValue += Time.deltaTime;
        return currentTimerValue;
    }

    private float SetStatusBarPositionAccordingToTimerValue(float currentTimerValue, float minTimer, float maxTimer, float minBarPos, float maxBarPos)
    {
        return (currentTimerValue - minTimer) * (maxBarPos - minBarPos) / (maxTimer - minTimer) + minBarPosition;
    }




}

