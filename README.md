# Web Event Manager

Web Event Manager on külaliste registreerimissüsteem, mis võimaldab lisada/muuta tulevikus toimuvaid üritusi ja vaadata juba toimunud üritusi. Kasutaja saab vajadusel tulevikus toimuvaid üritusi ka kustutada. Igale üritusele saab lisada osavõtjaid, kes jagunevad eraisikuteks ning juriidilisteks isikuteks (ettevõtted). Salvestatud osavõtjate andmeid saab vaadata, muuta ning vajadusel kustutada, lisaks saab salvestatud olemasolevaid osavõtjaid lisada teistele üritustele. Iga ürituse puhul saab näha täielikku nimekirja külalistest.

## Kasutamine

Projekti käivitamiseks vajalik Visual Studio 2022. Veenduge, et järgmised paketid on paigaldatud:

- Microsoft.VisualStudio.Web.CodeGeneration.Design  {7.0.3}
- xunit.runner.visualstudio  {2.4.5}
- Microsoft.EntityFrameworkCore {7.0.2}
- Microsoft.NET.Test.Sdk   {17.4.1}
- Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore {7.0.2}
- xunit.assert   {2.4.2}
- xunit  {2.4.2}
- Moq   {4.18.4}
- xunit.extensibility.core {2.4.2}
- Microsoft.EntityFrameworkCore.Tools  {7.0.2}
- Microsoft.EntityFrameworkCore.SqlServer  {7.0.2}

Kui need paketid pole paigaldatud, siis paigaldage need järgmiselt:

1. Avage Visual Studio 2022
2. Valige menüüst `Tools` -> `NuGet Package Manager` -> `Package Manager Console`
3. Käivitage järgmine käsk `Install-Package [paketi nimi] -Version [versiooni number]` iga paketi jaoks eraldi

Kui soovite algse sisuga andmebaasi asemel proovandmebaasi, siis tuleb Program.cs failis lk 34 ühendusestringi kommentaar maha võtta. (Proovandmebaasis on isikukoodid valed. Kui soovid muuta külalise andmeid, siis pead muutma isikukoodi, kuna käivutub valideerimine)

Projekti käivitamine:

1. Avage projekt Visual Studiost
2. Klõpsake nuppu "Käivita" või vajutage F5
3. Sirvige veebisaiti brauseris, tavaliselt leiate selle aadressilt http://localhost:<port>

Loodame, et teile meeldib meie Web Event Manager projekt!
