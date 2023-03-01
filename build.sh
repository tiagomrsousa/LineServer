
dotnet restore . && \
    dotnet build . && \
    echo "Now, run the following to start the project: dotnet run -p LineServer.csproj --launch-profile web"

