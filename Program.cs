
using System.Xml.Serialization;

string path = "C:\\Users\\msette\\Desktop\\C#\\Rubrica\\bin\\Debug\\net8.0\\Rubrica.csv";
string newPath = "C:\\Users\\msette\\Desktop\\Rubrica.csv";

await Program();
async Task Program()
{

    bool end = false;

    do
    {
        Rubrica();

        string? scelta = Console.ReadLine();




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

void Rubrica()
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



//Funzione crea contatto (case 1)
async Task CreateContact()
{
    Console.WriteLine("Inserisci Nome contatto:");
    string? name = Console.ReadLine();
    Console.WriteLine("Inserisci Cognome:");
    string? surname = Console.ReadLine();
    Console.WriteLine("Inserisci Email:");
    string? email = Console.ReadLine();

    if (!File.Exists(path))
    {
        using StreamWriter writer = new StreamWriter(path);
    }
    string[] arrayContact = await File.ReadAllLinesAsync(path);


    bool trovato = false;

    for (int i = 0; i < arrayContact.Length; i++)
    {
        if (arrayContact[i].Contains(email))
        {
            Console.WriteLine("email gia esistente");
            trovato = true;
            break;
        }

    }
    if (trovato == false)
    {
        Console.WriteLine("Inserisci numero di telefono: ");
        string? phone = Console.ReadLine();

        string contact = $"{name}, {surname}, {email}, {phone}" + "\n";

        await File.AppendAllTextAsync(path, contact);
        Console.WriteLine("Contatto salvato con successo");
    }




}

//Funzoine che visualizza i contatti (Case 2)

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


}

//Funzione per modificare un contatto (Case 3)

async Task ModifyContact()
{
    Console.WriteLine("Inserisci l'email del contatto da modificare: ");
    string? modifyEmail = Console.ReadLine();

    string[] arrayContact = await File.ReadAllLinesAsync(path);

    bool trovato = false;

    for (int i = 0; i < arrayContact.Length; i++)
    {
        if (arrayContact[i].Contains(modifyEmail))
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

            //Conferma modifica
            string answer = "";

            while (answer != "si" && answer != "no")
            {
                Console.WriteLine("Sei sicuro di voler modificare il contatto? (si/no)");
                answer = Console.ReadLine().ToLower();


                if (answer == "si")
                {
                    arrayContact[i] = arrayContact[i].Replace(arrayContact[i], contactNew);
                    Console.WriteLine("Contatto modificato con successo");

                }
                else if (answer == "no")
                {
                    Console.WriteLine("Operazione annullata");
                }
                else
                {
                    Console.WriteLine("Carattere non valido");

                }
            }


            trovato = true;
        }
    }

    await File.WriteAllLinesAsync(path, arrayContact);

    if (!trovato)
    {
        Console.WriteLine("email non trovata");
    }
}

//Funzione elimina contatto (Case 4)

async Task DeleteContact()
{
    Console.WriteLine("Inserisci l'email del contatto da Eliminare: ");
    string? deleteContact = Console.ReadLine();

    string[] arrayContact = await File.ReadAllLinesAsync(path);

    bool trovato = false;

    for (int i = 0; i < arrayContact.Length; i++)
    {
        if (!string.IsNullOrEmpty(deleteContact) && arrayContact[i].Contains(deleteContact))
        {
            //Conferma eliminazione contatto
            string answer = "";
            while (answer != "si" && answer != "no")
            {
                Console.WriteLine("Sei sicuro di voler eliminare il contatto? (si/no)");


                answer = Console.ReadLine().ToLower();

                if (answer == "si")
                {
                    arrayContact[i] = arrayContact[i].Remove(0);
                    await File.WriteAllLinesAsync(path, arrayContact);
                    Console.WriteLine("Contatto Eliminato con successo");
                }
                else if (answer == "no")
                {
                    Console.WriteLine("Operazione annullata");
                }
                else
                {
                    Console.WriteLine("Carattere non valido");
                }
            }

            trovato = true;
        }
    }
    if (!trovato)
    {
        Console.WriteLine("email non trovata");
    }

}

//funzione di elimina rubrica (Case 5)

async Task DeletePhoneNumbers()
{
    string answer = "";
    while (answer != "si" && answer != "no")
    {

        Console.WriteLine("Sei sicuro di voler eliminare la rubrica? (si/no)");
        answer = Console.ReadLine().ToLower();
        if (answer == "si")
        {
            File.Delete(path);
            Console.WriteLine("Rubrica ELIMINATA!!");
        }
        else if (answer == "no")
        {
            Console.WriteLine("Operazione annullata");
        }
        else
        {
            Console.WriteLine("Carattere non valido");

        }
    }
}

//Importa Rubrica (Case 6)

async Task ImportFile()
{
    string[] contatti = await File.ReadAllLinesAsync(path);


    string[] nuoviContatti = await File.ReadAllLinesAsync(newPath);


    for (int i = 0; i < nuoviContatti.Length; i++)
    {

        string[] newData = nuoviContatti[i].Split(",");
        for (int j = 0; j < contatti.Length; j++)
        {


            if (contatti[j].Contains(newData[2]))
            {
                Console.WriteLine("Vuoi mantenere il nuovo contatto?");
                string[] dati = contatti[j].Split(",");
                Console.WriteLine("si - Contatto importato");
                Console.WriteLine($"Nome: {newData[0]}, Cognome: {newData[1]}, Email: {newData[2]}, Telefono: {newData[3]}");
                Console.WriteLine("no - Contatto vecchio");
                Console.WriteLine($"Nome: {dati[0]}, Cognome: {dati[1]}, Email: {dati[2]}, Telefono: {dati[3]}");

                Console.WriteLine("Inserisci il numero del contatto che vuoi mantenere: ");
                string? scelta = Console.ReadLine();

                if (scelta == "si")
                {
                    Console.WriteLine("Operazione effettuata");
                    contatti[j] = nuoviContatti[i];
                    break;

                }
                else
                {
                    Console.WriteLine("Carattere non valido");
                }
            }
        }

    }
    await File.WriteAllLinesAsync(path, contatti);
}



//Esportazione (Case 7)

async Task ExportFile()
{
    
    File.Copy(path, newPath );
    Console.WriteLine("Rubrica esportata sul Desktop");

}

