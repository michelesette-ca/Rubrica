
using System.Xml.Serialization;

string path = "C:\\Users\\msette\\Desktop\\C#\\Rubrica\\bin\\Debug\\net8.0\\Rubrica.csv";

await Program();
async Task Program()
{

    bool end = false;

    do
    {
        await Rubrica();

        string scelta = Console.ReadLine();


        switch (scelta)
        {
            case "1":
                await CreateContact();
                break;

            case "2":
                await ReadContact();
                break;

            case "3":
                await ModifyContact();
                break;

            case "4":
                await DeleteContact();
                break;

            case "5":
                await DeletePhoneNumbers();
                break;

            case "6":
                await ImportFile();
                break;

            case "7":
                await ExportFile();
                break;

            case "8":
                end = true;
                Console.WriteLine("Grazie e Arrivederci");
                break;

        }
    } while (!end);
    Console.ReadKey();
}

//fuzione comandi

async Task Rubrica()
{

    Console.WriteLine("1 - Inserisci Contatto ");
    Console.WriteLine("2 - Visualizza Contatti ");
    Console.WriteLine("3 - Modifica Contatto ");
    Console.WriteLine("4 - Elimina Contatto ");
    Console.WriteLine("5 - Elimina Rubrica ");
    Console.WriteLine("6 - Importa Rubrica");
    Console.WriteLine("7 - Esporta Rubrica ");
    Console.WriteLine("8 - Esci dalla rubrica");
}



//Funzione crea contatto
async Task CreateContact()
{
    Console.WriteLine("Inserisci Nome contatto:");
    string? name = Console.ReadLine();
    Console.WriteLine("Inserisci Cognome:");
    string? surname = Console.ReadLine();
    Console.WriteLine("Inserisci Email:");
    string? email = Console.ReadLine();
    Console.WriteLine("Inserisci numero di telefono: ");
    string? phone = Console.ReadLine();

    string contact = $"{name}, {surname}, {email}, {phone}" + "\n";

    await File.AppendAllTextAsync(path, contact);
    Console.WriteLine("Contatto salvato con successo");

}

//Funzoine che visualizza i contatti

async Task ReadContact()
{


    if (!File.Exists(path))
    {
        Console.WriteLine("La rubrica non esiste");
    }
    else if ((await File.ReadAllLinesAsync(path)).Length <= 0)
    {
        Console.WriteLine("La rubrica è vuota");
    }
    else
    {
        string contactBook = await File.ReadAllTextAsync(path);
        Console.WriteLine("questi sono i contatti in rubrica:");
        Console.WriteLine(contactBook);
    }

    bool end = false;
}

//Funzione per modificare un contatto

async Task ModifyContact()
{
    Console.WriteLine("Inserisci l'email del contatto da modificare: ");
    string ModifyEmail = Console.ReadLine();

    string fileValue = await File.ReadAllTextAsync(path);

    string[] arrayContact = fileValue.Split('\n');

    bool trovato = false;

    for (int i = 0; i < arrayContact.Length; i++)
    {
        if (arrayContact[i].Contains(ModifyEmail))
        {

            Console.WriteLine("Inserisci Nome contatto:");
            string? name = Console.ReadLine();
            Console.WriteLine("Inserisci Cognome:");
            string? surname = Console.ReadLine();
            Console.WriteLine("Inserisci Email:");
            string? email = Console.ReadLine();
            Console.WriteLine("Inserisci numero di telefono: ");
            string? phone = Console.ReadLine();

            string contactNew = $"{name}, {surname}, {email}, {phone}";
            arrayContact[i] = arrayContact[i].Replace(arrayContact[i], contactNew);

            Console.WriteLine("Contatto modificato con successo");

            trovato = true;
        }
    }
    
    await File.WriteAllLinesAsync(path, arrayContact);

    if (!trovato)
    {
        Console.WriteLine("email non trovata");
    }
}

//Funzione elimina contatto

async Task DeleteContact()
{
    Console.WriteLine("Inserisci l'email del contatto da Eliminare: ");
    string DeleteContact = Console.ReadLine();

    string ContactValue = await File.ReadAllTextAsync(path);

    string[] arrayContact = ContactValue.Split('\n');

    bool trovato = false;

    for (int i = 0; i < arrayContact.Length; i++)
    {
        if (arrayContact[i].Contains(DeleteContact))
        {
            arrayContact[i] = arrayContact[i].Remove(0);
            await File.WriteAllLinesAsync(path, arrayContact);

            Console.WriteLine("Contatto Eliminato con successo");
            trovato = true;
        }
    }
    if (!trovato)
    {
        Console.WriteLine("email non trovata");
    }

}

//funzione di elimina rubrica

async Task DeletePhoneNumbers()
{
    File.Delete(path);
    Console.WriteLine("Rubrica ELIMINATA!!");
}

//Importa Rubrica

async Task ImportFile()
{
    Console.WriteLine("Ci vediamo Lunedi");
}



//Esportazione

async Task ExportFile()
{
    string newPath = "C:\\Users\\msette\\Desktop\\Rubrica.csv";

    File.Copy(path, newPath);
    Console.WriteLine("Rubrica esportata sul Desktop");

}

