# Cosmos DB

[Local Emulator](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator)

[Data Import](https://docs.microsoft.com/en-us/azure/cosmos-db/import-data)

## SQL

[Cosmos DB - SQL Learning Site](https://www.documentdb.com/sql/demo)

## Mongo DB

[Mongo DB - Introduction](https://docs.mongodb.com/manual/tutorial/query-documents/)

## Gremlin

[Gremlin Reference](https://docs.microsoft.com/en-us/azure/cosmos-db/gremlin-support)

Add Gremlin Sample Data:

```
// add some actors -> Vertex
g.AddV('actor').property('name', 'DeNiro').property('sex', 'male')
g.AddV('actor').property('name', 'Reno').property('sex', 'male')
g.AddV('actor').property('name', 'Wright').property('sex', 'female')

// add some movies -> Vertex
g.AddV('movie').property('name', 'Forest Gump').property('year', '2000')
g.AddV('movie').property('name', 'Ronin').property('year', '1995')
...

// add relations -> Edges
g.V().has('name', 'DeNiro').addE('plays-in').to(g.V().has('name', 'Ronin'))
g.V().has('name', 'Reno').addE('plays-in').to(g.V().has('name', 'Ronin'))


// queries
g.V().has('name', 'DeNiro')
g.V().has('name', 'Ronin')
g.V().has('sex', 'male')
g.V().has('year', between('1990', '2000'))
```
