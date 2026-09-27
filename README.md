# HealthLink Patient Portal

A fictional patient portal for browsing available doctors, requesting a chat or video consultation, and viewing scheduled and completed sessions. The Angular client talks to a .NET 10 REST API. Demo doctor data and consultation bookings are held in memory; this is an assessment sample, not a production medical service.

## Requirements

- .NET 10 SDK
- Node.js 20.19+ or 22.12+ and npm

## Run locally

1. Start the API:

   ````1. visual studio 2026
   open the sln solution, and start by using https, after start will show swagger, can use that to test api.

   or

   ```2.powershell
   dotnet run --project .\HealthLinkAPI\HealthLinkAPI.csproj --launch-profile https
   ````

   The API listens at `https://localhost:7101`. In Development, interactive Swagger UI is available at `https://localhost:7101/swagger`; the built-in OpenAPI document remains at `https://localhost:7101/openapi/v1.json`. If your browser does not trust the local HTTPS certificate, run `dotnet dev-certs https --trust` once.

2. In a second terminal, start the Angular app:

   ```powershell
   cd .\HealthLinkWebAngular
   npm install
   npm run dev
   ```

   or

```Open HealthLinkWebAngular folder by using Visual Studio Code
   npm install
   npm run dev


3. Open `http://localhost:4200`. The page loads doctor and consultation data from the API. Select **Book Consultation** on a doctor card, choose Chat or Video and a future date/time, then confirm. New bookings appear in **My Consultations**.

The API allows the Angular development server at `http://localhost:4200` through CORS. The demo portal uses a fixed patient identity (`patient-demo`) instead of authentication. Restarting the API resets bookings to the seeded sample records. This is fictional demo software: do not enter real patient data or rely on its privacy/security for sensitive information.
                                   |

## AI-use reflection

- AI help to read instruction from assessment and help to implement all the requirement.
- AI help to create folder, file, swagger configuration , create all the code for frontend Angular with the backend c# code.
- It also helped translate the supplied mockup into a responsive dashboard with a real booking flow.
- AI helped scaffold the API contracts, seeded demo data, and the standalone Angular client quickly.
- AI help both the .NET solution build and the Angular production build passed after implementation. If error will solve itself
- Manually check coding, folder/file struture, logic implement, validation, naming. If anything not right, ask AI to refactor.
- Manually check and ask AI to implment addtional features helpful, like pagination, search filter, store data in ngxs store, use component lifecycle to avoid memory leaks.

## MyHealthAdvisor feedback

- The “all-in-one digital health companion” message is easy to understand, and the site clearly brings teleconsultation, e-pharmacy, and patient records together.
- The feature sections explain useful parts of the experience, such as chat/video consultations, consultation history, prescriptions, and pharmacy delivery or collection.
- It can help us to understand well what the app service provide

## Improve

- Navigation: The website navigation can be improved. Some tabs take a bit longer to load, so making the navigation faster would give users a better experience.
- Too much information: There is quite a lot of information on the website. It may take some time for users to find what they need. A search function could help users find the information they are looking for more easily.
- Chat with us: A "Chat with Us" function could be added so users can quickly ask questions when they cannot find the information they need.
- App guide: A simple guide on how to use the mobile app would be helpful, especially for new users. For example, the website could provide a step-by-step guide or FAQ section.
- Simpler user journey: The website could highlight the most important actions, such as downloading the app, booking a consultation and getting help, so users can find them more easily.
```
