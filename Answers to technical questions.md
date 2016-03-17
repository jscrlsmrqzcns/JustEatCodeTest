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

2. _What was the most useful feature that was added to the latest version of your chosen language? Please include a snippet of code that shows how you've used it._
As I'm using c# 5.0 I have to go with async/await, although I'm really looking forward to be able to use c# 6.0 null conditional operator '?'.

public async Task<ActionResult> GetByOutCode(string outCode)  
{  
    var restaurants = await _restaurantService.GetByOutCodeAsync(outCode);  
    [...]  
}

3. _How would you track down a performance issue in production? Have you ever had to do this?_

4. _How would you improve the JUST EAT APIs that you just used?_

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