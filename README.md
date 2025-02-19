# PSI 2


## Team:
<img src="https://imgur.com/OmBghUj.png" width="25%">

### Members:
- Adrian
- Darius
- Tadas
- Arnas

<img src="https://imgur.com/uGkawtH.png" width="50%">

## How to run the project:
### Frontend:
Go to aim-reaction-app folder. Open up bash (git) terminal and run `npm install`, then `npm run dev`. Thats it. It should work now...
### Backend:
Go to AimReacitonAPI folder. Create appsettings.Development.json

```
{
    "ConnectionStrings": {
      "DefaultConnection": "Host=host;Port=1111;Database=defaultdb;Username=1111;Password=1111"
    }
  }
  
```

Ask user5427 for db connection string or create your own postgreSQL DB.

Open up terminal and run `dotnet restore`, `dotnet build` and `dotnet run`. Everything should work now.