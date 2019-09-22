# klassify

You can use this `klassify` console application to make plain old C# objects 
based on the tables in your SQL Server database.

## Usage

```
 --out, -o:
        output directory
 --user, -u:
        sql server user id
 --password, -p:
        sql server password
 --server, -s:
        sql server
 --database, -d:
        sql database
 --timeout, -t:
        connection timeout
 --help, -h:
        show help
```

Arguments, except `--help`, can be specified in a `.env` file. See 
[the example](./example.env). Place any `.env` file in the same folder 
as `klassify.exe`. These methods can be combined. Values set on the command 
line take precedence.

## Attribution, Acknowledgment and References

- The SQL at the heart of this was shared by [Alex Aza](https://stackoverflow.com/users/732945/alex-aza) as an answer
  to [this question](https://stackoverflow.com/questions/5873170/generate-class-from-database-table) on Stack Overflow.

- Thanks to  [JP Dillingham](https://github.com/jpdillingham) 
  for his command line parser available on nuget.org

- Makes use of [DotNetEnv](https://github.com/tonerdo/dotnet-env) too.  

## Known Issues

- Asterisk (*) and other special characters in column names may result in an 
  empty class being generated for the table to which that column belongs.

## License

[MIT License](https://choosealicense.com/licenses/mit/)

Copyright (c) 2019 Trevor Reid

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.