# .NET test assignment

## Test Assignment
The purpose of this test assignment is to demonstrate the skills of building scalable and resilient services.
We will assess code structure, patterns applied, solution completeness and correctness.

## Description
Build a REST service to measure distance in miles between two airports. Airports are identified by 3-letter IATA code.
To get airport details HTTP call shall be used, sample for AMS airport:

``` shell
GET https://places-dev.cteleport.com/airports/AMS HTTP/1.1
```

It's allowed to use any 3-rd party components/frameworks. Solution has to be based on dotnet core 5.0+

# Process 

-> User makes a request with two codes 
    [ ] codes in URL parameters 
    [ ] validate parameters 
    [ ] get data from the codes repo
        [ ] check cache if exists for pair 
            [ ] how to cache a pair 1 2 == 2 1
        [ ] get from URL repo 
    [ ] calculate distance 
    [ ] return value or failed result 

-> Logger 
-> Redis as cache   
-> Docker compose with nginx as LB to scale services  
-> Write Readme 

[x] Repository to get IATA 
[x] Validate input string as IATA code
[x] Calculate distance 

# What DONE 

[x] Added API versioning. TODO example 
[ ] tell that Haversine formula used but also has another algorithms such as Vincenty formula
[ ] explain another approaches when : 
    1. over 12,000 IATA (International Air Transport Association) airport codes in use worldwide may be cached in DB and each service read it when starts (NO REDIS)
    2. redis to have reached IATA codes 
    3. Event - Driven 


# temp 

calculate the distance between two coordinates (latitude and longitude) on the Earth's surface using the Haversine formula or Vincenty formula. Below, I'll provide an example of how to calculate the distance using the Haversine formula in C#.

```csharp 
    const double EarthRadiusInMiles = 3958.8; // Earth's radius in miles

    // Convert latitude and longitude from degrees to radians
    double radLat1 = Math.PI * lat1 / 180.0;
    double radLon1 = Math.PI * lon1 / 180.0;
    double radLat2 = Math.PI * lat2 / 180.0;
    double radLon2 = Math.PI * lon2 / 180.0;

    // Haversine formula
    double deltaLat = radLat2 - radLat1;
    double deltaLon = radLon2 - radLon1;
    double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
               Math.Cos(radLat1) * Math.Cos(radLat2) *
               Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);
    double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
    double distance = EarthRadiusInMiles * c;

    return distance;
```