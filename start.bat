rem 現在のディレクトリを そのまま WEB アクセスできるようにします。

start http://localhost:5000/
dotnet run --project "%~dp0" %CD%


