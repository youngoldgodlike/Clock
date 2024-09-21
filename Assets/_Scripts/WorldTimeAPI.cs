using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WorldTimeAPI : MonoBehaviour
{
    [SerializeField] private ClockController _clockController;
    [SerializeField] private float _updateRequestDelay;
    [SerializeField] private string API_URL_1 = "https://www.timeapi.io/";
    
     private void Start()
     {
         StartCoroutine(SendRequest());
     }
     
     private IEnumerator SendRequest()
     {
         var request = UnityWebRequest.Get(API_URL_1);
         yield return request.SendWebRequest();
         
         if (request.result == UnityWebRequest.Result.Success)
         {
             var jsonResponse = request.downloadHandler.text;
             
             TimeDateData timeDateData = JsonUtility.FromJson<TimeDateData>(jsonResponse);
             DateTime dateTime = DateTime.Parse(timeDateData.dateTime);
             
             _clockController.StartClock(dateTime);
         }
         
         StartCoroutine(UpdatingRequest());
     }

     private IEnumerator UpdatingRequest()
     {
         yield return new WaitForSeconds(_updateRequestDelay);
         StartCoroutine(SendRequest());
     }
}

[Serializable]
public struct TimeDateData
{
    public string dateTime;
}