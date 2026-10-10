using System;
using System.Collections.Generic;


// ==============================
//          BUS STOPS
// ==============================

[Serializable]
public class ResponseBS
{
    public LinesRootBS lines;
}

[Serializable]
public class LinesRootBS
{
    public LineInformationBS LineInformation;
}

[Serializable]
public class LineInformationBS
{
    public string codLine;
    public string shortDescription;
    public string description;
    public string codMode;
    public CodMunicipalitiesBS codMunicipalities;
    public ItineraryContainerBS itinerary;
}

[Serializable]
public class CodMunicipalitiesBS
{
    public string[] stringn;
}

[Serializable]
public class ItineraryContainerBS
{
    public ItineraryBS[] Itinerary;
}

[Serializable]
public class ItineraryBS
{
    public string codItinerary;
    public string name;
    public int direction;
    public string kml;
    public StopsContainerBS stops;
}

[Serializable]
public class StopsContainerBS
{
    public StopInformationBS[] StopInformation;
}

[Serializable]
public class StopInformationBS
{
    public string codStop;
    public string shortCodStop;
    public string codMode;
    public string name;
    public string address;
    public string postCode;
    public string codMunicipality;
    public CoordinatesBS coordinates;
    public LinesContainerBS lines;
    public int access;
    public int park;
    public int nightLinesService;
}

[Serializable]
public class CoordinatesBS
{
    public float longitude;
    public float latitude;
}

[Serializable]
public class LinesContainerBS
{
    public LineBS[] Line;
}

[Serializable]
public class LineBS
{
    public string codLine;
    public string shortDescription;
    public string description;
    public string codMode;
    public string updateDate;
    public string updateKmlDate;
    public int nightService;
    public bool active;
    public ShortItineraryBS shortItinerary;
    public string companyCode;
}

[Serializable]
public class ShortItineraryBS
{

}



// ==============================
//          REAL TIMES
// ==============================

//for bus
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

//for metro
[Serializable]
public class ApiResponseRT_metro
{
    public string stopCode;
    public List<RouteRT_metro> routes;
    public string stopName;
    public string isPrimaryProvider;
    public string providerCode;
}

[Serializable]
public class RouteRT_metro
{
    public string platform;
    public List<ArrivalTimesRT_metro> times;
    public string transportType;
    public string routeName;
    public string longName;
    public string color;
    public string shortName;
    public string lineCode;
}

[Serializable]
public class ArrivalTimesRT_metro
{
    public string arrivalDate;
    public bool isProgrammed;
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





// ==============================
//        ALL POSITIONS
// ==============================

[Serializable]
public class StopDataAP
{
    public string id;
    public string name;
    public float latitude;
    public float longitude;
    public List<string> lines;
    public int min;
    public int max;
}

[Serializable]
public class StopCollectionAP
{
    public List<StopDataAP> stops;
}