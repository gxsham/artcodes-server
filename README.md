# artcodes-server

## Install and run
Before you run the project make sure you have install dotnet core SDK. Here you can get it for your prefered environment: [Dotnet core SDK](https://www.microsoft.com/net/download?initial-os=windows). After that you should be able to run the project. There are 2 ways to do it:

1. If you use Visual Studio - just open the solution file and click run
2. If you want to use a terminal/commandline open it in artcodes-server/LinkCraft and execute `dotnet run`

If everything is ok go to `localhost:5000` and you will get the swagger page. Here is the [Swagger documentation](https://swagger.io/tools/swagger-ui/) if you're curious about it.

There you can see for now 3 controllers: Account , Experience  and Values. Values controller has no protection, you can use it to test you connection to the server.

## Registration
If you want to test the experience API you will need to register first. Use Swagger to do that.

1. Go to "Register" endpoint, add the required information (email should follow the email pattern ex. dd@gmail.com and password should be at least 4 charachters long).
2. After you register (or login if you registered previously) you will get as a response a token. Copy this token.
3. At the top right part page of swagger you can see a button "Authorize" - Click on it and write "Bearer  "(be sure you have space after) and paste the token you got after login. Click ok. 
4. Now you should be authorized and make calls like Get or Create Experiences. 

This is the same flow we need to implement in Android applicaion. You will send your credentials to the API, you will get a token back so you can use it (until it expires) in every request without the need to login each time. On the server side this sesion will be active until your token expires. We could cache your credentials in Android app so this login could happen when the application starts. 


