using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClockController : MonoBehaviour
{
   private const int SECOND_IN_DAY = 86400;
 
   [SerializeField] private HourHand _hourHand;
   [SerializeField] private MinuteHand _minuteHand;
   [SerializeField] private RectTransform _secondHand;
   [Space] 
   [SerializeField] private TextMeshProUGUI _timeText;
   [Space]
   [SerializeField] private TMP_InputField _hoursField;
   [SerializeField] private TMP_InputField _minutesField;
   [Space]
   [SerializeField] private Button _useTimeEditButton;
   [SerializeField] private Button _confirmTimeButton;

   private TimeData _changeTimeData;
   private AnalogClock _analogClock;
   private KeyboardTimeEdit _keyboardTimeEdit;
   private Coroutine _currentCoroutine;

   private readonly List<ICanChangeTime> _timeChanges = new ();
   
   private void Awake()
   {
      _useTimeEditButton.onClick.AddListener(UseTimeChangeMode);
      _confirmTimeButton.onClick.AddListener(ExitExitTimeChangeModeAndSave);
      
      _analogClock = new AnalogClock(_hourHand, _minuteHand, _secondHand);
      _keyboardTimeEdit = new KeyboardTimeEdit(_hoursField, _minutesField);
      
      _timeChanges.Add(_analogClock);
      _timeChanges.Add(_keyboardTimeEdit);

     foreach (var changer in _timeChanges)
        changer.onTimeChange += ChangeTimeData;
   }

   private void OnDisable()
   {
      foreach (var changer in _timeChanges)
         changer.onTimeChange -= ChangeTimeData;
   }

   private void UseTimeChangeMode()
   {
      StopTimeCourse();
      _useTimeEditButton.gameObject.SetActive(false);
      _confirmTimeButton.gameObject.SetActive(true);
      
      foreach (var changer in _timeChanges)
         changer.StartChangeTime();
   }

   private void ChangeTimeData(TimeData timeData)
   {
      _changeTimeData = timeData;
      _timeText.text = _timeText.text = $"{timeData.hours}:{timeData.minutes}:{timeData.seconds}";
   }


   private void ExitExitTimeChangeModeAndSave()
   {
      StartClock(_changeTimeData);
      ExitTimeChangeMode();
   }
   
   private void ExitTimeChangeMode()
   {
      _useTimeEditButton.gameObject.SetActive(true);
      _confirmTimeButton.gameObject.SetActive(false);
      
      foreach (var changer in _timeChanges)
         changer.StopChangeTime();
      
      _changeTimeData = new TimeData(0, 0, 0);
   }
   
   private void StopTimeCourse()
   {
      StopCoroutine(_currentCoroutine);
   }

   public void StartClock(DateTime dateTime)
   {
      if (_currentCoroutine != null)
      {
         StopTimeCourse();
         ExitTimeChangeMode();
      }
      
      float seconds = dateTime.Hour * 3600 + dateTime.Minute * 60 + dateTime.Second;
      _currentCoroutine = StartCoroutine(StartClockOperation(seconds));
   }
   
   private void StartClock(TimeData timeData)
   {
      float seconds = timeData.hours * 3600 + timeData.minutes * 60 + timeData.seconds;
      _currentCoroutine = StartCoroutine(StartClockOperation(seconds));
   }

   private IEnumerator StartClockOperation(float currentSeconds)
   {
      while (true)
      {
         currentSeconds++;

         if (currentSeconds > SECOND_IN_DAY)
            currentSeconds = 0;

         var timeSpan = TimeSpan.FromSeconds(currentSeconds);
         int hours = (int)timeSpan.TotalHours;
         int minutes = timeSpan.Minutes % 60;
         int seconds = (int)(timeSpan.Seconds + timeSpan.Milliseconds / 1000f);

         TimeData currentTimeData = new TimeData(hours, minutes, seconds);

         _analogClock.SetTime(currentTimeData);
         _timeText.text = $"{hours}:{minutes}:{seconds}";
         
         var secondCounter = 0f;
         while (secondCounter < 1)
         {
            secondCounter += Time.deltaTime;
            yield return null;
         }
      }
   }
}