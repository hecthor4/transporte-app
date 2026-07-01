using System;
using System.Collections;
using Unity.Notifications.Android;
using UnityEditor.Search;
using UnityEngine;

public class notifications : MonoBehaviour
{
    public string idCanal = "canal";


    private void Start()
    {
        CreateChannel();

        StartCoroutine(NotiPerms());
    }


    public void Create(string type, string info, int minutesRemaining)
    {
        idCanal = type;

        DateTime remaining = DateTime.Now.AddMinutes(minutesRemaining);

        CreateNotification(remaining, idCanal, info);
    }


    private void CreateChannel()
    {
        AndroidNotificationChannel channel = new AndroidNotificationChannel
        {
            Id = idCanal,
            Name = "final_trayecto",
            Description = "Canal usado para avisar del final de trayecto.",
            Importance = Importance.High
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);



        AndroidNotificationChannel channel2 = new AndroidNotificationChannel
        {
            Id = idCanal,
            Name = "final_paso",
            Description = "Canal usado para avisar del final de un paso del trayecto, antes de un transbordo.",
            Importance = Importance.High
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel2);
    }


    private void CreateNotification(DateTime fecha, string notiChannel, string text)
    {
        if (notiChannel == "final_trayecto")
        {
            AndroidNotification notification = new AndroidNotification
            {
                Title = "Fin de trayecto",
                Text = "Ha llegado a su destino.",
                SmallIcon = "default",
                LargeIcon = "default",
                FireTime = fecha,
            };
            AndroidNotificationCenter.SendNotification(notification, idCanal);
        }

        if (notiChannel == "final_paso")
        {
            AndroidNotification notification = new AndroidNotification
            {
                Title = "Transbordo necesario",
                Text = text,
                SmallIcon = "default",
                LargeIcon = "default",
                FireTime = fecha,
            };
            AndroidNotificationCenter.SendNotification(notification, idCanal);
        }
    }


    IEnumerator NotiPerms()
    {
        var request = new PermissionRequest();
        while (request.Status == PermissionStatus.RequestPending)
        {
            yield return new WaitForSeconds(1);
        }
    }
}
