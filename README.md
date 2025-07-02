# Funda Assessment

This is the technical assessment for Funda.

This program meets the requirements of:
- Getting all properties in Amsterdam and ranking them by the number of properties per agent.
- Including the option to filter properties that have a garden.
- Allowing any city name to be searched for the same result.

---

## What's Happening?

This app is a **console application** that takes user input parameters to determine:
- Which city to search for.
- Whether to filter by properties with a garden.

Persistence is implemented to allow flexible input for the garden filter beyond just 'y' or 'n'.

The input is passed to a **service layer** that performs the heavy lifting by:
- Calling the HTTP client.
- Grouping agents by the number of properties they have listed.

The HTTP client is configured with a **retry policy using Polly** to handle rate limits.  
Note: Rate limits appear disguised as unauthorized errors rather than explicit messages.

Any other HTTP errors (400 or 500 range) are handled via an extension method.  
The approach is "all or nothing":  
- Beyond retrying for rate limits, the program fails on any 400 or 500 errors.  
- Since a top 10 list is required, 400 errors indicate user input mistakes, so retrying is pointless.  
- Retrying on 500 errors could be considered, but there’s no clear indication when the server might recover.

Once the service layer groups agents appropriately, it returns the results to the app class, which uses **Spectre.Console** to display a nicely formatted table in the console.

---

## Doing Things Differently

Ideas for improvement:  
- Add more resilience to 500 errors, such as exponential backoff retry policies.  
- Output results to a downloadable file.  
- Store the API key securely in a key vault or similar secret management service.

---

## AI Assistance

- Recommended using **Spectre.Console** for console output.  
- Assisted with setting up **Polly** retry policies (no prior experience).  
- Provided recommendations for **unit testing**.

---
