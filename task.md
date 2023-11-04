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
        [x] check cache if exists for pair 
            [ ] how to cache a pair 1 2 == 2 1
        [x] get from URL repo
        [ ] redis
        [x] in-memory
    [x] calculate distance 
    [ ] return proper status codes
    [ ] return value or failed result 

-> Logger 
-> Redis as cache   
-> Docker compose with nginx as LB to scale services  
-> Write Readme 
-> Final refactoring

[?] the path could be in both ways around the glob. Choose smalest?  

# What DONE 

[x] Added API versioning. TODO example 
[ ] tell that Haversine formula used but also has another algorithms such as Vincenty formula
[ ] explain another approaches when : 
    1. over 12,000 IATA (International Air Transport Association) airport codes in use worldwide may be cached in DB and each service read it when starts (NO REDIS)
    2. redis to have reached IATA codes 
    3. Event - Driven 


# json  

{"detail":"Airport not found"}%

[{'type': 'string_pattern_mismatch', 'loc': ('path', 'code'), 'msg': "String should match pattern '^[A-Z]{3}$'", 'input': 'AMSS', 'ctx': {'pattern': '^[A-Z]{3}$'}, 'url': 'https://errors.pydantic.dev/2.4/v/string_pattern_mismatch'}]%

