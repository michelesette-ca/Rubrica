string path = "C:\\Users\\msette\\Desktop\\C#\\Rubrica\\bin\\Debug\\net8.0\\Rubrica.csv";

await Program();
async Task Program()
{
    


    bool end = false;

    do {
        Rubrica();

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

                break;

            case "7":

                break;

            case "8":
                await Exit();
                break;

        }
    } while (!end);
  
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
    Console.WriteLine("questi sono i contatti in rubrica:");
    string ContactBook = await File.ReadAllTextAsync(path);
    Console.WriteLine(ContactBook);

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
            //Console.WriteLine("TROVATO");
            Console.WriteLine("Inserisci Nome contatto:");
            string? name = Console.ReadLine();
            Console.WriteLine("Inserisci Cognome:");
            string? surname = Console.ReadLine();
            Console.WriteLine("Inserisci Email:");
            string? email = Console.ReadLine();
            Console.WriteLine("Inserisci numero di telefono: ");
            string? phone = Console.ReadLine();

            string contactNew = $"{name}, {surname}, {email}, {phone}";

            arrayContact[i]= arrayContact[i].Replace(arrayContact[i], contactNew);
            await File.WriteAllLinesAsync(path, arrayContact);

            Console.WriteLine("Contatto modificato con successo");

            trovato = true;
        }
    }
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


    //Funzione Uscita rubrica

    async Task Exit()
    {
        bool end = false;
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

