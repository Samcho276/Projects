# Doniczkomat
ASP.NET(MVC) web app managing smart plant pots build using ESP8266. Each ESP has its own on board api, the web app is sending requests to gather status of soil moisture and water level in tank.
Based on received information app sends api request to order plant watering.
![](https://github.com/Samcho276/Projects/blob/main/Doniczkomat-main/Readme-images/01.png?raw=true)
![](https://github.com/user-attachments/assets/133db44f-21c7-4881-b16d-bcd1fb141367)
![](https://github.com/user-attachments/assets/dbdf33db-0fa9-4610-a93a-c5550aeada97)

# Organizer3
WebApp created using ASP.NET(MVC), Entity Framework and Identity for user auth. Functionality centered around data storage and employee management for a small company.
To create Admin account: run database migration(```Add-Migration MigrationName```
```Update-Database``` ), register using Identity form, and set permissions in database.

Functionality:

Store users personal data, profile photos and uploads profile pictures.

![0 5](https://github.com/Samo276/Projects/assets/33495575/c18b61a2-0092-48c1-98ac-5a3e15a5a3cc)

Create schedules for employees and register leaves.

![05](https://github.com/Samo276/Projects/assets/33495575/92817615-b9af-4d7a-9ff2-1b880a01a176)

Displays schedules.

![03](https://github.com/Samo276/Projects/assets/33495575/1a06f29b-6545-45f7-b07a-06867e8bb1ce)

Display blog style announcements.

![01](https://github.com/Samo276/Projects/assets/33495575/2df75985-c8c0-49a4-8e37-fc15fde2ddc0)

Document upload using web form, display job applications and recruitment notes.

![04](https://github.com/Samo276/Projects/assets/33495575/d75312ce-e992-4135-abf3-0a27782276d0)

Containes un-configured automatic mailing system created using MailKit, C# open source email technology by Jeffrey Stedfast. 


# Performance reasearch: System.Drawing
</hr>
Microsoft C# System.Drawing performance test in comparison to custom made methods created for simple processing of 24bit jpg images.
Testing was carried out on set of 50 1024x681 images, results were recorded using Jetbrains dotMemory software.

Processing images using Microsoft System.Drawing took 20-21 seconds and used 720MB of system memmory.  
![microsoft-drawing1](https://github.com/Samo276/Projects/assets/33495575/02d30a77-9559-4e1c-8f6c-4a8f89f1669c)
![microsoft-drawing2](https://github.com/Samo276/Projects/assets/33495575/ff6a4be6-6fbd-42a7-a0de-4f96a20bb2d5)

Processing images using Custom made  took 4.4-4.5 seconds and used 248MB of system memmory.  
![custom-image-edit1](https://github.com/Samo276/Projects/assets/33495575/14f0772a-c3b8-4a06-9620-caae1c70ee20)
![custom-image-edit2](https://github.com/Samo276/Projects/assets/33495575/374809f1-9ab5-4af6-b3a3-3ee8f04b6af0)
![custom-image-edit3](https://github.com/Samo276/Projects/assets/33495575/97eb5f32-1812-4571-a55c-4d04eac74a84)

Conclusion:
Using custom methods reduced memmory usage in this case by 65% and execution time by 78%.

