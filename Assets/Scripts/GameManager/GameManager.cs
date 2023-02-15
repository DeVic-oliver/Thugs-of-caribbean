using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Core.Components.Counters;
using Assets.Scripts.Player;
[RequireComponent(typeof(TimerCounter))]
public class GameManager : MonoBehaviour
{
    private TimerCounter _timer;
    [SerializeField] private PlayerHealth _playerHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        _timer = GetComponent<TimerCounter>();
        _timer.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
