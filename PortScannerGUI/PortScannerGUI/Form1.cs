using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortScannerGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        
                    // 1. Добавь в самое начало файла (над namespace), если их там нет:
                    // using System.Net.Sockets;
                    // using System.Threading.Tasks;

        private async void btnScan_Click(object sender, EventArgs e)
        {
            // Берем IP из текстового поля
            string ip = txtIp.Text.Trim();

            // Очищаем окно логов от старых записей
            txtLog.Clear();
            txtLog.AppendText($"Начинаем сканирование {ip}...\n\n");

            // Перебираем порты от 1 до 10000
            for (int port = 1; port <= 1000; port++)
            {
                // Вызываем нашу функцию проверки (с await, чтобы окно не зависало)
                bool isOpen = await CheckPortAsync(ip, port);

                if (isOpen)
                {
                    // Если порт открыт, красим текст в зеленый и пишем в лог
                    txtLog.SelectionColor = System.Drawing.Color.Green;
                    txtLog.AppendText($"[+] Порт {port} ОТКРЫТ\n");

                    // Автоматически прокручиваем вниз
                    txtLog.ScrollToCaret();
                }
            }

            txtLog.SelectionColor = System.Drawing.Color.Black;
            txtLog.AppendText("\nСканирование завершено.");
        }

        // Это вспомогательная функция для проверки одного порта
        private async Task<bool> CheckPortAsync(string ip, int port)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    var connectTask = client.ConnectAsync(ip, port);
                    // Ждем 300 миллисекунд
                    if (await Task.WhenAny(connectTask, Task.Delay (300) == connectTask)
                    {
                        await connectTask; // Если ошибки нет - порт открыт
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
    }

