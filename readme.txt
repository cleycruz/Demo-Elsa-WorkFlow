1-Se siguen pasos normales de pa pagina como ensamblados

2.
se abre cmd como administrador
se va a la ruta del proyecto

SET EF_CONNECTIONSTRING=Server=CLEY-ONE;Database=Elsa;User=sa;Password=sa;
dotnet ef migrations add InitialCreate --context SqlServerContext --output-dir Migrations/SqlServer