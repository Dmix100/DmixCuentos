using System.IO;
using System.IO.Pipes;

namespace DCuentosServer
{
    public class Programa
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var servidor = new NamedPipeServerStream("T.2xl+3^;41d@I;");

                servidor.WaitForConnection();

                var emisor = new StreamWriter(servidor);
                var receptor = new StreamReader(servidor);

                Console.WriteLine("Conexión establecida");
                

                while (true)
                {
                    Console.WriteLine("Esperando el nombre de un cuento...");
                    string recibido = receptor.ReadLine();

                    string fichero = @"..\..\..\..\..\Cuentos\" + recibido + ".txt";


                    if (File.Exists(fichero))
                    {
                        Console.WriteLine("Procesando el cuento...");

                        Cuento miCuento = new Cuento(fichero);

                        int numHuecos = miCuento.NumHuecos;
                        string[] huecos = miCuento.getHuecos();
                        string[] relleno = new string[numHuecos];

                        // Solicitud de las palabras clave al cliente para rellenar los huecos                        
                        for (int x = 0; x < numHuecos; x++)
                        {
                            emisor.WriteLine("Escribe un " + huecos[x]);
                            Console.WriteLine("Enviado: \"Escribe un " + huecos[x] + "\"");
                            emisor.Flush();

                            relleno[x] = receptor.ReadLine();
                            Console.WriteLine("Recibido: \"" + relleno[x] + "\"");
                        }

                        // Obtención del cuento rellenado
                        string cuentoFinal = miCuento.getCuentoFinal(relleno);

                        Console.WriteLine("Enviando el cuento final...");
                        emisor.WriteLine(cuentoFinal.Replace(Environment.NewLine, " ")+"%");
                        emisor.Flush();

                    } else
                    {
                        emisor.WriteLine("Cuento no encontrado, intentalo de nuevo:");
                        emisor.Flush();
                    }

                    
                    

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error inesperado. \n Error: " + e.Message);
            }
        }
    }




    public class Cuento
    {
        private string cuento;
        private int numHuecos;
        public int NumHuecos { get { return numHuecos; } }

        // Método constructor: Lee el archivo del cuento e inicializa la variable numHuecos
        public Cuento(string fichero)
        {
            cuento = File.ReadAllText(fichero);
            Console.WriteLine("Se ha leído el archivo correctamente");

            // Conteo de huecos a rellenar
            numHuecos = 0;
            foreach (char i in cuento)
            {
                if (i == '<')
                {
                    numHuecos++;
                }
            }
        }

        // Método getHuecos: Devuelve un array de string con los huecos extraídos del cuento
        public string[] getHuecos()
        {
            
            // Obtención de palabras clave para rellenar los huecos
            string[] huecos = new string[numHuecos];
            string cuentoProcesar = cuento;
            for (int i = 0; i < numHuecos; i++)
            {
                int indiceInicio = cuentoProcesar.IndexOf("<");
                int indiceFinal = cuentoProcesar.IndexOf(">");
                string hueco = "";

                for (int x = indiceInicio + 1; x < indiceFinal; x++)
                {
                    string letra = Convert.ToString(cuentoProcesar[x]);

                    // Obtención de las palabras clave letra por letra y supresión del guión
                    if (letra == "-")
                    {
                        hueco += " ";
                    }
                    else
                    {
                        hueco += letra;
                    }
                }

                huecos[i] = hueco;

                // Supresión del segmento del cuento ya procesado
                cuentoProcesar = cuentoProcesar.Substring(indiceFinal + 1);
            }

            return huecos;
        }

        /* Método getCuentoFinal: recibe un array de string como parámetro con las palabras para completar el cuento
         * devuelve un string con el cuento rellenado y crea el fichero resultado.txt donde almacena el cuento final
         */
        public string getCuentoFinal(string[] relleno)
        {
            // Fichero del cuento final
            string ficheroResultado = @"..\..\..\..\..\Cuentos\resultado.txt";

            string cuentoFinal = "";
            using (StreamWriter fSalida = File.CreateText(ficheroResultado))
            {
                int i = 0;
                for (int l = 0; l < cuento.Length; l++)
                {
                    string letra = Convert.ToString(cuento[l]);
                    if (letra != "<")
                    {
                        cuentoFinal += letra;
                    } else
                    {
                        cuentoFinal += relleno[i];
                        i++;

                        while (true)
                        {
                            l++;
                            if (cuento[l] == '>')
                            {
                                break;
                            }
                        }
                    }
                }
                fSalida.WriteLine(cuentoFinal);
            }
            
            return cuentoFinal;
        }
    }

}
