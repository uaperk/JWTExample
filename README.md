# JWTExample
I wanted to manipulate the idea of ​​how a simple jwt token works and how to integrate for .net core by recording the token information created as a database transaction over mongoDb into the database.

To use, you can compile the project and use the return token information by sending a post request from http://localhost:5000/api/token/createJwtBearer

(Note : Do not forget to add them to the body(json) of your request 
{
    "username":"uaperk",
    "password":"Xrjk.ck3ty2"
})

If your token time is not finished by sending a get request to address http://localhost:5000/api/weather/getWeatherForecast then simple data will return json dataset.

(Note : Do not forget to add them to the headers of your request 
{
  Content-Type : application/json
  Authorization : Bearer {Token}
})



Packages Used
  - Microsoft.AspNetCore.Authentication.JwtBearer (3.1.5)
  - Microsoft.VisualStudio.Web.CodeGeneration.Design(3.1.3)
  - Microsoft.Extensions.Options(3.1.6)
  - MongoDB.Bson(2.10.4)
  - MongoDB.Driver(2.10.4)
  - Pluralize.Net(1.0.2)
