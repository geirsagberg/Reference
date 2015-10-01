## Git
For å sikre at filer med æøå vises riktig, legg til følgende felter i din `.gitconfig`:

```
[core]
quotepath = false
precomposeunicode = true
autocrlf = true
```

## Konvensjoner

- Norske domenenavn med PascalCase (for å gjøre Intellisense lettere): `FilData`, `BygningsNr`
- "Nr" i domenet for Nummer
- "Id" med stor I, liten d
- Controllers og Views kan ikke ha æøå

## Migrering

For å legge til nye domeneklasser/tabeller:
- Lag en klasse i `Reference.Domain`
- Klassen må minst implementere `IEntity`, helst `IVersionedEntityWithId`. Bruk gjerne `EntityBase` som superklasse.
- Bygg `Reference.Domain`
- Kjør T4-transformen `EntityContextBase.tt` i `Reference.Data` (f.eks. Build -> Transform all T4 templates)

Deretter kjør migrering:

- Sett et prosjekt med `EntityContext` ConnectionString som startup project
- i Package Manager Console, velg `Reference.Data` som "Default Project"

```
Add-Migration Navn_på_migrering
Update-Database -Verbose
```

eller

```
Add-Migration Navn_på_migrering -StartupProjectName Reference.Data -ProjectName Reference.Data
Update-Database -Verbose -StartupProjectName Reference.Data -ProjectName Reference.Data
```

## Migrere ned
```
Update-Database -Verbose -StartupProjectName Reference.Data -ProjectName Reference.Data -TargetMigration Navn_på_migrering
```

## Troubleshooting

### Hjelp, noen andre har laget en migrering samtidig!
How to fix:
- Git pull
- `Add-Migration Merge_etellerannet -IgnoreChanges`
- Update-Database
