# NiftyCreator — Mint-o-Matic Degen creator

The Unity project behind the Mint-o-Matic Degen character creator. Licensed
under Apache-2.0 (see `LICENSE` / `NOTICE`).

## Excluded commercial plugins

Two Unity Asset Store products are **not** redistributed in this repository
because their licenses prohibit public redistribution:

- `Assets/Plugins/CodeStage/` — Anti-Cheat Toolkit (Code Stage)
- `Assets/Plugins/Beebyte/` + `Assets/Plugins/Editor/Beebyte/` — Beebyte
  Obfuscator, including `ProjectSettings/ACTkSettings.asset`

Re-add them from your Asset Store library to restore anti-cheat and build
obfuscation. `[SkipRename]` call sites still compile via a no-op stub at
`Assets/Scripts/Stubs/BeebyteObfuscatorStub.cs`.

## Notes

- `ContractHelper.cs` contains a legacy Ropsten Infura endpoint; the project
  key it references has been retired.
- `Assets/Prefabs/GraphQL/GraphAPI.asset` points at The Graph's public
  subgraph query endpoint — no credentials are embedded.
