namespace Simulazione_partita_di_calcio2
{
    internal class Program
    {

        static void GeneraSquadra(int[] titolari, int[] panchina)
        {
            Random rnd = new Random();
            int i = 0;
            while (i < titolari.Length)
            {
                titolari[i] = rnd.Next(40, 91);
                i++;
            }

            int j = 0;
            while (j < panchina.Length)
            {
                panchina[j] = rnd.Next(40, 86);
                j++;
            }
        }


        static int CalcolaPotenza(int[] titolari)
        {
            int totale = 0;
            int i = 0;
            while (i < titolari.Length)
            {
                totale = totale + titolari[i];
                i++;
            }
            return totale;
        }


        static int ContaGiocatori(int[] titolari)
        {
            int attivi = 0;
            int i = 0;
            while (i < titolari.Length)
            {
                if (titolari[i] > 0)
                {
                    attivi++;
                }
                i++;
            }
            return attivi;
        }

        static void Main()
        {
            Random rnd = new Random();

            int[] titA = new int[11];
            int[] panA = new int[5];
            int[] ammonizioniA = new int[11];
            int golA = 0;
            int rossiA = 0;
            int sostA = 0;

            int[] titB = new int[11];
            int[] panB = new int[5];
            int[] ammonizioniB = new int[11];
            int golB = 0;
            int rossiB = 0;
            int sostB = 0;

            int minutoUltimoGol = 0;

            GeneraSquadra(titA, panA);
            GeneraSquadra(titB, panB);

            int recupero = rnd.Next(1, 6);
            int durataTotale = 90 + recupero;

            Console.WriteLine("--- FISCHIO D'INIZIO ---");
            Console.WriteLine("Partita di 90m + " + recupero + "m di recupero");
            Console.WriteLine("------------------------");

            int m = 1;
            while (m <= durataTotale)
            {
                int probEvento = rnd.Next(1, 101);


                if (probEvento <= 5)
                {
                    int potA = CalcolaPotenza(titA);
                    int potB = CalcolaPotenza(titB);
                    int numA = ContaGiocatori(titA);
                    int numB = ContaGiocatori(titB);

                    if (numA > numB)
                    {
                        potA = potA + 100;
                    }
                    else if (numB > numA)
                    {
                        potB = potB + 100;
                    }

                    int totaleMisto = potA + potB;
                    int tiro = rnd.Next(0, totaleMisto);

                    if (tiro < potA)
                    {
                        golA++;
                        minutoUltimoGol = m;
                        Console.WriteLine("Min " + m + ": GOL SQUADRA A!");
                    }
                    else
                    {
                        golB++;
                        minutoUltimoGol = m;
                        Console.WriteLine("Min " + m + ": GOL SQUADRA B!");
                    }

                    Console.WriteLine("    __");
                    Console.WriteLine(" .'\".'\"'.");
                    Console.WriteLine(":._.\"\"._.:");
                    Console.WriteLine(":  \\__/  :");
                    Console.WriteLine(" './  \\.'");
                    Console.WriteLine("    \"\"");
                }


                else if (probEvento <= 8)
                {
                    int squadraScelta = rnd.Next(0, 2);
                    int giocatoreScelto = rnd.Next(0, 11);

                    if (squadraScelta == 0)
                    {
                        if (titA[giocatoreScelto] > 0)
                        {
                            ammonizioniA[giocatoreScelto]++;
                            if (ammonizioniA[giocatoreScelto] == 2)
                            {
                                titA[giocatoreScelto] = 0;
                                rossiA++;
                                Console.WriteLine("Min " + m + ": ROSSO (A) - Giocatore " + (giocatoreScelto + 1) + " espulso!");
                            }
                            else
                            {
                                titA[giocatoreScelto] = titA[giocatoreScelto] - 5;
                                Console.WriteLine("Min " + m + ": Giallo per Squadra A");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Min " + m + ": Non è successo niente");
                        }
                    }
                    else
                    {
                        if (titB[giocatoreScelto] > 0)
                        {
                            ammonizioniB[giocatoreScelto]++;
                            if (ammonizioniB[giocatoreScelto] == 2)
                            {
                                titB[giocatoreScelto] = 0;
                                rossiB++;
                                Console.WriteLine("Min " + m + ": ROSSO (B) - Giocatore " + (giocatoreScelto + 1) + " espulso!");
                            }
                            else
                            {
                                titB[giocatoreScelto] = titB[giocatoreScelto] - 5;
                                Console.WriteLine("Min " + m + ": Giallo per Squadra B");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Min " + m + ": Non è successo niente");
                        }
                    }
                }


                else if (probEvento <= 12)
                {
                    int chiCambia = rnd.Next(0, 2);
                    if (chiCambia == 0)
                    {
                        if (sostA < 5)
                        {
                            int peggiore = 0;
                            int migliore = 0;
                            int j = 0;
                            while (j < 11)
                            {
                                if (titA[j] < titA[peggiore])
                                {
                                    if (titA[j] > 0)
                                    {
                                        peggiore = j;
                                    }
                                }
                                j++;
                            }
                            int k = 0;
                            while (k < 5)
                            {
                                if (panA[k] > panA[migliore])
                                {
                                    migliore = k;
                                }
                                k++;
                            }
                            int temp = titA[peggiore];
                            titA[peggiore] = panA[migliore];
                            panA[migliore] = temp;
                            sostA++;
                            Console.WriteLine("Min " + m + ": Cambio Tattico A (" + sostA + "/5)");
                        }
                        else
                        {
                            Console.WriteLine("Min " + m + ": Non è successo niente");
                        }
                    }
                    else
                    {

                        Console.WriteLine("Min " + m + ": Non è successo niente");
                    }
                }


                else
                {
                    Console.WriteLine("Min " + m + ": Non è successo niente");
                }

                m++;
            }


            Console.WriteLine("\n==============================");
            Console.WriteLine("FISCHIO FINALE");
            Console.WriteLine("Risultato: A " + golA + " - " + golB + " B");
            if (minutoUltimoGol > 0)
            {
                Console.WriteLine("Minuto dell'ultimo gol: " + minutoUltimoGol + "'");
            }
            else
            {
                Console.WriteLine("Nessun gol segnato.");
            }
            Console.WriteLine("Potenza Finale A: " + CalcolaPotenza(titA));
            Console.WriteLine("Potenza Finale B: " + CalcolaPotenza(titB));
            Console.WriteLine("==============================");
        }
    }

}
