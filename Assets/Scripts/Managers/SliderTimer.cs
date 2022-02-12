using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTimer : MonoBehaviour
{
    public Slider timeSlider;
    public float gameTime;

    private bool stopTimer;
    // Start is called before the first frame update
    void Start()
    {
        stopTimer = false;
        timeSlider.maxValue = gameTime;
        timeSlider.value = gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        float time = gameTime - Time.time;

        int mins = Mathf.FloorToInt(time / 60);
        int sec = Mathf.FloorToInt(time - mins * 60f);

        if (time <= 0)
        {
            stopTimer = true;
        }
        if (stopTimer == false)
        {
            timeSlider.value = time;
        }
    }
}
