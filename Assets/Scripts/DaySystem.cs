using UnityEngine;

public class DaySystem : MonoBehaviour
{
    [SerializeField] int day = 1;
    [SerializeField] int recordDay = 1;

    public int Day { get { return day; } }
    public int RecordDay { get { return recordDay; } }
    void Start()
    {
        LoadRecord();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextDay()
    {
        day++;
        if (day >= recordDay)
        {
            recordDay = day;
            SaveRecord();
        }
    }
    public void SaveRecord() => PlayerPrefs.SetInt("RecordDay", recordDay);
    public void LoadRecord() => recordDay = PlayerPrefs.GetInt("RecordDay");

}
