
Funda Assessment

This is the technical assessment for Funda.
This program will meet the requirements of getting all properties in Amsterdam and rank them by most properties as well as the second requirement of including the garden.
It is also possible to enter any city name you wish for the same result.


What's Happening?

This app uses a console application and takes user input parameters to determine which city is being searched for as well the need for a garden.
Persistence is in place to allow for choice of garden outside of the letters y/n.
The input is passed to a service layer, this layer performs the “heavy lifting” of calling the http client and calling the required methods to group the agents by number of properties.
The http client itself is building the retry policy, using Polly, to allow for rate limits.These rate limits being reached appear to be disguised in unauthorised rather than a specific rate limit message.

Any other messages outside of this are being handled in an extension. I have taken an all or nothing approach. So, outside of retrying for rate limits, this will fail if any 400 or 500 errors occur. This is due to the need for the program, it requires a top 10, 400 errors are indicating user errors meaning there is little point in continuing to spam the end point.
There could be a case for retrying on 500 errors but there is no indication of how or when a server might be back online.

After the service layer has group agents appropriately, it will return this response to the app class and use specter console to output the table to the console.


Doing things differently

Continuing from an earlier point. Perhaps build in more resilience to 500 errors. Perhaps exponential back off etc
Output to a downloadable file
Api key should be in key vault or similar secure service


AI Assistance: 

Recommended specter console
Assisted in setting up Polly. I have no experience doing it myself.
Gave recommendations with unit tests.

