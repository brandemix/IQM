# Solution Notes

## Prereq
Copied project into a new project generated for .NET Core for macos

## The Plan - Part 1
Create a DataSet class to represent our set of data.
Methods to add data points and retreive various calculations, specifically IQM.
Add testing to validate refactor was successful.

1. 
The initial code was simple, but it did not do a great job conveying its purpose.
It looped through the lines of a file, reading them into a list after converting them to Int32 values. On each iteration it would do some math on the list and print the result to stdout.

The first step I took to improve the readability of the code was create a class to describe the purpose and to break up the inner pieces of the loop. 

2.
One of the things I added to the code was some comments to provide context at different pieces of the
program.
I also added unit tests to help describe the different scenarios each unit covers an help uncover any assumptions.

## The Plan - Part 2
Calling Sort() every iteration is bad.
AddPoint() method will use a divide-and-conquer method to insert a new data point
at the correct index, maintaining an ordered list.

Remove the need to do a full recalculation every step.
Remove finding the inner-quartile
Remove the need to sort the list every time we at a point.
Keep track of min/max of first, inner, fourth quartile.
On Insert of point, shuffle data between first, inner, fourth quartile.

Possibly calculate the iqm on insert