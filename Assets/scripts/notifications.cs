using System;
using System.Collections;
using Unity.Notifications.Android;
using UnityEngine;

public class notifications : MonoBehaviour
{
    private const string canal_trayecto_id = "canal_final_trayecto";
    private const string canal_paso_id = "canal_paso_id";
    private const string canal_aviso_autobuses_id = "canal_aviso_autobuses";
    private const string testing_id = "testing";


    private void Start()
    {
        CreateChannel();

        StartCoroutine(NotiPerms());
    }


    public void Create(string type, string info, int minutesRemaining)
    {
        DateTime remaining = DateTime.Now.AddMinutes(minutesRemaining);
        CreateNotification(remaining, type, info);
    }


    private void CreateChannel()
    {
        var channelTrayecto = new AndroidNotificationChannel
        {
            Id = canal_trayecto_id,
            Name = "final_trayecto",
            Description = "Canal usado para avisar del final de trayecto.",
            Importance = Importance.High
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channelTrayecto);



        var channelPaso = new AndroidNotificationChannel
        {
            Id = canal_paso_id,
            Name = "final_paso",
            Description = "Canal usado para avisar del final de un paso del trayecto, antes de un transbordo.",
            Importance = Importance.High
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channelPaso);


        var channelBus = new AndroidNotificationChannel
        {
            Id = canal_aviso_autobuses_id,
            Name = "canal_aviso_autobuses",
            Description = "Canal usado para avisar cuando un autobús se estacione en una parada y una alerta haya sido configurada.",
            Importance = Importance.High
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channelBus);



        var channelTesting = new AndroidNotificationChannel
        {
            Id = testing_id,
            Name = "testing",
            Description = "Canal usado para pruebas.",
            Importance = Importance.Default
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channelTesting);
    }


    private void CreateNotification(DateTime fecha, string targetChannel, string text)
    {
        AndroidNotification notification = new AndroidNotification
        {
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = fecha
        };

        string channelIdToSend = "";

        if (targetChannel == "final_trayecto")
        {
            notification.Title = "Fin de trayecto";
            notification.Text = "Ha llegado a su destino.";
            channelIdToSend = canal_trayecto_id;
        }

        if (targetChannel == "final_paso")
        {
            notification.Title = "Transbordo necesario";
            notification.Text = text;
            channelIdToSend = canal_paso_id;
        }

        if (targetChannel == "canal_aviso_autobuses")
        {
            string bus = text.Length >= 2 ? text.Substring(0, 2) : text;
            string parada = text.Length > 2 ? text.Substring(2) : text;

            notification.Title = "Alerta sobre el " + bus;
            notification.Text = "El autobús " + bus + " va a efecturar su parada sobre la parada " + parada;
            channelIdToSend = canal_aviso_autobuses_id;
        }

        if (targetChannel == "testing")
        {
            notification.Title = "Prueba de notificación";
            notification.Text = "Parece que ha funcionado.";
            channelIdToSend = testing_id;
        }

        if (!string.IsNullOrEmpty(channelIdToSend))
        {
            AndroidNotificationCenter.SendNotification(notification, channelIdToSend);
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


    public void TestNotification()
    {
        Create("testing", "prueba", 0);
    }
}
