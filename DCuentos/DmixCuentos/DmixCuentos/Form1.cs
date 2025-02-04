using System.Diagnostics;
using System.IO.Pipes;

namespace DmixCuentos
{
    public partial class DCuentos : Form
    {
        private Thread clientePipe;
        private string mensajeEnviado;
        private string mensajeRecibido;


        public DCuentos()
        {
            InitializeComponent();

            clientePipe = new Thread(inicioCliente);
            clientePipe.Start();
        }

        // Método del evento Click del botón para enviar mensajes al servidor
        private async void botonEnviar_Click(object sender, EventArgs e)
        {
            // Se asigna el valor del texto del campo de entrada a la variable mensajeEnviado para que sea enviado al servidor por el método inicioCliente
            mensajeEnviado = (!string.IsNullOrEmpty(campoEntrada.Text))?campoEntrada.Text: " ";

            await anadirMensaje();
        }

        /* Método anadirMensaje: Método asíncrono que añade el texto recibido 
         * por el servidor en el campo de salida con efecto de escritura
         */
        public async Task anadirMensaje()
        {
            // Bucle a la espera de respuesta por el servidor 
            while (true)
            {
                if (!string.IsNullOrEmpty(mensajeRecibido))
                {
                    // Se deshabilita el botón para evitar entradas del usuario mientras se escribe en el campo de salida
                    botonEnviar.Enabled = false;

                    // Se recorre el mensaje recibido por el servidor letra por letra y se añaden cada 30ms para darle efecto
                    foreach (char i in mensajeRecibido)
                    {
                        
                        if (i == '%')
                        {
                            await Task.Delay(1000);
                            string invitacion = "¿Quieres completar otro cuento? Si es así escribe el nombre del cuento";
                            campoSalida.AppendText("\n\n");

                            foreach (char c in invitacion)
                            {
                                campoSalida.AppendText(Convert.ToString(c));
                                await Task.Delay(15);
                            }

                            break;
                        }

                        campoSalida.AppendText(Convert.ToString(i));
                        await Task.Delay(30);

                        
                    }

                    campoSalida.AppendText("\n");

                    // Se habilita el botón y se vacia el mensajeRecibido
                    botonEnviar.Enabled = true;
                    mensajeRecibido = "";
                    break;
                }
            }
        }


        /* Método inicioCliente: Método que se ejecuta en un hilo secundario, llama a un método para iniciar el servidor
         * con el que realiza una conexión pipe, ejecuta un bucle while a la espera de información para enivar y esperando
         * recibir información del servidor para actualizar la variable intermedia que usan ambos hilos
         */
        public void inicioCliente()
        {
            try
            {
                
                Process procesoServidor = new Process();
                inicioServidor(out procesoServidor);
                Task.Delay(1000).Wait();
                

                var cliente = new NamedPipeClientStream("T.2xl+3^;41d@I;");

                cliente.Connect();

                // Utilizamos Invoke para solicitar al hilo principal añadir texto en el campo de salida
                Invoke(new MethodInvoker(delegate
                {
                    campoSalida.AppendText("Conexión establecida \n\n");
                    campoSalida.AppendText("¿Con qué cuento te gustaría empezar?\n");
                }));

                var emisor = new StreamWriter(cliente);
                var receptor = new StreamReader(cliente);

                while (true)
                {

                    // El bucle se ejecuta hasta que se asigna un valor a mensajeEnviado desde la interfaz gráfica
                    if (!string.IsNullOrEmpty(mensajeEnviado))
                    {
                        emisor.WriteLine(mensajeEnviado);
                        emisor.Flush();


                        string recibido = receptor.ReadLine();

                        // Si se recibe información del servidor se asigna el valor a mensajeRecibido para que sea leido por el hilo principal
                        mensajeRecibido = recibido;
                        
                          
                        mensajeEnviado = "";

                    }

                }
            }
            catch (Exception e)
            {
                Invoke(new MethodInvoker(delegate
                {
                    campoSalida.AppendText("Ha ocurrido un error inesperado. \nDetalles: " + e.Message);
                }));
            }
        }

        /* Método inicioServidor: Se encarga de iniciar el ejecutable Servidor.exe 
         * con el que se reliaza una conexión pipe.
         */
        public static Process inicioServidor(out Process proceso)
        {
            ProcessStartInfo info = new ProcessStartInfo(@"..\..\..\..\..\DCuentosServer\DCuentosServer\bin\Debug\net8.0\DCuentosServer.exe");

            info.CreateNoWindow = false;

            proceso = Process.Start(info);

            return proceso;
        }
    }
}
