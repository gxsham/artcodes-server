# artcodes-server

## Install and run
Before you run the project make sure you have install dotnet core SDK. Here you can get it for your prefered environment: [Dotnet core SDK](https://www.microsoft.com/net/download?initial-os=windows). After that you should be able to run the project. There are 2 ways to do it:

1. If you use Visual Studio - just open the solution file and click run
2. If you want to use a terminal/commandline open it in artcodes-server/LinkCraft and execute `dotnet run`

If everything is ok go to `localhost:5000` and you will get the swagger page. Here is the [Swagger documentation](https://swagger.io/tools/swagger-ui/) if you're curious about it.

There you can see for now 3 controllers: Account , Experience  and Values. Values controller has no protection, you can use it to test you connection to the server.

## Registration
If you want to test the experience API you will need to register first. Use Swagger to do that. Go to "Register" endpoint, add the required information (email should follow the email patter ex. dd@gmail.com and password should be at least 4 charachters long). After you register (or login if you registered previously) you will get as a response a token. You can use that token to authorize yourself for Experience API.
