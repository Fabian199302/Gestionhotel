namespace Gestionhotel
{
    partial class frmGestionReservas
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblHabitacion = new System.Windows.Forms.Label();
            this.txtHabitacion = new System.Windows.Forms.TextBox();
            this.lblTipoEmpleado = new System.Windows.Forms.Label();
            this.cmbTipoHabitacion = new System.Windows.Forms.ComboBox();
            this.lblHorasExtras = new System.Windows.Forms.Label();
            this.txtDiasEstadia = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnMostrar = new System.Windows.Forms.Button();
            this.lstReserva = new System.Windows.Forms.ListBox();
            this.dateTimeinicio = new System.Windows.Forms.DateTimePicker();
            this.dateTimefinal = new System.Windows.Forms.DateTimePicker();
            this.textvalorpagar = new System.Windows.Forms.TextBox();
            this.labelTotal = new System.Windows.Forms.Label();
            this.lblfechainicio = new System.Windows.Forms.Label();
            this.lblfechafinal = new System.Windows.Forms.Label();
            this.listhabitaciones = new System.Windows.Forms.ListBox();
            this.HABITACIONESDIPONIBLES = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(35, 51);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(44, 13);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(136, 51);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(100, 20);
            this.txtNombre.TabIndex = 1;
            // 
            // lblHabitacion
            // 
            this.lblHabitacion.AutoSize = true;
            this.lblHabitacion.Location = new System.Drawing.Point(35, 220);
            this.lblHabitacion.Name = "lblHabitacion";
            this.lblHabitacion.Size = new System.Drawing.Size(96, 13);
            this.lblHabitacion.TabIndex = 2;
            this.lblHabitacion.Text = "Ingrese Habitacion";
            // 
            // txtHabitacion
            // 
            this.txtHabitacion.Location = new System.Drawing.Point(136, 217);
            this.txtHabitacion.Name = "txtHabitacion";
            this.txtHabitacion.Size = new System.Drawing.Size(100, 20);
            this.txtHabitacion.TabIndex = 3;
            // 
            // lblTipoEmpleado
            // 
            this.lblTipoEmpleado.AutoSize = true;
            this.lblTipoEmpleado.Font = new System.Drawing.Font("Arial Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoEmpleado.Location = new System.Drawing.Point(12, 0);
            this.lblTipoEmpleado.Name = "lblTipoEmpleado";
            this.lblTipoEmpleado.Size = new System.Drawing.Size(376, 33);
            this.lblTipoEmpleado.TabIndex = 4;
            this.lblTipoEmpleado.Text = "Seleccione Tipo Habitaciòn";
            // 
            // cmbTipoHabitacion
            // 
            this.cmbTipoHabitacion.FormattingEnabled = true;
            this.cmbTipoHabitacion.Location = new System.Drawing.Point(419, 12);
            this.cmbTipoHabitacion.Name = "cmbTipoHabitacion";
            this.cmbTipoHabitacion.Size = new System.Drawing.Size(214, 21);
            this.cmbTipoHabitacion.TabIndex = 5;
            this.cmbTipoHabitacion.SelectedIndexChanged += new System.EventHandler(this.cmbTipoEmpleado_SelectedIndexChanged);
            // 
            // lblHorasExtras
            // 
            this.lblHorasExtras.AutoSize = true;
            this.lblHorasExtras.Location = new System.Drawing.Point(35, 314);
            this.lblHorasExtras.Name = "lblHorasExtras";
            this.lblHorasExtras.Size = new System.Drawing.Size(83, 13);
            this.lblHorasExtras.TabIndex = 6;
            this.lblHorasExtras.Text = "Días de Estadia";
            // 
            // txtDiasEstadia
            // 
            this.txtDiasEstadia.Location = new System.Drawing.Point(136, 314);
            this.txtDiasEstadia.Name = "txtDiasEstadia";
            this.txtDiasEstadia.Size = new System.Drawing.Size(100, 20);
            this.txtDiasEstadia.TabIndex = 7;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(88, 399);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(222, 38);
            this.btnAgregar.TabIndex = 8;
            this.btnAgregar.Text = "AGREGAR RESERVA";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnMostrar
            // 
            this.btnMostrar.Location = new System.Drawing.Point(467, 217);
            this.btnMostrar.Name = "btnMostrar";
            this.btnMostrar.Size = new System.Drawing.Size(128, 27);
            this.btnMostrar.TabIndex = 9;
            this.btnMostrar.Text = "MOSTRAR RESERVA";
            this.btnMostrar.UseVisualStyleBackColor = true;
            // 
            // lstReserva
            // 
            this.lstReserva.FormattingEnabled = true;
            this.lstReserva.Location = new System.Drawing.Point(467, 51);
            this.lstReserva.Name = "lstReserva";
            this.lstReserva.Size = new System.Drawing.Size(277, 147);
            this.lstReserva.TabIndex = 10;
            // 
            // dateTimeinicio
            // 
            this.dateTimeinicio.Location = new System.Drawing.Point(18, 279);
            this.dateTimeinicio.Name = "dateTimeinicio";
            this.dateTimeinicio.Size = new System.Drawing.Size(200, 20);
            this.dateTimeinicio.TabIndex = 11;
            this.dateTimeinicio.ValueChanged += new System.EventHandler(this.dateTimeinicio_ValueChanged);
            // 
            // dateTimefinal
            // 
            this.dateTimefinal.Location = new System.Drawing.Point(233, 279);
            this.dateTimefinal.Name = "dateTimefinal";
            this.dateTimefinal.Size = new System.Drawing.Size(200, 20);
            this.dateTimefinal.TabIndex = 12;
            this.dateTimefinal.ValueChanged += new System.EventHandler(this.dateTimefinal_ValueChanged);
            // 
            // textvalorpagar
            // 
            this.textvalorpagar.Location = new System.Drawing.Point(136, 354);
            this.textvalorpagar.Name = "textvalorpagar";
            this.textvalorpagar.Size = new System.Drawing.Size(100, 20);
            this.textvalorpagar.TabIndex = 13;
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Location = new System.Drawing.Point(35, 361);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(70, 13);
            this.labelTotal.TabIndex = 14;
            this.labelTotal.Text = "Total a pagar";
            // 
            // lblfechainicio
            // 
            this.lblfechainicio.AutoSize = true;
            this.lblfechainicio.Location = new System.Drawing.Point(46, 263);
            this.lblfechainicio.Name = "lblfechainicio";
            this.lblfechainicio.Size = new System.Drawing.Size(122, 13);
            this.lblfechainicio.TabIndex = 15;
            this.lblfechainicio.Text = "Seleccione Fecha inicial";
            // 
            // lblfechafinal
            // 
            this.lblfechafinal.AutoSize = true;
            this.lblfechafinal.Location = new System.Drawing.Point(261, 263);
            this.lblfechafinal.Name = "lblfechafinal";
            this.lblfechafinal.Size = new System.Drawing.Size(118, 13);
            this.lblfechafinal.TabIndex = 16;
            this.lblfechafinal.Text = "Seleccione Fecha Final";
            // 
            // listhabitaciones
            // 
            this.listhabitaciones.FormattingEnabled = true;
            this.listhabitaciones.Location = new System.Drawing.Point(136, 100);
            this.listhabitaciones.Name = "listhabitaciones";
            this.listhabitaciones.Size = new System.Drawing.Size(120, 95);
            this.listhabitaciones.TabIndex = 17;
            // 
            // HABITACIONESDIPONIBLES
            // 
            this.HABITACIONESDIPONIBLES.AutoSize = true;
            this.HABITACIONESDIPONIBLES.Location = new System.Drawing.Point(143, 84);
            this.HABITACIONESDIPONIBLES.Name = "HABITACIONESDIPONIBLES";
            this.HABITACIONESDIPONIBLES.Size = new System.Drawing.Size(109, 13);
            this.HABITACIONESDIPONIBLES.TabIndex = 18;
            this.HABITACIONESDIPONIBLES.Text = "Lista de Habitaciones";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 528);
            this.Controls.Add(this.HABITACIONESDIPONIBLES);
            this.Controls.Add(this.listhabitaciones);
            this.Controls.Add(this.lblfechafinal);
            this.Controls.Add(this.lblfechainicio);
            this.Controls.Add(this.labelTotal);
            this.Controls.Add(this.textvalorpagar);
            this.Controls.Add(this.dateTimefinal);
            this.Controls.Add(this.dateTimeinicio);
            this.Controls.Add(this.lstReserva);
            this.Controls.Add(this.btnMostrar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.txtDiasEstadia);
            this.Controls.Add(this.lblHorasExtras);
            this.Controls.Add(this.cmbTipoHabitacion);
            this.Controls.Add(this.lblTipoEmpleado);
            this.Controls.Add(this.txtHabitacion);
            this.Controls.Add(this.lblHabitacion);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblNombre);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblHabitacion;
        private System.Windows.Forms.TextBox txtHabitacion;
        private System.Windows.Forms.Label lblTipoEmpleado;
        private System.Windows.Forms.ComboBox cmbTipoHabitacion;
        private System.Windows.Forms.Label lblHorasExtras;
        private System.Windows.Forms.TextBox txtDiasEstadia;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnMostrar;
        private System.Windows.Forms.ListBox lstReserva;
        private System.Windows.Forms.DateTimePicker dateTimeinicio;
        private System.Windows.Forms.DateTimePicker dateTimefinal;
        private System.Windows.Forms.TextBox textvalorpagar;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Label lblfechainicio;
        private System.Windows.Forms.Label lblfechafinal;
        private System.Windows.Forms.ListBox listhabitaciones;
        private System.Windows.Forms.Label HABITACIONESDIPONIBLES;
    }
}

