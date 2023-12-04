#!/bin/bash

DAY=$1
PART=$2
INPUT_FILE=$3

echo "Running solution for day $DAY, part $PART, input file $INPUT_FILE..."
dotnet run --project AdventSolutions/AdventSolutions.csproj -- $DAY $PART $INPUT_FILE
