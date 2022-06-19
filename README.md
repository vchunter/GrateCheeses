# GRATE CHEESES!!

A POC API for cheese lovers who love fine cheese! Because Cheese is Grate!

## INSTALLATION

You'll need a few tools on your local machine to get this running, in particular:
1. Visual Studio or Visual Studio Code
2. Docker Desktop

## USAGE

To build and run the docker container for the Api service, you'll need to open up Powershell, change directories to the GrateCheeses.Api folder and run the following commands.

Run the following command to build the docker container
``` powershell
docker build -t gratecheeses.api:latest .
```

Run the following command to run the docker container. Remember to keep the port mappings the right way, it should be formatted as `host:container`
``` powershell
docker run -d -p 8000:80 gratecheeses.api
```
Once you run the docker command, the API should be available on `localhost:8000` and should default to Swagger


## PROJECT STRUCTURE

The Solution file is called `GrateCheeses.sln` and can be found in the root `GrateCheeses` folder.
The solution is made up of 2 projects:
1. GrateCheeses.Api - The Api project has Swagger configured to run by default to allow the testing of the API endpoints.
2. GrateCheeses.Test - An XUnit test project to unit test the Api project

## License
[MIT](https://choosealicense.com/licenses/mit/)