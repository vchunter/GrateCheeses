# GRATE CHEESES!!

A POC API for cheese lovers who love fine cheese!

## INSTALLATION

You'll need a few tools on your local machine to get this running, in particular:
1. Visual Studio or Visual Studio Code
2. Docker Desktop

## USAGE
Run the following command to build the docker container
``` powershell
docker build -t gratecheeses.api:latest .
```

Run the following command to run the docker container
``` powershell
docker run -d -p 8000:80 gratecheeses.api
```
Once you run the docker command, the API should be available on `localhost:8000` and should default to Swagger


## USAGE

The Solution file is called `GrateCheeses.sln` and can be found in the `GrateCheeses.Api` folder.
The API project has Swagger configured to run by default to allow the testing of the API endpoints.

## License
[MIT](https://choosealicense.com/licenses/mit/)