using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class MobilePushNotification : MonoBehaviour
{
    public static MobilePushNotification Instance;


    public List<AndroidNotification> notifications = new List<AndroidNotification>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    AndroidNotificationChannel taskCompleteChannel = new AndroidNotificationChannel()
    {
        Id = "channel_taskComplete",
        Name = "Task Complete",
        Importance = Importance.Default,
        Description = "Notifications for timed task that have been completed",
    };

    // Start is called before the first frame update
    void Start()
    {
        AndroidNotificationCenter.RegisterNotificationChannel(taskCompleteChannel);
     //   sendNotification("Test", "this is a test");
     //   sendNotification("Test", "this is a test", 15);
    //    sendNotification("Test", "this is a test", 60);
        AndroidNotificationCenter.CancelAllScheduledNotifications();

    }


    public void sendNotificationNow(string title, string text, float firetimeSeconds = 0)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.FireTime = System.DateTime.Now.AddSeconds(firetimeSeconds + 1);
        notifications.Add(notification);
        AndroidNotificationCenter.SendNotification(notification, "channel_taskComplete");
    }
    public AndroidNotification sendNotification(string title, string text, float firetimeSeconds = 0)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.FireTime = System.DateTime.Now.AddSeconds(firetimeSeconds + 1);
        notifications.Add(notification);
        return notification;
    }


#if UNITY_ANDROID || UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            foreach (var notification in notifications)
            {
                if (notification.FireTime > System.DateTime.Now)
                {
                    AndroidNotificationCenter.SendNotification(notification, "channel_taskComplete");
                }
            }
            notifications.Clear();
        }
    }



#endif

    // Update is called once per frame
    void Update()
    {





    }
}
