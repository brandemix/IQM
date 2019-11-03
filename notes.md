# Solution Notes

## Prereq
Copied project into a new project generated for .NET Core for macos

## The Plan - Part 1
Create a DataSet class to represent our set of data.
Methods to add data points and retreive various calculations, specifically IQM.
Add testing to validate refactor was successful.

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