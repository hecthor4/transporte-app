using System;
using System.Collections.Generic;

[Serializable]
public class ArrivalTime
{
    public string arrivalDate;
}

[Serializable]
public class BusTrip
{
    public string longName;
    public string transportType;
    public string routeName;
    public string shortName;
    public List<ArrivalTime> times;
}

[Serializable]
public class Response
{
    public bool isPrimaryProvider;
    public string providerCode;
    public string stopCode;
    public List<BusTrip> routes;
}