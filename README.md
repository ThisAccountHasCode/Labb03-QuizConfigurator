# Quiz Configurator

## Svenska

En WPF-applikation för att skapa frågepaket och spela quiz. Projektet är under utveckling; checklistan beskriver målen för väl godkänt (VG), inte färdig funktionalitet.

### Kom igång

Kräver Windows och .NET 10 SDK. Öppna projektfilen i Visual Studio med stöd för WPF eller kör från projektmappen:

```powershell
dotnet run --project "Labb03 - Quiz Configurator.csproj"
```

### Krav och plan

- [ ] Bygg med WPF, XAML och MVVM. Använd huvudsakligen databindning och commands, med tydlig separation och minimal kod i code-behind.
- [ ] Skapa och ta bort frågepaket samt ange namn, svårighetsgrad och tidsgräns per fråga.
- [ ] Lägg till, redigera och ta bort frågor med fyra svarsalternativ, varav ett är korrekt. Lägg till och ta bort frågor samt öppna paketinställningar via meny, tangentbordsgenvägar och knappar.
- [ ] Visa frågenummer och totalt antal frågor. Slumpa ordningen på frågor och svar, räkna ner tiden och visa rätt/fel samt korrekt svar efter svar eller timeout. Visa resultat efter alla frågor.
- [ ] Gör menyn tillgänglig med både tangentbordsgenvägar och Alt-navigering.
- [ ] Spara och läs in JSON asynkront med grundläggande felhantering: antingen alla paket automatiskt i Local AppData (skapa mapp/fil vid behov) eller ett paket per fil via filväljare.
- [ ] Importera frågor från [Open Trivia Database](https://opentdb.com/): välj bland alla kategorier, svårighetsgrad och antal. Hämta kategorier och frågor asynkront och visa resultat eller begripliga fel, inklusive API-felkoder och nätverksfel, utan att programmet kraschar.
- [ ] Testa de viktigaste funktionerna och kontrollera användarupplevelse och felhantering, särskilt vid asynkrona operationer.

För VG krävs även välorganiserad, lättläst och utbyggbar kod samt självständiga, välgrundade tekniska val. Helskärmsläge är valfritt.

### Tekniska lösningar och problemlösning

Fyll i under utvecklingen med faktiska exempel inför inlämningen:

- **Viktiga tekniska lösningar:** Beskriv kort några val, hur de fungerar och varför de gjordes.
- **Problem:** Vad gick fel? Hur felsöktes det och hur löstes det?

### Inlämning

Arbeta individuellt och versionshantera projektet i ett nytt GitHub-repository. Gör minst 10 regelbundna commits med beskrivande meddelanden och lämna in länken till repot i en kommentar på itslearning.

---

## English

A WPF application for creating question packs and playing quizzes. The project is under development; the checklist describes the goals for the highest grade (VG), not completed functionality.

### Getting started

Requires Windows and the .NET 10 SDK. Open the project file in Visual Studio with WPF support, or run from the project folder:

```powershell
dotnet run --project "Labb03 - Quiz Configurator.csproj"
```

### Requirements and plan

- [ ] Build with WPF, XAML and MVVM. Primarily use data binding and commands, with clear separation and minimal code-behind.
- [ ] Create and delete question packs, and set their name, difficulty and time limit per question.
- [ ] Add, edit and delete questions with four answer options, one of which is correct. Add and delete questions and open pack options through the menu, keyboard shortcuts and buttons.
- [ ] Show the question number and total. Randomize question and answer order, count down the time, and show whether the answer was correct and identify the correct answer after a response or timeout. Show the result after all questions.
- [ ] Make the menu accessible through both keyboard shortcuts and Alt navigation.
- [ ] Save and load JSON asynchronously with basic error handling: either all packs automatically in Local AppData (creating the folder/file if needed) or one pack per file using file dialogs.
- [ ] Import questions from [Open Trivia Database](https://opentdb.com/): choose from all categories, a difficulty and a number of questions. Fetch categories and questions asynchronously and show the result or understandable errors, including API response codes and network failures, without crashing.
- [ ] Test the main features and check usability and error handling, especially for asynchronous operations.

VG also requires well-organized, readable and maintainable code, as well as independent, well-reasoned technical choices. Full-screen mode is optional.

### Technical solutions and problem solving

Fill this in during development with real examples before submission:

- **Key technical solutions:** Briefly describe several choices, how they work and why they were made.
- **Problem:** What went wrong? How was it investigated and resolved?

### Submission

Work individually and use Git in a new GitHub repository. Make at least 10 regular commits with descriptive messages and submit the repository link in a comment on itslearning.
