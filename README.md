# Roslyn API: primi esempi

Questa soluzione .NET 6 contiene vari esempi di utilizzo di Roslyn API, sia in merito di **analisi statica** che di **modifica del codice**:
- `01-syntax-tree`: fa il parsing di un blocco di codice C# e stampa i vari nodi e token ottenuti dal parsing;
- `02-ricerca-nodi`: cerca le dichiarazioni di metodi e, per ciascuna, stampa il nome del metodo e il numero dei parametri che definisce;
- `03-diagnostica`: cerca e stampa l'elenco degli eventuali errori di compilazione;
- `04-modello-semantico`: usa il modello semantico per ottenere informazioni a proposito di metodi, come il tipo che li contiene;
- `05-rimozione-using`: rimuove istruzioni _using_ non necessarie;
- `06-aggiunta-invocazione`: aggiunge l'invocazione a un metodo al codice preesistente;
- `07-caricamento-progetto`: mostra come caricare un intero progetto, fornendo il percorso al file _.csrpoj_;
- `08-document-editor`: apporta varie modifiche a un file di codice usando la classe _DocumentEditor_.

Inoltre, in questa soluzione si trova anche il progetto `AnalyzedProject` che viene usato dagli esempi come progetto oggetto di analisi.

## Per iniziare
Da riga di comando, portati all'interno della directory di uno degli esempi e lancia il progetto con il comando `dotnet`. Ad esempio:
```
cd 01-syntax-tree
dotnet run
```

Assicurati di avere installato la [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).