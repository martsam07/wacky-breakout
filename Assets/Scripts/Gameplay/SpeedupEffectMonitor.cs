﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Monitors the speedup effect
/// </summary>
public class SpeedupEffectMonitor : MonoBehaviour
{
    // speedup effect support
    Timer speedupEffectTimer;
    float speedupValue;

    /// <summary>
    /// Gets whether or not the speedup effect is active
    /// </summary>
    /// <value><c>true</c> if speedup effect active; otherwise, <c>false</c>.</value>
    public bool SpeedupEffectActive
    {
        get { return speedupEffectTimer.Running; }
    }
        
    /// <summary>
    /// Gets how many seconds are left in the speedup effect
    /// </summary>
    /// <value>speedup effect seconds left</value>
    public float SpeedupEffectSecondsLeft
    {
        get { return speedupEffectTimer.SecondsLeft; }
    }

    /// <summary>
    /// Gets the speedup factor for the speedup effect
    /// </summary>
    /// <value>speedup factor</value>
    public float SpeedupValue
    {
        get { return speedupValue; }
    }

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
        speedupEffectTimer = gameObject.AddComponent<Timer>();
        speedupEffectTimer.AddTimerFinishedListener(HandleSpeedupEffectTimerFinished);
        EventManager.AddSpeedupEffectListener(HandleSpeedupEffectActivatedEvent);
	}

    /// <summary>
    /// Handles the speedup effect activated event
    /// </summary>
    /// <param name="duration">duration of the speedup effect</param>
    /// <param name="speedupFactor">the speedup value</param>
    void HandleSpeedupEffectActivatedEvent(float duration, float speedupValue)
    {
        // run or add time to timer
        if (!speedupEffectTimer.Running)
        {
            this.speedupValue = speedupValue;
            speedupEffectTimer.Duration = duration;
            speedupEffectTimer.Run();
        }
        else
        {
            speedupEffectTimer.AddTime(duration);
        }
    }

    /// <summary>
    /// Handle the speedup timer finishing
    /// </summary>
    void HandleSpeedupEffectTimerFinished()
    {
        AudioManager.Play(AudioClipName.Effect);
        speedupEffectTimer.Stop();
        speedupValue = 1;
    }
}
