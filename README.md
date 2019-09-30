# intrun
Benchmark CPU by iterating over an int

Program will go through every value of an integer and count how long it takes to do so.
Specify the number of threads to run to see performance for any number of threads.

### Commands
```
Use '-h' or 'help' for help menu
Use '-s' or 'single' to run a single test for give amount of threads
Use '-i' or 'increment' to run a test for each number of threads until given amount of threads
Use '-r' or 'repeat' to repeat a test a given number of times
Use '-f' or 'file' to save output in given file.
Use '-e' or 'echo' to output the result to the console
```

### TODO
- Add option to choose between overwriting or appending to results file
- Update how parameters are processed to avoid double work
