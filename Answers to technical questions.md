Answers to technical questions
==============================

1. _How long did you spend on the coding test? What would you add to your solution if you had more time? If you didn't spend much time on the coding test then use this as an opportunity to explain what you would add._
Something around four hours split in two sessions, although spent most of the time configuring the tooling and the solution, I have left some TODOs in the code, but first:
 * add acceptance Testing (eg: selenium) for the user history
 * work on the UI: loading, animations, layout, pagination, add element ids for testing...
 * cache requests made to service
 * Add more data to results: result count, sort using distance...
 * Use service to prepopulate user's post code
 * Clean up project of unnecessary libraries  

My approach was to start with a "minimum viable product", using asp.mvc razor. I reused this initial version for clients with no javascript enabled (gradual degradation would have been a better approach),  
and developed a SPA using AngularJs and Asp Web Api.  
For the integration with the restaurants service, I started creating a test using the sample json response in your repository and later changed it to be an integration test that calls the real web service (surprisingly the schema was different).
The application calls the service from the server instead of the browser for various reasons like:  
 * security: Auth info is keep safe in the server and requests originate from our servers only  
 * performance/reliability/availability: api results can be cached/mirrored  
The service implementation accepts  configuration as a parameter as it will change in different environments. Also, in some cases it will be possible to adapt to service changes with a simple configuration adjustment.  
The Web application project Url is set to http://localhost:port/JustEatCodeTest to avoid the use of hardcoded paths in the application.  
Also added quick error handling/logging using Elmah and redirecting errors to Home/Error.  


2. _What was the most useful feature that was added to the latest version of your chosen language? Please include a snippet of code that shows how you've used it._
As I'm using c# 5.0 I have to go with async/await, although I'm really looking forward to be able to use c# 6.0 null conditional operator '?'.

public async Task<ActionResult> GetByOutCode(string outCode)  
{  
    var restaurants = await _restaurantService.GetByOutCodeAsync(outCode);  
    [...]  
}

3. _How would you track down a performance issue in production? Have you ever had to do this?_
I would start by finding out where the problem is looking at the usual suspects:
* browser: javascript memory leak, are we rendering (angular ng-repeat)/transferring too much data at once and we need paging? -> use browser developer tools to find out
* server: have a look at performance counters to see if the application is using too much memory or there is connection retention.
* database: Look at the reports of slowest queries or setup a profiler and look for queries that are deadlocked, reading too many rows, using too much cpu or taking too long.
The majority of performance issues I have come across have been in the database, and I have solved them looking at the execution plan for table scans,  
incorrect typing in joins/wheres, missing indexes or fields in indexes. Other times was as simply as regenerating statistics.


4. _How would you improve the JUST EAT APIs that you just used?_
Found it a bit slow sometimes, maybe it could do with paging or additional filtering.

5. _Please describe yourself using JSON._
{
  "firstName": "Jose Carlos",
  "lastName": "Marquez Cañas",
  "birthDay": "1980-11-26",
  "nationality": "Spanish", 
  "addresses": [
      {
        "street": "Plumbers Row",
        "city": "London",
        "postalCode": "E11AE"
      }
  ],
  "phoneNumbers": [
    {
      "type": "mobile",
      "number": "07514744736"
    },
    {
      "type": "office",
      "number": "020 7666 3082"
    }
  ],
  "profession": "Software Developer",
  "hobbies": [ "coding", "photography", "football" ]
}