﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RespZip
{
    public partial class Form_Pruebas : Form
    {
        public Form_Pruebas()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }
        private void startAsyncButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy!=true)
            {
                //iniciamos la operacion asincrona o en segundo plano
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void cancelAsyncButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation==true)
            {
                //cancelamos la operacion asincrona
                backgroundWorker1.CancelAsync();
            }
        }
        //el siguiente Controlador de evento es donde se  hace el trabajo con mucho tiempo consumido
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            for (int i = 1; i <= 50; i++)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    //pausa entre cada completado del progreso de la barra que significara un retardo simulado
                    System.Threading.Thread.Sleep(400);//milisegundos
                    worker.ReportProgress(i * 2);
                }
            }
        }
        //Este controlador de Evento actualiza el progreso
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            resultLabel.Text = (e.ProgressPercentage.ToString() + " %");
        }
        //Este controlador de Eventos se refiere a los resultados de la operecion asincrona
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Cancelado!";
            }
            if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                resultLabel.Text = "Esta Hecho!";
            }
        }

        private void Form_Pruebas_Load(object sender, EventArgs e)
        {
            
        }
    }
}
