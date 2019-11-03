# Solution Notes

## Prereq
Copied project into a new project generated for .NET Core for macos

## Part 1
The initial code was simple, but it did not do a great job conveying its purpose.
It looped through the lines of a file, reading them into a list after converting them to Int32 values. On each iteration it would do some math on the list and print the result to stdout.

The first step I took to improve the readability of the code was create a class to describe the purpose and to break up the inner pieces of the loop. This was called DataSet. It represented the
list of data points and allowed me to add functionality while minimizing side effects. For example,
I could extend the List Add() method by creating an AddPoint() method to DataSet. If I wanted to optimize the add method to suite our data needs I could do it here. It also lets other developers know that if they want to tweak how data is added to the set, this would be where to do it.

The other method I added was GetIQMean(). This encapsulated calculating the interquartile mean of the
current set of data. This helped make it obvious what calculation was going on. It also allows for a testable unit of code to validate the calculation is done correctly.

In addition to creating a class to wrap the functionality of the data calculation, I added unit tests
to validate the correctness of the AddPoint() and GetIQMean() methods. For other developers, the unit tests provide a way to help describe the different scenarios possible in each unit and help uncover any assumptions.

Comments are another important part of extending the readability of a program. I added comments to descibe classes, methods, and inner pieces of units to describe my approach to certain scenarios.

## Part 2
1. The first opportunity for optimization I saw was the step where data is inserted at the end of a list
and then sorted at each iteration of the loop. When working up to 100,000 data points, this becomes a pretty significant bottleneck. Since the default comparer for Sort() has Big O of O(n^2) in the worst case, we're looking at Big 0 of (x*n^2) where x is the number of data points were looping over, effectively O(n^3).
To remedy this, instead of always inserted a new data point at the end, I created an AddPoint() method to insert the data in order, using a divide-and-conquer method. The Big O for the new AddPoint was near constant time.

Another approach I took was removing any calls to copy data into a new set. There was no reason we could not use the data list to run calculations as long as we know which data range we were interested in.

2. Based on my understanding, because we're storing each data point as a 32-bit integer, we could probably handle a million, but holding a billion data points in memory would require at least 4GB of memory. I would say that's not a reasonable requirement for any desktop application, another solution would need to be found. A possible approach would be to start throwing away data in the first and fourth quartiles, once we reached a set memory limit.

3.

## Notes
Create a DataSet class to represent our set of data.
Methods to add data points and retreive various calculations, specifically IQM.
Add testing to validate refactor was successful.

Calling Sort() every iteration is bad.
AddPoint() method will use a divide-and-conquer method to insert a new data point
at the correct index, maintaining an ordered list.

Remove the need to do a full recalculation every step.
Remove finding the inner-quartile
Remove the need to sort the list every time we at a point.
Keep track of min/max of first, inner, fourth quartile.
On Insert of point, shuffle data between first, inner, fourth quartile.

Possibly calculate the iqm on insert
