using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Actualizador_de_Enterprise_Basico
{
    public partial class Form1 : Form
    { 
        public Form1()
        {
            InitializeComponent();
            }

        private string nombreMes(int numeroMes) 
        {
            string mes = "";
            switch (numeroMes)
            {
                case 1:
                    {
                        mes = "Ene";
                        break;
                    }
                case 2:
                    {
                        mes = "Feb";
                        break;
                    }
                case 3:
                    {
                        mes = "Mar";
                        break;
                    }
                case 4:
                    {
                        mes = "Abr";
                        break;
                    }
                case 5:
                    {
                        mes = "May";
                        break;
                    }
                case 6:
                    {
                        mes = "Jun";
                        break;
                    }
                case 7:
                    {
                        mes = "Jul";
                        break;
                    }
                case 8:
                    {
                        mes = "Ago";
                        break;
                    }
                case 9:
                    {
                        mes = "Sep";
                        break;
                    }
                case 10:
                    {
                        mes = "Oct";
                        break;
                    }
                case 11:
                    {
                        mes = "Nov";
                        break;
                    }
                case 12:
                    {
                        mes = "Dic";
                        break;
                    }
               default:
                   mes="";
                       break;
            }
            return mes;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Enterprise (*.exe)|*.exe";
            abrir.InitialDirectory = @"\\central\colaboracion\sistemas\enterprise";
            abrir.Title = "Seleccione el ejecutable NUEVO que va a actualizar";
            if (abrir.ShowDialog() == DialogResult.OK) 
            {
                txtRuta.Text = abrir.FileName;
            }
            abrir.Dispose();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            bool error = false;
            
            DateTime x = DateTime.Now;
            string fecha = x.Day.ToString() + "-" + nombreMes(x.Month) + "-" + x.Year.ToString();
            string hora = x.Hour.ToString() + "_" + x.Minute.ToString() + "_" + x.Second.ToString();

            string rutaEjecutableActual = @"\\central\ApsRed\ENT-MODULAR\";
            string nombreEjecutableActual = "EnterpriseModular.exe";
            string ruta_actual = rutaEjecutableActual + nombreEjecutableActual;
            string ultimoEjecutable = @"\\central\ApsRed\ENT-MODULAR\UltimoEjecutable\EnterpriseModular " + fecha + " " + hora + ".exe";
            
            
            if (txtRuta.Text != "")
            {                
                //aqui hay bug cuando no encuetra el archivo que será renombrado, sin emabargo siempres debe estar porque será el que estén ejecutando.
                try
                {
                    if (chkNotas.Checked)
                    {                        
                        File.Move(ruta_actual, ultimoEjecutable + " " + txtNotas.Text + ".exe");
                    }
                    else
                    {
                        File.Move(ruta_actual, ultimoEjecutable);
                    }

                }
                catch
                {
                    MessageBox.Show("Error!!! Todavia se encuentra Alguien adentro del " + nombreEjecutableActual + " El proceso será abortado para evitar errores");
                    error = true;
                }
                if (error)
                {
                    Application.Exit();
                }
                else
                {
                    File.Move(txtRuta.Text, ruta_actual);
                    MessageBox.Show("El Ejecutable " + nombreEjecutableActual + " fué actualizado con exito.");
                    txtRuta.Text = "";
                    error = false;
                    Application.Exit();
                } 
            }
            else
            {
                MessageBox.Show("Debe ubicar ejecutable que va a actualizar primero\n", "Imposible Actualizar. Posibles Causas:");            
            }
        }

        private void txtNotas_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkNotas_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNotas.Enabled == true)
            {
                txtNotas.Visible = true;
                txtNotas.Text = "";
                txtNotas.Focus();
            }
            else
            {
                txtNotas.Visible = false;
            }
        }        
  }
}
