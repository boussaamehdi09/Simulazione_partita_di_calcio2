namespace Simulazione_partita_di_calcio2
{
    internal class Program
    {
        static int Somma(int[] squadra) // Calcola la potenza totale di una squadra
        {
            int somma = 0;
            for (int i = 0; i < squadra.Length; i++)
                somma += squadra[i];
            return somma;
        }

        static void ValorizzazioneGiocatori(int[] squadra, int[] panchinari) // assegna valori casuali che vanno da 30 a 100
        {
            Random rnd = new Random();
            for (int i = 0; i < squadra.Length; i++)
            {
                squadra[i] = rnd.Next(30, 100);
            }
            for (int i = 0; i < panchinari.Length; i++)
            {
                panchinari[i] = rnd.Next(1, 50);
            }

        }

       

        static void StampaGiocatori(int[] squadra, int[] panchinari) // Stampa la lista dei giocatori con la potenza di ogni giocatore
        {
            for (int i = 0; i < squadra.Length; i++)
            {
                Console.WriteLine("Giocatore " + (i + 1) + "  Potenza: " + squadra[i]);
            }
            for (int i = 0; i < panchinari.Length; i++)
            {
                Console.WriteLine("Panchinaro " + (i + 1) + "  Potenza: " + panchinari[i]);
            }
        }

        

        static int ContaGiocatori(int[] squadra)// conta i giocatori titolari
        {
            int tit = 0;
            for (int i = 0; i < squadra.Length; i++)
            {
                if (squadra[i] > 0) // Un giocatore con potenza 0 è considerato espulso
                {
                    tit++;
                }
            }
            return tit;
        }

        // ====== MAIN ======

        static void Main()
        {
            // Squadra A
            int[] titA = new int[11]; // Potenza titolari Squadra A
            int[] panA = new int[5]; // Potenza panchina Squadra A
            int[] ammonA = new int[11]; // Contatore di gialli per giocatore Squadra A
            int golA = 0, rossiA = 0, cambiA = 0;
            // Squadra B
            int[] titB = new int[11]; // Potenza titolari Squadra B
            int[] panB = new int[5]; // Potenza panchina Squadra B
            int[] ammonB = new int[11]; // Contatore di gialli per giocatore Squadra B
            int golB = 0, rossiB = 0, cambiB = 0;

            int minutoUltimoGol = 0;
            Random rnd = new Random();
            // --------- INIZIALIZZAZIONE ---------

            Console.WriteLine("===== SQUADRA A =====");
            ValorizzazioneGiocatori(titA, panA);
            StampaGiocatori(titA, panA);
            Console.WriteLine("----------------------");
            

            Console.WriteLine("\n===== SQUADRA B =====");
            ValorizzazioneGiocatori(titB, panB);
            
            StampaGiocatori(titB, panB);
            Console.WriteLine("----------------------");

            // Calcolo durata partita (90 minuti + recupero da (1-5)
            int recupero = rnd.Next(1, 6);
            int durata = 90 + recupero;

            Console.WriteLine("\n--- FISCHIO D'INIZIO ---");
            Console.WriteLine("========================");

            Console.WriteLine("Durata: 90 + " + recupero + " minuti\n");

           

            for (int minuto = 1; minuto <= durata; minuto++) //  simulazione minuto per minuto
            {
                
                int evento = rnd.Next(1, 101); // Genera un numero da 1 a 100 per determinare l'evento
                if (evento <= 70) // succede un evento
                {
                    
                    if (evento <= 5) // GOL (probabilità 5%)
                    {
                        int potA = Somma(titA);
                        int potB = Somma(titB);

                        if (ContaGiocatori(titA) > ContaGiocatori(titB))
                        {
                            potA += 100;
                        }
                        if (ContaGiocatori(titB) > ContaGiocatori(titA)) 
                        {
                            potB += 100;
                        }

                        int tiro = rnd.Next(0, potA + potB); // gol basato sulla potenza totale

                        if (tiro < potA)
                        {
                            golA++;
                            minutoUltimoGol = minuto;
                            Console.WriteLine("Min " + minuto + ": GOL SQUADRA A");
                        }
                        else
                        {
                            golB++;
                            minutoUltimoGol = minuto;
                            Console.WriteLine("Min " + minuto + ": GOL SQUADRA B");
                        }
                        Console.WriteLine("    __");
                        Console.WriteLine(" .'\".'\"'.");
                        Console.WriteLine(":._.\"\"._.:");
                        Console.WriteLine(":  \\__/  :");
                        Console.WriteLine(" './  \\.'");
                        Console.WriteLine("    \"\"");

                    }

                    // CARTELLINO (Giallo / Rosso)(probabilità dal 6 % all'8%)
                    else if (evento <= 8)
                    {
                        int squadra = rnd.Next(0, 2);
                        int g = rnd.Next(0, 11);

                        if (squadra == 0 && titA[g] > 0)
                        {
                            ammonA[g]++;
                            if (ammonA[g] == 2)
                            {
                                titA[g] = 0;
                                rossiA++;
                                Console.WriteLine("Min " + minuto + ": ROSSO Squadra A (Giocatore " + (g + 1) + ")");
                            }
                            else
                            {
                                titA[g] -= 5;
                                Console.WriteLine("Min " + minuto + ": Giallo Squadra A");
                            }
                            
                        }
                        else if (squadra == 1 && titB[g] > 0)
                        {
                            ammonB[g]++;
                            if (ammonB[g] == 2)
                            {
                                titB[g] = 0;
                                rossiB++;
                                Console.WriteLine("Min " + minuto + ": ROSSO Squadra B (Giocatore " + (g + 1) + ")");
                            }
                            else
                            {
                                titB[g] -= 5;
                                Console.WriteLine("Min " + minuto + ": Giallo Squadra B");
                            }
                            
                        }
                    }

                    // CAMBIO
                    else if (evento <= 12)
                    {
                        int squadra = rnd.Next(0, 2);

                        if (squadra == 0 && cambiA < 5)
                        {
                            int peggiore = 0;
                            for (int i = 1; i < 11; i++)
                            {
                                if (titA[i] > 0 && titA[i] < titA[peggiore])
                                {
                                    peggiore = i;
                                }
                            }
                            int migliore = 0;
                            for (int i = 1; i < 5; i++)
                            {
                                if (panA[i] > panA[migliore])
                                {
                                    migliore = i;
                                }
                            }
                            int temp = titA[peggiore];
                            titA[peggiore] = panA[migliore];
                            panA[migliore] = temp;
                            cambiA++;

                            Console.WriteLine("Min " + minuto + ": Cambio Squadra A (" + cambiA + "/5)");
                            
                        }
                        else if (squadra == 1 && cambiB < 5)
                        {
                            int peggiore = 0;
                            for (int i = 1; i < 11; i++)
                            {
                                if (titB[i] > 0 && titB[i] < titB[peggiore])
                                {
                                    peggiore = i;
                                }
                            }

                            int migliore = 0;
                            for (int i = 1; i < 5; i++)
                            {
                                if (panB[i] > panB[migliore])
                                {
                                    migliore = i;
                                }
                            }
                            int temp = titB[peggiore];
                            titB[peggiore] = panB[migliore];
                            panB[migliore] = temp;
                            cambiB++;

                            Console.WriteLine("Min " + minuto + ": Cambio Squadra B (" + cambiB + "/5)");
                            
                        }
                    }                    
                }
                else
                {
                    Console.WriteLine("Min " + minuto + ": Non è successo niente");

                }

            }

            // --------- RISULTATO FINALE ---------

            Console.WriteLine("\n--- FISCHIO FINALE ---");
            Console.WriteLine("RISULTATO: A " + golA + " - " + golB + " B");

            if (minutoUltimoGol > 0) 
            {
                Console.WriteLine("Ultimo gol al minuto: " + minutoUltimoGol);
            }
            else 
            {
                Console.WriteLine("Nessun gol segnato");
            }
            Console.WriteLine("Potenza finale A: " + Somma(titA));
            Console.WriteLine("Potenza finale B: " + Somma(titB));
        }

    }

}
