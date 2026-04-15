using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Threading;

public class EventCaller : MonoBehaviour
{
    [Header("Events List")]
    [SerializeField] private List<UnityEvent> eventList;
    [Header("Events Value")]
    [SerializeField] private int timer;
    private int quantityOfEvents;
    private float cronometer;
    private int randomIndex;
    void Start()
    {
        setUpValues();
    }
    void Update()
    {
        cronometer += Time.deltaTime;
        if (cronometer >= timer)
        {
            randomIndex = Random.Range(0, quantityOfEvents);
            eventList[randomIndex].Invoke();
            cronometer = 0; 
        }
    }

    void setUpValues()
    {
        quantityOfEvents = eventList.Count;
        cronometer = 0;
    }
}
