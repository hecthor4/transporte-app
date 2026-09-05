using System;
using System.Collections.Generic;


// ==============================
//          REAL TIMES
// ==============================


[Serializable]
public class ArrivalTimeRT
{
    public string arrivalDate;
}

[Serializable]
public class BusTripRT
{
    public string longName;
    public string transportType;
    public string routeName;
    public string shortName;
    public List<ArrivalTimeRT> times;
}

[Serializable]
public class ResponseRT
{
    public bool isPrimaryProvider;
    public string providerCode;
    public string stopCode;
    public List<BusTripRT> routes;
}






// ==============================
//          POSITIONS
// ==============================



[Serializable]
public class ResponsePS
{
    public string status;
    public string code;
    public string message;
    public DataPS data;
}

[Serializable]
public class DataPS
{
    public List<TrayectoPS> trayectos;
}

[Serializable]
public class TrayectoPS
{
    public string empresa;
    public string codlin;
    public string codtray;
    public string sentido;
    public string codsae;
    public List<ParadaPS> paradas;
}

[Serializable]
public class ParadaPS
{
    public string codigo;
    public string nombre;
    public string idsae;
    public List<string> coordinates;
    public List<string> enlaces;
}