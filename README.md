# LineServer
API to return the content of a line in a file

## Build

You may need to install the dotnet SDK for your system - https://dotnet.microsoft.com/en-us/download/dotnet/7.0
After that make sure the dotnet command is in the Path

Linux/OS X:

```
sh build.sh
```

Windows:

```
./build.bat
```

## Run

Linux/OS X:

```
sh run.sh
```

Windows:

```
./run.bat
```
And you should be able to target https://localhost:5001/swagger/index.html

## Results

The tests we executed in a developer machine, so they are simply indicators, and we can assume that in a production machine with better specs the results will never be worst than these results.
The results of the load tests through postman can be see [here](https://github.com/tiagomrsousa/LineServer/tree/main/Postman%20load%20tests).

### First request time

In the following table we can see the cost of the first request, since we are loading to memory all the file at this time.

| Number of lines | File size (kb) | request time (ms) |
|-----------------|----------------|-------------------|
| 100 | 62 | 144 |
| 500 | 313 | 177 |
| 1000 | 626 | 164 |
| 10000 | 6266 | 162 |
| 100000 | 62668 | 387 |
| 1000000 | 627339 | 2000 |

![Load time](https://github.com/tiagomrsousa/LineServer/blob/main/Postman%20load%20tests/loadGraph.PNG)

We can see the time increase is not proportional to the file size increase, the load time increase slowly. In any case is a bad experience for the first request takes so much time. My suggestion is a mechanism to make a request each time the application raises up, so the first real client don't have the bad experience of waiting so much time.

### Handling errors

### Increasing the users

### Next steps

So, I spent about 20 hours doing the code, preparing the build/run and writing the conclusions.
If I had more time, or if this will turn in a product and not just a POC, I would like to do the following steps:
- Create a docker container to be easier to deploy in every environment
- To avoid the load time impacting real customers, have a process to load the file in background when the application raises up.
- Increase the number of tests instead of just having the scaffolding with a few examples of possible tests.
- Have an implementation using mongo, rather than the in memory approach. Doing NFR tests to find the sweet spot where we should start using mongo.
- Have some kind of cache for the most recurrent requests. We can use in memory or Redis, again just testing it we could have more answers.
- Create NFR so we can test the performance easily, and know in each code modification if the performance is decreasing.
- Having a pipeline to do the deployment, in this case we are not integrating with anyone so we just need a CD process.

