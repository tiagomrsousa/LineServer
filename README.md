# LineServer
API to return the content of a line in a file

## Run

Linux/OS X:

```
sh build.sh
```

Windows:

```
build.bat
```

## Run in Docker

```
docker build -t lineServer .
docker run -p 5000:5000 lineServer
```


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

We can see the time increase is not proportional to the file size increase, the load time increase slowly. In any case is a bad experience for the first request takes so much time. My suggestion is a mechanism to make a request each time the application raise up, so the first real client don't have the bad experience of waiting so much time.


