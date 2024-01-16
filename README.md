# SE-4458-Final
For Donor services: https://19070006002donor.azurewebsites.net/

For Find Blood Service: https://19070006002finalbloodsearch.azurewebsites.net/Swagger/index.html


# Assumptions
Every donor has same photo and that photo stored in https://19070006002cdn.blob.core.windows.net/container/icon.jpg

I used caching in donor page.

# Used services for deployment
o For data model:Azure SQL Server

o For API and UI layer hosting of app services: Azure App Services 

o For API gateway: Azure API management 

o For queue service: Azure Message Queues

o For photos of donors stored CDN: Azure CDN (Blob storage)

o For sending email: Google Gmail Account  

o For scheduling services: Azure Functions

# Data Models
![Resim2](https://github.com/ihsanefeuzun/SE-4458-Final/blob/main/diagram.png?raw=true)





