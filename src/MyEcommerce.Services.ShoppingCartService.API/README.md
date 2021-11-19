# Shopping Cart Service

## Development

Use the watch and dev scripts for development.
Watch compiles typescript.
Dev runs server.

```
npm run watch
npm run dev
```

## Migrations

Entities and migrations should first be compiled from typescript
into the dist folder before using the commands below.

```
npm run migrate:generate -n <MigrationName>
npm run migrate:up
npm run migrate:down
```

## Env

Use this command to generate type safe environment values:

```
npm run gen:env
```

# tsoa

Use this command to generate routes and openapi spec.

```
npm run gen:tsoa
```
