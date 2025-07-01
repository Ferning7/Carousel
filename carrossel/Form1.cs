using carrossel.UCs_Fotos;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace carrossel
{
    public partial class Form1 : Form
    {

        private Banners banners;
        public Form1()
        {
            InitializeComponent();
            CarregarBanners();
        }
      
        private void CarregarBanners()
        {
            string caminhoDasImagens = @"C:\Users\Aluno_Manha\source\repos\carrossel\carrossel\Resources";
            banners = new Banners(caminhoDasImagens);

            panelFotos.Controls.Clear();
         

            int x = 10;

            foreach (Image img in banners.Fotos)
            {
                pictureBox1.Image = img;
                panelFotos.Controls.Add(pictureBox1);
               
                //x += pictureBox1.Width + 10;
            }
        }

        private async Task SlideTransition(UserControl oldControl, UserControl newControl)
        {
            int panelWidth = panelFotos.Width;

            // Posição inicial
            oldControl.Location = new Point(0, 0);
            oldControl.Size = panelFotos.Size;

            newControl.Location = new Point(panelWidth, 0);
            newControl.Size = panelFotos.Size;

            panelFotos.Controls.Clear();
            panelFotos.Controls.Add(oldControl);
            panelFotos.Controls.Add(newControl);

            int step = 20;
            int delay = 10;

            // Enquanto a nova imagem não chegou ao destino
            while (newControl.Left > 0)
            {
                oldControl.Left -= step;     // antiga vai saindo para esquerda
                newControl.Left -= step;     // nova vai entrando da direita
                if (newControl.Left < 0)
                    newControl.Left = 0;
                if (oldControl.Left < -panelWidth)
                    oldControl.Left = -panelWidth;

                await Task.Delay(delay);
            }

            // Depois remove o antigo controle
            panelFotos.Controls.Remove(oldControl);
        }




        private async void Form1_Load(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            UC_Foto1 foto1 = new UC_Foto1();
            UC_Foto2 foto2 = new UC_Foto2();
            UC_Foto3 foto3 = new UC_Foto3();
            UC_Foto4 foto4 = new UC_Foto4();

            UserControl[] fotos = new UserControl[4] { foto1, foto2, foto3, foto4 };

            int sizeVetor = fotos.Length;
            int i = 0;  // melhor começar em 0 para indexar o array

            

            for (int segundos = 0; segundos < 30; segundos++)
            {
                int proximo = (i + 1) % sizeVetor;  // próximo índice com wrap-around

                if(i == 0)
                {
                    //pictureBox1.Image = fotos[i];
                }

                await SlideTransition(fotos[i], fotos[proximo]);

                i = proximo;

                await Task.Delay(4000);
            }

            // Se quiser fazer transição final para a foto anterior, cuidado com índice negativo:
            int anterior = (i - 1 + sizeVetor) % sizeVetor;
            await SlideTransition(fotos[i], fotos[anterior]);
        }






        private void addUc(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelFotos.Controls.Clear();
            panelFotos.Controls.Add(userControl);
            userControl.BringToFront();
        }
    }
}
