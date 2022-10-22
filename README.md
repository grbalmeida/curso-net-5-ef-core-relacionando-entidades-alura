# Exemplos de código do curso .NET 5 e EF Core: relacionando entidades da Alura

Código SQL gerado na consulta de cinemas pelo nome do filme

```sql
SELECT `c`.`Id`, `c`.`EnderecoId`, `c`.`GerenteId`, `c`.`Nome`
 FROM `Cinemas` AS `c`
 WHERE EXISTS (
     SELECT 1
     FROM `Sessoes` AS `s`
     INNER JOIN `Filmes` AS `f` ON `s`.`FilmeId` = `f`.`Id`
     WHERE (`c`.`Id` = `s`.`CinemaId`) AND (('Avatar' LIKE '') OR (LOCATE('Avatar', `f`.`Titulo`) > 0)))
```
