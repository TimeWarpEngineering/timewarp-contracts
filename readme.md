# TimeWarp.Contracts

Transport-neutral operation contracts for HTTP, CLI, and MCP. One request class is the
operation. An HTTP host supplies the route and verb. TimeWarp.Nuru supplies CLI and MCP.
There is no second schema.

The library publishes from this repository as `TimeWarp.Contracts`, on its own prerelease
line. It targets `net10.0` and `net11.0`. Dependencies are `TimeWarp.Mediator`
(15.0.0-beta.4) and `OneOf`. The marker interfaces a request implements (`IQuery<T>`,
`ICommand`, `IIdempotentCommand`, ...) come from `TimeWarp.Mediator`.

Source lives in `source/timewarp-contracts/`. The packed package readme is
`source/timewarp-contracts/readme.md`. Contracts-only tests live in
`tests/timewarp-contracts-tests/`.

```bash
./bin/dev build
./bin/dev test
```

## `[Operation]`

Apply `[Operation(name, Description = ..., Group = ..., Title = ...)]` to the request class
(`Query` or `Command`).

- `name` is the canonical operation name. It is not an HTTP route, a CLI pattern, or an MCP tool name.
- `Description` is the operation summary hosts show in `--capabilities`, OpenAPI, and MCP `tools/list`.
- `Group` is the operation's purpose group. Segments are separated by `.` or space. Null, empty, and
  whitespace mean ungrouped. The group does not prefix the route pattern or the command name.
- `Title` is the human-readable name hosts project to MCP `title`. Null, empty, and whitespace mean
  no title.
- Kind is not an attribute argument. Generators read it from the mediator interface on the request.

## Tool hints

`readOnlyHint` and `idempotentHint` come from the kind. `[Destructive]` and `[OpenWorld]` state
the hints the kind cannot supply. A generator reads them by metadata name. Absence is unset, not
false.

Metadata names: `TimeWarp.Contracts.OperationAttribute`, `TimeWarp.Contracts.DestructiveAttribute`,
and `TimeWarp.Contracts.OpenWorldAttribute`.

## Kind

| Request implements | `OperationKind` |
| --- | --- |
| `IQuery<TResponse>` | `Query` |
| `IIdempotentCommand` or `IIdempotentCommand<TResponse>` | `IdempotentCommand` |
| `ICommand` or `ICommand<TResponse>` | `Command` |

`IIdempotentCommand` is tested before `ICommand`. A request that implements none of these is
omitted from the catalog.

## Catalog

`OperationCatalog` is the document. `--capabilities`, OpenAPI, and MCP `tools/list` are
projections of it. `OperationDescriptor.ReturnType` is the success type's display name. Hosts
that return `OneOf<TSuccess, SharedProblemDetails>` record `TSuccess`.

`SharedProblemDetails` lives in this assembly, in namespace `TimeWarp.Foundation.Types`, so
existing callers keep that identity. It has no ASP.NET Core dependency.
