# CashValueNumberToWordsConverterService

## Description

This service, based on ASP.NET core with a GRPC server converts dollar currency values into the written words,
e.g. when input is 10.57, the result is ten dollars and fifty-seven cents.

- input: must be a double number between 0 and 999 999 999.99
- the separator between dollars and cents must be a `,`(comma)
- the service returns a string with the converted number

## Run the server in windows environment

- Clone the master branch, the branch contains an already build version
- after cloning the application can be found under:
- [local project path]\CashValueNumberToWordsConverterService\CashValueNumberToWordsConverterService\bin\Release\netcoreapp3.1
- start the *CashValueNumberToWordsConverterService* app
- the app starts a local webserver, available under https://localhost:5001, which can be reached by a client.
