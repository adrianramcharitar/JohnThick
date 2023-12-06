using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    // script to control time

    private float fixedDeltaTime;
    // public float slowDownTimer;
    
    public Text timerText;
    
    // Start is called before the first frame update
    void Awake()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
       // timerText.text = "Timer: " + slowDownTimer;
        if (Input.GetKeyDown("z"))
           // this.slowDownTimer = 5;
        {
            if (Time.timeScale == 1.0f)
            {
                Time.timeScale = 0.3f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
            Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
            // slowDownTimer -= Time.deltaTime;
        }
    }
}
