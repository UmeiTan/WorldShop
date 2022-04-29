using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarController : MonoBehaviour
{
    public static event Action<DayConfig> OnDayChanged;

    [SerializeField] private List<DayConfig> Week;

    private int _currentDay;//save
    //current day state
    private int _clientsKickedOut;//save
    private int _clientsServed;//save

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
}