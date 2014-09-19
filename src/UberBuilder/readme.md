UberBuilder.cs
--------------
A simple builder class to help constructing UBER messages and output them as XML or JSON strings for testing. 

Nothing fancy, just type something like (based on the examples on the UBER format documentation https://rawgit.com/mamund/media-types/master/uber-hypermedia.html):

```csharp
//***** Create UBER builder;
var uber = new UberBuilder();

//***** The <data> element example https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_the_tt_lt_data_gt_tt_element
uber.Data.Add(new UberData { Rel="self", Url = new Uri("http://example.org") });
uber.Data.Add(new UberData { Name = "list", Rel = "collection", Url = new Uri("http://example.org/list/") });
uber.Data.Add(new UberData { Name = "search", Rel = "search collection", Url = new Uri("http://example.org/search"), Model = "{title}" });
uber.Data.Add(new UberData { Name = "todo", Rel = "item http://example.org/rels/todo", Url = new Uri("http://example.org/list/1"), Data = { new UberData { Name = "title", Value = "Clean House" }, new UberData { Name = "dueDate", Value = "2014-05-01" } } });
uber.Data.Add(new UberData { Name = "todo", Rel = "item http://example.org/rels/todo", Url = new Uri("http://example.org/list/2"), Data = { new UberData { Name = "title", Value = "Paint the fence" }, new UberData { Name = "dueDate", Value = "2014-06-01" } } });

//***** The <error> element example https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_the_tt_lt_error_gt_tt_element
uber.Error.Add(new UberData { Name = "code", Value = "q1w2e3" });
uber.Error.Add(new UberData { Name = "dump", Value = "http://example.org/debug/1" });

//***** Output as XML;
uber.ToXmlString();

//***** Output as JSON;
uber.ToJsonString();
```

And it will output:

```xml
<uber version="1.0">
	<data rel="self" url="http://example.org/"/>
	<data name="list" rel="collection" url="http://example.org/list/"/>
	<data name="search" rel="search collection" url="http://example.org/search" model="{title}"/>
	<data name="todo" rel="item http://example.org/rels/todo" url="http://example.org/list/1">
		<data name="title">Clean House</data>
		<data name="dueDate">2014-05-01</data>
	</data>
	<data name="todo" rel="item http://example.org/rels/todo" url="http://example.org/list/2">
		<data name="title">Paint the fence</data>
		<data name="dueDate">2014-06-01</data>
	</data>
	<error>
		<data name="code">q1w2e3</data>
		<data name="dump">http://example.org/debug/1</data>
	</error>
</uber>
```

```json
{
	"uber" :
	{
		"version" : "1.0",
		"data" : [
			{
				"rel" : ["self"],
				"url" : "http://example.org/"
			},
			{
				"name" : "list",
				"rel" : ["collection"],
				"url" : "http://example.org/list/"
			},
			{
				"name" : "search",
				"rel" : ["search", "collection"],
				"url" : "http://example.org/search",
				"model" : "{title}"
			},
			{
				"name" : "todo",
				"rel" : ["item", "http://example.org/rels/todo"],
				"url" : "http://example.org/list/1",
				"data" : [
					{
						"name" : "title",
						"value" : "Clean House"
					},
					{
						"name" : "dueDate",
						"value" : "2014-05-01"
					}
				]
			},
			{
				"name" : "todo",
				"rel" : ["item", "http://example.org/rels/todo"],
				"url" : "http://example.org/list/2",
				"data" : [
					{
						"name" : "title",
						"value" : "Paint the fence"
					},
					{
						"name" : "dueDate",
						"value" : "2014-06-01"
					}
				]
			}
		],
		"error" : [
			{
				"name" : "code",
				"value" : "q1w2e3"
			},
			{
				"name" : "dump",
				"value" : "http://example.org/debug/1"
			}
		]
	}
}
```
