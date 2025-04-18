# 🎮 Wordle en C#

Une version console du célèbre jeu **Wordle**, développée en C#. Devinez un mot mystère de 5 lettres en un maximum de 6 tentatives. Chaque lettre vous donne un indice :

- 🟩 Vert (G) : bonne lettre à la bonne place  
- 🟨 Jaune (Y) : bonne lettre à la mauvaise place  
- ⬛ Gris (X) : lettre absente du mot

## ▶️ Lancer le jeu

1. Ouvre un terminal dans le dossier `Wordle_Test`
2. Exécute :

```bash
dotnet run
```

# 📊 Générer un rapport de couverture de code

```bash
dotnet test --collect:"XPlat Code Coverage"
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coverage-report"
start coverage-report/index.html
```

# si le reportgenerator n'est pas déjà installer 

```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
```