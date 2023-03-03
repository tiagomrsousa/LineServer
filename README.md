# LineServer
API to return the content of a line in a file

## Build

You may need to install the dotnet SDK for your system - https://dotnet.microsoft.com/en-us/download/dotnet/7.0.
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

Before run, you can configure the file to be processed. In the config file *appsettings.json* set the variable *Filepath* with the file path. Example:


```
"AppSettings": {
    "Filepath":  "Resources/Text100000.txt"
  }
```

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

The tests were executed in a developer machine, so they are simply indicators, and we can assume that in a production machine with better specs the results will never be worse than these results.
The results of the load tests through postman can be seen [here](https://github.com/tiagomrsousa/LineServer/tree/main/Postman%20load%20tests).

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

We can see the time increase is not proportional to the file size increase, the load time increase slowly. In any case it is a bad experience for the first request takes so much time. My suggestion is a mechanism to make a request each time the application raises up, so the first real client doesn't have the bad experience of waiting so much time.

### Handling errors

The way we were handling the errors at the first implementation leads us to a poor performance. In the first implementation we were not validating the line requested, and then if the line was out of bounds we threw an exception and caught it, sending to the client a 413 error code.

In a file with 500 lines and doing 1000 requests, the performance was the following depending on the percentages of errors.

| Percentage of errors | Avg. response time (m/s) |
|-----------------|----------------|
| 0% | 3 |
| 50% | 18 |
| 100% | 33 |

As we can see, exception handling was a really big problem. Like the name says *Exception* should be raised just in exceptional situations. Then in the new implementation we validate first if the input is valid and inside the boundaries, if so we continue, if not we just return a 413 error. The performance improved a lot as we can see in the following table.

| Percentage of errors | Avg. response time (m/s) |
|-----------------|----------------|
| 0% | 3 |
| 50% | 4 |
| 100% | 4 |


### Increasing the users

For testing multiple users at the same time we used the file with 100000 rows. As we saw before, and since the file is in memory, the number of lines or the file size doesn't have any impact on the response time. We could only go until 50 users, because the developer machine where the tests were made became unstable. We tried to do tests with more users with postman and newman, and both of them started to give unexpected errors, blocking and we believe the cause was lack of memory since the machine were with 100% of memory usage. Postman and Newman were taking too much memory, so a better test would be running the server in one machine and Postman/Newman and another machine, so the tests didn't impact the server itself.

| Number of users | Avg. response time (m/s) | TPS |
|-----------------|----------------|-------------------|
| 1 | 3 | 333 |
| 5 | 6 | 833 |
| 10 | 10 | 1000 |
| 20 | 14 | 1429 |
| 50 | 31 | 1613 |

![Avg. response time](https://github.com/tiagomrsousa/LineServer/blob/main/Postman%20load%20tests/userVsResponse.PNG)

![TPS](https://github.com/tiagomrsousa/LineServer/blob/main/Postman%20load%20tests/userVsTps.PNG)

As we can see by the data, the increasing of the response time, starts to be proportional to the increasing of users at the same time, so it's possible to extrapolate the behaviour with the increasing of users simultaneously using the server. Now we can decide when we want to scale horizontally, what is the response time acceptable and how many users we expect to have simultaneously.

We can also see that we reached a plateau regarding the TPS, which indicates the system is exhausted.

### Next steps

So, I spent about 20 hours doing the code, preparing the build/run and writing the conclusions.
If I had more time, or if this will turn in a product and not just a POC, I would like to do the following steps:
- Create a docker container to be easier to deploy in every environment
- To avoid the load time impacting real customers, have a process to load the file in background when the application raises up.
- Increase the number of tests instead of just having the scaffolding with a few examples of possible tests.
- Have an implementation using mongo, rather than the in memory approach. Doing NFR tests to find the sweet spot where we should start using mongo.
- Breaking the file in smaller files to improve the load time doing it in parallel.
- Have some kind of cache for the most recurrent requests. We can use in memory or Redis, again just testing it we could have more answers.
- Create NFR so we can test the performance easily, and know in each code modification if the performance is decreasing.
- Having a pipeline to do the deployment, in this case we are not integrating with anyone so we just need a CD process.

