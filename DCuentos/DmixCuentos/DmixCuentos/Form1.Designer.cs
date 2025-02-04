namespace DmixCuentos
{
    partial class DCuentos
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            botonEnviar = new Button();
            campoSalida = new RichTextBox();
            campoEntrada = new TextBox();
            SuspendLayout();
            // 
            // botonEnviar
            // 
            botonEnviar.Location = new Point(499, 243);
            botonEnviar.Name = "botonEnviar";
            botonEnviar.Size = new Size(119, 29);
            botonEnviar.TabIndex = 3;
            botonEnviar.Text = "Enviar";
            botonEnviar.UseMnemonic = false;
            botonEnviar.UseVisualStyleBackColor = true;
            botonEnviar.Click += botonEnviar_Click;
            // 
            // campoSalida
            // 
            campoSalida.Location = new Point(152, 12);
            campoSalida.Name = "campoSalida";
            campoSalida.ReadOnly = true;
            campoSalida.Size = new Size(466, 229);
            campoSalida.TabIndex = 5;
            campoSalida.Text = "";
            // 
            // campoEntrada
            // 
            campoEntrada.Location = new Point(152, 247);
            campoEntrada.Name = "campoEntrada";
            campoEntrada.Size = new Size(341, 23);
            campoEntrada.TabIndex = 6;
            // 
            // DCuentos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 340);
            Controls.Add(campoEntrada);
            Controls.Add(campoSalida);
            Controls.Add(botonEnviar);
            Name = "DCuentos";
            Text = "DmixCuentos";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        public TextBox textBox2;
        private Button botonEnviar;
        private RichTextBox campoSalida;
        private TextBox campoEntrada;
        private Label label1;
    }
}
