# Quiz Configurator

## Svenska

En WPF-applikation under utveckling för att skapa frågepaket och spela quiz. Checklistan visar vad som är klart och vad som återstår för väl godkänt (VG).

### Kom igång

Kräver Windows och .NET 10 SDK. Öppna projektfilen i Visual Studio med stöd för WPF eller kör från projektmappen:

```powershell
dotnet run --project "Labb03 - Quiz Configurator.csproj"
```

### Krav och plan

- [x] Bygg med WPF, XAML och MVVM. Använd huvudsakligen databindning och commands, med tydlig separation och minimal kod i code-behind.
- [x] Skapa och ta bort frågepaket samt ange namn, svårighetsgrad och tidsgräns per fråga.
- [ ] Lägg till, redigera och ta bort frågor med fyra svarsalternativ, varav ett är korrekt. Lägg till och ta bort frågor samt öppna paketinställningar via meny, tangentbordsgenvägar och knappar.
  - [x] Lägg till och ta bort frågor med knappar, redigera frågetext och fyra svar samt markera rätt svar.
  - [ ] Lägg till meny och tangentbordsgenvägar för frågehantering och paketinställningar.
- [ ] Visa frågenummer och totalt antal frågor. Slumpa ordningen på frågor och svar, räkna ner tiden och visa rätt/fel samt korrekt svar efter svar eller timeout. Visa resultat efter alla frågor.
- [ ] Gör menyn tillgänglig med både tangentbordsgenvägar och Alt-navigering.
- [ ] Spara och läs in JSON asynkront med grundläggande felhantering: antingen alla paket automatiskt i Local AppData (skapa mapp/fil vid behov) eller ett paket per fil via filväljare.
- [ ] Importera frågor från [Open Trivia Database](https://opentdb.com/): välj bland alla kategorier, svårighetsgrad och antal. Hämta kategorier och frågor asynkront och visa resultat eller begripliga fel, inklusive API-felkoder och nätverksfel, utan att programmet kraschar.
- [ ] Testa de viktigaste funktionerna och kontrollera användarupplevelse och felhantering, särskilt vid asynkrona operationer.

För VG krävs även välorganiserad, lättläst och utbyggbar kod samt självständiga, välgrundade tekniska val. Helskärmsläge är valfritt.

Paket och frågor finns för närvarande bara i minnet. Redigeraren visar meddelanden för tom frågetext och ogiltig tidsgräns; fullständig validering av paket och svar återstår.

### Framtida förbättring

- [ ] Vid en kodgranskning på användarens begäran uppmärksammade AI likhetskontrollen i `Question.Text`: överväg samma mönster för andra egenskaper som skickar `PropertyChanged`, så att de bara meddelar ändringar när värdet faktiskt ändras.

### Inlämning

Arbeta individuellt och versionshantera projektet i ett nytt GitHub-repository. Gör minst 10 regelbundna commits med beskrivande meddelanden och lämna in länken till repot i en kommentar på itslearning.

---

## English

A WPF application under development for creating question packs and playing quizzes. The checklist shows what is complete and what remains for the highest grade (VG).

### Getting started

Requires Windows and the .NET 10 SDK. Open the project file in Visual Studio with WPF support, or run from the project folder:

```powershell
dotnet run --project "Labb03 - Quiz Configurator.csproj"
```

### Requirements and plan

- [x] Build with WPF, XAML and MVVM. Primarily use data binding and commands, with clear separation and minimal code-behind.
- [x] Create and delete question packs, and set their name, difficulty and time limit per question.
- [ ] Add, edit and delete questions with four answer options, one of which is correct. Add and delete questions and open pack options through the menu, keyboard shortcuts and buttons.
  - [x] Add and delete questions with buttons, edit question text and four answers, and mark the correct answer.
  - [ ] Add menu items and keyboard shortcuts for question management and pack options.
- [ ] Show the question number and total. Randomize question and answer order, count down the time, and show whether the answer was correct and identify the correct answer after a response or timeout. Show the result after all questions.
- [ ] Make the menu accessible through both keyboard shortcuts and Alt navigation.
- [ ] Save and load JSON asynchronously with basic error handling: either all packs automatically in Local AppData (creating the folder/file if needed) or one pack per file using file dialogs.
- [ ] Import questions from [Open Trivia Database](https://opentdb.com/): choose from all categories, a difficulty and a number of questions. Fetch categories and questions asynchronously and show the result or understandable errors, including API response codes and network failures, without crashing.
- [ ] Test the main features and check usability and error handling, especially for asynchronous operations.

VG also requires well-organized, readable and maintainable code, as well as independent, well-reasoned technical choices. Full-screen mode is optional.

Packs and questions currently exist only in memory. The editor displays messages for empty question text and an invalid time limit; complete pack and answer validation remains to be implemented.

### Future refinement

- [ ] After a user-requested code review, AI highlighted the equality check in `Question.Text`: consider applying the same pattern to other properties that raise `PropertyChanged`, so they only notify when their value changes.

### Submission

Work individually and use Git in a new GitHub repository. Make at least 10 regular commits with descriptive messages and submit the repository link in a comment on itslearning.
