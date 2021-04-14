# mini_webserver
現在のディレクトリにある html ファイルを ブラウザに表示するための 小さな WEB サーバー

## dotnet tool としてインストール

git からソースを clone して アプリを起動します。
```
git clone https://github.com/kkato233/mini_webserver.git
cd mini_webserver
dotnet pack
dotnet tool install --global --add-source ./nupkg mini_webserver
```

表示したい html ファイルがあるディレクトリで
```
mini_webserver
```
コマンドを実行すると カレントディレクトリにある index.html ファイルを `http://localhost:5000/` に表示できます。 簡易的な WEB サーバーとして利用可能です。


## 起動オプション

```
-v 仮想パス指定
```

仮想パスを指定できます。

```
mini_webserver -v my_virtual_path
```
とすると 現在のディレクトリにあるファイルが 

 `http://localhost:5000/my_virtual_path/` 

として表示できるようになります。

## 表示できるファイルの種類を増やす

`StaticFileExtensions` では あらかじめ決まったファイルの拡張子以外のファイルは 404 エラーとなり ファイルの内容が取得できません。

セキュリティー的に予期しないファイルを表示しないようにするための制約ですが下記の２つのパターンの対応策があります。

全ての拡張子を許可する場合
[非標準のコンテンツ タイプ](https://docs.microsoft.com/ja-jp/aspnet/core/fundamentals/static-files?view=aspnetcore-3.1#non-standard-content-types)

``` C#
    app.UseStaticFiles(new StaticFileOptions
    {
        ServeUnknownFileTypes = true,
        DefaultContentType = "image/png"
    });
```

指定の拡張子を許可する場合
[FileExtensionContentTypeProvider](https://docs.microsoft.com/ja-jp/aspnet/core/fundamentals/static-files?view=aspnetcore-3.1#fileextensioncontenttypeprovider)


``` C#
    var provider = new FileExtensionContentTypeProvider();

    // 許可する 拡張子と その Mime/Type を追加する
    provider.Mappings[".opf"] = "text/html";

    app.UseStaticFiles(new StaticFileOptions
    {
        ContentTypeProvider = provider,
    });
```

参考 [file_extention ブランチ](https://github.com/kkato233/mini_webserver/tree/file_extention)

セキュリティーに配慮して 拡張子を指定して そのファイルを許可するようにする方が望ましいと思います。
