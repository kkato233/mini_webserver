# mini_webserver
現在のディレクトリにある html ファイルを ブラウザに表示するための 小さな WEB サーバー

## 利用手順
start.bat を 起動すると カレントディレクトリにある index.html ファイルを `http://localhost:5000/` に表示できます。 簡易的な WEB サーバーとして利用可能です。

動作環境： [.NET Core ](https://dotnet.microsoft.com/download) 3.0 以上

git からソースを clone して アプリを起動します。
```
git clone https://github.com/kkato233/mini_webserver.git
cd mini_webserver
start.bat
```


## 応用例

### github Page の 動作確認として

github Page で利用する index.html を動作確認するために利用します。

mini_webserver は 現在の github の ディレクトリと同じ並びのディレクトリに clone しておきます。

作業ディレクトリで
```
..\mini_webserver\start.bat
```

とすると http://localhost:5000/index.html ファイルが表示されます。


### 表示できるファイルの種類を増やす

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
