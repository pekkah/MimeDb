Mime type db
=============

* Provides methods to get mime-type from extension
* DB is generated from [mime-db](https://github.com/jshttp/mime-db)

Usage
-------------

```csharp
[Theory]
[InlineData("json", "application/json")]
[InlineData("md", "text/markdown")]
public void TryGet(string extension, string mimeType)
{
    /* Given */
    /* When */
    var found = MimeType.TryGet(extension, out var actual);

    /* Then */
    Assert.True(found);
    Assert.Equal(mimeType, actual.Type);
}
```

> NOTE: Extension can start with '.'

Contribution
-------------

To add new mime type please make PR at [mime-db](https://github.com/jshttp/mime-db).