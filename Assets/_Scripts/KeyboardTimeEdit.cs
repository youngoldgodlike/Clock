using System;
using _Scripts;
using TMPro;

public class KeyboardTimeEdit : ICanChangeTime
{
    private TMP_InputField _hoursField;
    private TMP_InputField _minutesField;

    public event Action<TimeData> onTimeChange;

    public KeyboardTimeEdit(TMP_InputField hoursField, TMP_InputField minutesField)
    {
        _hoursField = hoursField;
        _minutesField = minutesField;
        
        _hoursField.onValueChanged.AddListener(value =>
        {
            var hoursValue = Math.Clamp(Convert.ToInt32(value), 0, 11);
            _hoursField.text = hoursValue.ToString();
            ChangeTime();
        });
        
        _minutesField.onValueChanged.AddListener(value =>
        {
            var minutesValue = Math.Clamp(Convert.ToInt32(value), 0, 59);
            _minutesField.text = minutesValue.ToString();
            ChangeTime();
        });
    }

    public void StartChangeTime()
    {
        _hoursField.text = "00";
        _minutesField.text = "00";
        
        _hoursField.gameObject.SetActive(true);
        _minutesField.gameObject.SetActive(true);
    }

    public void StopChangeTime()
    {
        _hoursField.gameObject.SetActive(false);
        _minutesField.gameObject.SetActive(false);
    }

    private void ChangeTime()
    {
        var hours = Convert.ToInt32(_hoursField.text);
        var minutes = Convert.ToInt32(_minutesField.text);
        
        TimeData timeData = new TimeData(hours, minutes, 0);
        onTimeChange?.Invoke(timeData);
    }
}
