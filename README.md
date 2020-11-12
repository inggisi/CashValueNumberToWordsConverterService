# CashValueNumberToWordsConverterService

## Description

This service, based on ASP.NET core with a GRPC server converts dollar currency values into the written words,
e.g. when input is 10.57, the result is ten dollars and fifty-seven cents.

- input: must be a double number between 0 and 999 999 999.99
- the separator between dollars and cents must be a `,`(comma)
- the service returns a string with the converted number

## Run the server in windows environment

- With Visual Studio, clone the master branch and build the project.
- Start the debugging or navigate to you project folder \CashValueNumberToWordsConverterService\CashValueNumberToWordsConverterService\bin\... 
and start the CashValueNumberToWordsConverterService app.
- The app starts a local webserver, available under https://localhost:5001, which can be reached by a client.
