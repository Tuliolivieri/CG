﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Primitivas_Graficas
{
    public partial class Form1 : Form
    {
        private int x1, x2, y1, y2;

        Ponto p1, p2;
        Poligono p_novo;

        List<Reta> retas;
        List<Circulo> circulos;
        List<Poligono> poligonos;
        public Form1()
        {
            InitializeComponent();
            x1 = x2 = y1 = y2 = -1;

            retas = new List<Reta>();
            circulos = new List<Circulo>();
            poligonos = new List<Poligono>();

            p_novo = new Poligono();

            p1 = new Ponto(-1, -1);
            p2 = new Ponto(-1, -1);

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;

            Bitmap bmp2 = new Bitmap(pictureBoxPoligonos.Width, pictureBoxPoligonos.Height);
            pictureBoxPoligonos.Image = bmp2;
        }

        private void EquacaoDaReta()    /// FUNCIONA
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);

            double dx = x2 - x1;
            double dy = y2 - y1;

            double m;
            if (dx == 0)
                m = 0;
            else
                m = dy / dx;
            int x, y;

            dx = Math.Abs(dx);
            dy = Math.Abs(dy);

            if (x1 == x2)
            {
                if (y1 > y2)
                {
                    int aux = y1;
                    y1 = y2;
                    y2 = aux;
                }

                for (y = y1; y <= y2; y++)
                {
                    if (isOnPictureBox((int)x1, (int)y, pictureBox1))
                        bmp.SetPixel(x1, y, Color.Black);
                }
            }
            else if (y1 == y2)
            {
                if (x1 > x2)
                {
                    int aux = x1;
                    x1 = x2;
                    x2 = aux;
                }

                for (x = x1; x <= x2; x++)
                {
                    if (isOnPictureBox((int)x, (int)y1, pictureBox1))
                        bmp.SetPixel(x, y1, Color.Black);
                }
            }
            else if (x2 > x1) /// 1, 2, 7 E 8 OCTANTES
            {
                if (y2 > y1) /// 1 E 2 OCTANTES  
                {
                    if (dx > dy) /// 1 OCTANTE
                    {
                        for (x = x1; x <= x2; x++)
                        {
                            y = (int)(y1 + m * (x - x1));
                            if (isOnPictureBox((int)x, (int)y, pictureBox1))
                                bmp.SetPixel(x, y, Color.Black);
                        }
                    }
                    else /// 2 OCTANTE
                    {
                        for (y = y1; y <= y2; y++)
                        {
                            x = (int)(x1 + (y - y1) / m);
                            if (isOnPictureBox((int)x, (int)y, pictureBox1))
                                bmp.SetPixel(x, y, Color.Black);
                        }
                    }
                }
                else /// 7 E 8 OCTANTES
                {
                    Console.WriteLine(dx + " - " + dy);
                    if (dx > dy) /// 8 OCTANTE
                    {
                        for (x = x1; x <= x2; x++)
                        {
                            y = (int)(y2 + m * (x - x2));
                            if (isOnPictureBox((int)x, (int)y, pictureBox1))
                                bmp.SetPixel(x, y, Color.Black);
                        }
                    }
                    else /// 7 OCTANTE
                    {
                        for (y = y1; y >= y2; y--)
                        {
                            x = (int)(x1 + (y - y1) / m);
                            if (isOnPictureBox((int)x, (int)y, pictureBox1))
                                bmp.SetPixel(x, y, Color.Black);
                        }
                    }
                }
            }
            else /// 3, 4, 5 E 5 OCTANTES
            {
                if (y2 > y1)
                {
                    if (dx > dy) /// 4 OCTANTE
                    {
                        for (x = x1; x >= x2; x--)
                        {
                            y = (int)(y1 + m * (x - x1));
                            if (isOnPictureBox((int)x, (int)y, pictureBox1))
                                bmp.SetPixel(x, y, Color.Black);
                        }
                    }
                    else /// 3 OCTANTE
                    {
                        for (y = y1; y <= y2; y++)
                        {
                            x = (int)(x2 + (y - y2) / m);
                            if (isOnPictureBox((int)x, (int)y, pictureBox1))
                                bmp.SetPixel(x, y, Color.Black);
                        }
                    }
                }
                else
                {
                    if (dx > dy) /// 5 OCTANTE
                    {
                        for (x = x1; x >= x2; x--)
                        {
                            y = (int)(y2 + m * (x - x2));
                            if (isOnPictureBox((int)x, (int)y, pictureBox1))
                                bmp.SetPixel(x, y, Color.Black);
                        }
                    }
                    else /// 6 OCTANTE
                    {
                        for (y = y1; y >= y2; y--)
                        {
                            x = (int)(x1 + (y - y1) / m);
                            if (isOnPictureBox((int)x, (int)y, pictureBox1))
                                bmp.SetPixel(x, y, Color.Black);
                        }
                    }
                }
            }
            pictureBox1.Image = bmp;
            x1 = x2 = y1 = y2 = -1;
        }

        private void DDA()  /// FUNCIONA
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);

            int length, i;
            double x, y, xinc, yinc;

            if (x1 > x2)
            {
                int aux = x1;
                x1 = x2;
                x2 = aux;
                aux = y1;
                y1 = y2;
                y2 = aux;
            }

            length = Math.Abs(x2 - x1);

            if (Math.Abs(y2 - y1) > length)
                length = Math.Abs(y2 - y1);

            xinc = (double)(x2 - x1) / length;
            yinc = (double)(y2 - y1) / length;

            x = x1;
            y = y1;

            if (y1 == y2)
            {
                for (x = x1; x <= x2; x++)
                {
                    if (isOnPictureBox((int)x, (int)y, pictureBox1))
                        bmp.SetPixel((int)x, (int)y, Color.Black);
                }
            }
            else if (x1 == x2)
            {
                if (y1 > y2)
                {
                    int aux = y1;
                    y1 = y2;
                    y2 = aux;
                }
                for (y = y1; y <= y2; y++)
                {
                    if (isOnPictureBox((int)x, (int)y, pictureBox1))
                        bmp.SetPixel((int)x, (int)y, Color.Black);
                }
            }
            else
            {
                while (x <= x2)
                {
                    if (isOnPictureBox((int)x, (int)y, pictureBox1))
                        bmp.SetPixel((int)x, (int)y, Color.Black);
                    x += xinc;
                    y += yinc;
                }
            }
            

            pictureBox1.Image = bmp;
            x1 = x2 = y1 = y2 = -1;
        }

        public void Bresenham(int x1, int x2, int y1, int y2, Bitmap bmp, Color cor, PictureBox pb) /// FUNCIONA
        {
            int dx, dy, incE, incNE, declive, d, x, y;

            dx = (int) (x2 - x1);
            dy = (int) (y2 - y1);
            declive = 1;

            /*if (cor == Color.Red)
                Console.WriteLine("COR: RED");

            Console.WriteLine("P: " + x1 + ", " + x2 + ", " + y1 + ", " + y2);*/

            if(Math.Abs(dx) > Math.Abs(dy)) // FOR EM RELAÇÃO A X
            {
                if (x1 > x2)
                {
                    // INVERTE OS PONTOS E REFAZ AS PERGUNTAS
                    Bresenham(x2, x1, y2, y1, bmp, cor, pb);
                }
                else
                {
                    if (dy < 0)
                    {
                        // X, -Y
                        declive = -1;
                        dy = -dy;
                    }

                    incE = 2 * dy;
                    incNE = 2 * dy - 2 * dx;
                    d = incE - dx;

                    y = (int)y1;

                    for (x = (int)x1; x <= x2; x++)
                    {
                        if (isOnPictureBox(x, y, pb))
                            bmp.SetPixel(x, y, cor);

                        if (d < 0)
                            d += incE;
                        else
                        {
                            d += incNE;
                            y += declive;
                        }
                    }
                }
            }
            else // FOR EM RELAÇÃO A Y
            {
                if(y1 > y2)
                {
                    Bresenham(x2, x1, y2, y1, bmp, cor, pb);
                }
                else
                {
                    if(dx < 0)
                    {
                        declive = -1;
                        dx = -dx;
                    }

                    incE = 2 * dx;
                    incNE = 2 * dx - 2 * dy;
                    d = incE - dy;

                    x = (int) x1;
                    for (y = (int) y1; y <= y2; ++y)
                    {
                        if(isOnPictureBox(x, y, pb))
                            bmp.SetPixel(x, y, cor);
                        if (d < 0) // escolhe incE
                            d += incE;
                        else
                        {   // escolhe incNE
                            d += incNE;
                            x += declive;
                        }
                    }
                }
            }

        }

        private void equacaoGeralCircunferencia(Ponto centro, int raio)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);

            double y, limite;

            y = raio;
            limite = raio / Math.Sqrt(2);

            for(int x = 0; x <= limite; x++)
            {
                desenhaSimetria8(x, (int) y, centro, bmp);
                y = Math.Sqrt(Math.Pow(raio, 2) - Math.Pow(x, 2));
            }

            pictureBox1.Image = bmp;
        }

        private void circunferenciaTrigonometrica(Ponto centro, int raio)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);

            double x, y, angulo, limite;

            x = 0;
            y = raio;
            limite = raio / Math.Sqrt(2);

            desenhaSimetria8((int)x, (int)y, centro, bmp);

            for(angulo = 90; angulo >= 45; angulo -= 0.01)
            {
                x = raio * Math.Cos(angulo * 180 / Math.PI);
                y = raio * Math.Sin(angulo * 180 / Math.PI);
                desenhaSimetria8((int)x, (int)y, centro, bmp);
            }

            pictureBox1.Image = bmp;
        }

        private void pontoMedioCircunferencia(Ponto centro, int raio)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);

            int x, y;
            double d = 1 - raio;
            for (x = 0, y = (int)raio; y >= x; ++x)
            {
                desenhaSimetria8(x, y, centro, bmp);
                if (d < 0)
                    d += 2 * x + 3;
                else
                {
                    d += (2 * x - 2 * y) + 5;
                    y--;
                }
            }

            pictureBox1.Image = bmp;
        }
        private void desenhaSimetria8(int dx, int dy, Ponto Centro, Bitmap bmp)
        {
            if (isOnPictureBox(Centro.X + dy, Centro.Y - dx, pictureBox1)) // 1o Octante
                bmp.SetPixel(Centro.X + dy, Centro.Y - dx, Color.Black);
            if (isOnPictureBox(Centro.X + dx, Centro.Y - dy, pictureBox1)) // 2o Octante
                bmp.SetPixel(Centro.X + dx, Centro.Y - dy, Color.Black);
            if (isOnPictureBox(Centro.X - dx, Centro.Y - dy, pictureBox1)) // 3o Octante
                bmp.SetPixel(Centro.X - dx, Centro.Y - dy, Color.Black);
            if (isOnPictureBox(Centro.X - dy, Centro.Y - dx, pictureBox1)) // 4o Octante
                bmp.SetPixel(Centro.X - dy, Centro.Y - dx, Color.Black);
            if (isOnPictureBox(Centro.X - dy, Centro.Y + dx, pictureBox1)) // 5o Octante
                bmp.SetPixel(Centro.X - dy, Centro.Y + dx, Color.Black);
            if (isOnPictureBox(Centro.X - dx, Centro.Y + dy, pictureBox1)) // 6o Octante
                bmp.SetPixel(Centro.X - dx, Centro.Y + dy, Color.Black);
            if (isOnPictureBox(Centro.X + dx, Centro.Y + dy, pictureBox1)) // 7o Octante
                bmp.SetPixel(Centro.X + dx, Centro.Y + dy, Color.Black);
            if (isOnPictureBox(Centro.X + dy, Centro.Y + dx, pictureBox1)) // 8o Octante
                bmp.SetPixel(Centro.X + dy, Centro.Y + dx, Color.Black);
        }

        private void desenhaSimetria4(int cx, int cy, int dx, int dy, Bitmap bmp)
        {
            Console.WriteLine("ENTROU");
            Console.WriteLine("X: " + (cx + dx) + " - Y: " + (cy + dy));

            if (isOnPictureBox(cx + dx, cy + dy, pictureBox1))
                bmp.SetPixel(cx + dx, cy + dy, Color.Black);

            if (isOnPictureBox(cx - dx, cy + dy, pictureBox1))
                bmp.SetPixel(cx - dx, cy + dy, Color.Black);

            if (isOnPictureBox(cx - dx, cy - dy, pictureBox1))
                bmp.SetPixel(cx - dx, cy - dy, Color.Black);

            if (isOnPictureBox(cx + dx, cy - dy, pictureBox1))
                bmp.SetPixel(cx + dx, cy - dy, Color.Black);
        }

        public void elipsePontoMedio(int cx, int cy, int dx, int dy)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);

                int x, y;
                double d1, d2;

                // valores iniciais
                x = 0;
                y = dy;
                d1 = dy * dx - dx * dx * dy + dx * dx / 4.0;

                desenhaSimetria4(cx, cy, x, y, bmp);
                while (dx * dx * (y - 0.5) > dy * dy * (x + 1))
                {
                    // regiao 1
                    if (d1 < 0)
                    {
                        d1 += dy * dy * (2 * x + 3);
                        x++;
                    }
                    else
                    {
                        d1 = d1 + dy * dy * (2 * x + 3) + dx * dx * (-2 * y + 2);
                        x++;
                        y--;
                    }
                    desenhaSimetria4(cx, cy, x, y, bmp);
                }

                d2 = dy * dy * (x + 0.5) * (x + 0.5) + dx * dx * (y - 1) * (y - 1) - dx * dx * dy * dy;
                while (y > 0)
                {
                    /* Regi~ao 2 */
                    if (d2 < 0)
                    {
                        d2 = d2 + dy * dy * (2 * x + 2) + dx * dx * (-2 * y + 3);
                        x++;
                        y--;
                    }
                    else
                    {
                        d2 = d2 + dx * dx * (-2 * y + 3);
                        y--;
                    }/*end if*/
                desenhaSimetria4(cx, cy, x, y, bmp);
            }
            pictureBox1.Image = bmp;
        }


        private void mouseClique(object sender, MouseEventArgs e)
        {
            if(x1 == -1)
            {
                x1 = e.X;
                y1 = e.Y;
            }
            else
            {
                x2 = e.X;
                y2 = e.Y;

                if (cbEqReal.CheckState == CheckState.Checked)
                {
                    retas.Add(new Reta(new Ponto(x1, x2), new Ponto(y1, y2)));
                    EquacaoDaReta();
                }
                else if(cbDeclive.CheckState == CheckState.Checked)
                {
                    retas.Add(new Reta(new Ponto(x1, x2), new Ponto(y1, y2)));
                    DDA();
                }
                else if(cbPtMedio.CheckState == CheckState.Checked)
                {
                    Bitmap bmp = new Bitmap(pictureBox1.Image);

                    retas.Add(new Reta(new Ponto(x1, x2), new Ponto(y1, y2)));
                    Bresenham(x1, x2, y1, y2, bmp, Color.Black, pictureBox1);

                    pictureBox1.Image = bmp;
                }
                else if(cbCircEqGeral.CheckState == CheckState.Checked)
                {
                    int raio = (int)Math.Abs(Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2)));
                    circulos.Add(new Circulo(new Ponto(x1, y1), raio));

                    equacaoGeralCircunferencia(circulos.Last().Centro, raio);
                }
                else if(cbCircTrig.CheckState == CheckState.Checked)
                {
                    int raio = (int)Math.Abs(Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2)));
                    circulos.Add(new Circulo(new Ponto(x1, y1), raio));

                    circunferenciaTrigonometrica(circulos.Last().Centro, raio);
                }
                else if(cbCircPM.CheckState == CheckState.Checked)
                {
                    int raio = (int) Math.Abs(Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2)));
                    circulos.Add(new Circulo(new Ponto(x1, y1), raio));

                    pontoMedioCircunferencia(circulos.Last().Centro, raio);
                }
                else if(cbElipse.CheckState == CheckState.Checked)
                {
                    int raio = (int) Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
                    elipsePontoMedio(x1, y1, Math.Abs(x1 - x2), Math.Abs(y1 - y2));
                }
                x1 = x2 = y1 = y2 = -1;

                exibeRetas();
                exibeCirculos();
            }
        }

        private void DgvPoligonos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            List<Ponto> vertices = poligonos.ElementAt(e.RowIndex).Vertices;

            dgvVertices.Rows.Clear();

            foreach(Ponto p in vertices)
            {
                dgvVertices.Rows.Add();
                dgvVertices.Rows[dgvVertices.Rows.Count - 1].Cells[0].Value = p.X.ToString();
                dgvVertices.Rows[dgvVertices.Rows.Count - 1].Cells[1].Value = p.Y.ToString();
            }

            desenharPoligonoMA(poligonos.ElementAt(e.RowIndex));
        }

        private void mouseCliquePoligono(object sender, MouseEventArgs e)
        {
            if(p1.X == -1)
            {
                p1.X = e.X;
                p1.Y = e.Y;

                p_novo = new Poligono();
                p_novo.addVertice(new Ponto(p1.X, p1.Y));
            }
            else
            {
                p2.X = e.X;
                p2.Y = e.Y;

                p_novo.addVertice(new Ponto(p2.X, p2.Y));

                if(e.Button == MouseButtons.Right && p_novo.Vertices.Count() > 2)
                {
                    Bitmap bmp = new Bitmap(pictureBoxPoligonos.Image);

                    Bresenham(p1.X, p2.X, p1.Y, p2.Y, bmp, Color.Black, pictureBoxPoligonos);
                    Bresenham(p2.X, p_novo.Vertices.First().X , p2.Y, p_novo.Vertices.First().Y, bmp, Color.Black, pictureBoxPoligonos);

                    p_novo.addVertice(new Ponto(p_novo.Vertices.First().X, p_novo.Vertices.First().Y));

                    pictureBoxPoligonos.Image = bmp;
                }
                else if(p2 != p1 && p2.X != -1)
                {
                    Bitmap bmp = new Bitmap(pictureBoxPoligonos.Image);

                    Bresenham(p1.X, p2.X, p1.Y, p2.Y, bmp, Color.Black, pictureBoxPoligonos);
                    p1 = p2;
                    p2 = new Ponto(-1, -1);

                    /*for(int i = 0; i < p_novo.Vertices.Count - 1; i++)
                    {
                        p1 = p_novo.Vertices.ElementAt(i);
                        p2 = p_novo.Vertices.ElementAt(i + 1);

                        Bresenham(p1.X, p2.X, p1.Y, p2.Y, bmp);
                    }*/

                    pictureBoxPoligonos.Image = bmp;
                }
                if(p_novo.isFechado())
                {
                    poligonos.Add(p_novo);
                    p1.X = p1.Y = p2.X = p2.Y = -1;
                    p_novo = new Poligono();
                    dgvPoligonos.Rows.Add(poligonos.Count.ToString());
                }
            }
        }

        public void exibeRetas()
        {
            for(int i = 0; i < retas.Count; i++)
            {
                Reta r = retas.ElementAt(i);

                Console.WriteLine("Reta [" + (i + 1) + "]: { X1: "+ r.Inicio.X + ", Y1: " + r.Inicio.Y + ", X2: " + r.Fim.X + ", Y2: " + r.Fim.Y + "}");
            }
        }

        public void translacao(double x, double y)
        {
            double[,] mt = new double[3, 3];

            mt[0, 0] = mt[1, 1] = mt[2, 2] = 1;

            mt[0, 2] = x;
            mt[1, 2] = y;

            /*Console.WriteLine(mt[0, 0].ToString() + mt[0, 1].ToString() + mt[0, 2].ToString());
            Console.WriteLine(mt[1, 0].ToString() + mt[1, 1].ToString() + mt[1, 2].ToString());
            Console.WriteLine(mt[2, 0].ToString() + mt[2, 1].ToString() + mt[2, 2].ToString());*/

            Poligono pol = poligonos.ElementAt(dgvPoligonos.CurrentRow.Index);

            pol.setMA(multiplicaMatriz(mt, pol.getMA()));
            double[,] MA = pol.getMA();

            Ponto p;

            for (int i = 0; i < pol.Vertices.Count; i++)
            {
                p = pol.Vertices.ElementAt(i);

                Console.WriteLine((p.X * MA[0, 0]/* + p.Y * MA[0, 1] + MA[0, 2]*/).ToString());
                p.X = (int)(p.X * MA[0, 0] + p.Y * MA[0, 1] + MA[0, 2]);
                p.Y = (int)(p.X * MA[1, 0] + p.Y * MA[1, 1] + MA[1, 2]);
            }
            /*mt = poligonos.ElementAt(dgvPoligonos.CurrentRow.Index).getMA();
            Console.WriteLine(mt[0, 0].ToString() + mt[0, 1].ToString() + mt[0, 2].ToString());
            Console.WriteLine(mt[1, 0].ToString() + mt[1, 1].ToString() + mt[1, 2].ToString());
            Console.WriteLine(mt[2, 0].ToString() + mt[2, 1].ToString() + mt[2, 2].ToString());*/

            redesenhaPoligonos();
        }

        private void BtTransladar_Click(object sender, EventArgs e)
        {
            double x, y;

            if (tbTransX.Text.Length > 0)
                double.TryParse(tbTransX.Text, out x);
            else
                x = 0;
            if (tbTransY.Text.Length > 0)
                double.TryParse(tbTransY.Text, out y);
            else
                y = 0;

            translacao(x, y);
            /*double[,] mt = new double[3, 3];

            mt[0, 0] = mt[1, 1] = mt[2, 2] = 1;

            if (tbTransX.Text.Length > 0)
                double.TryParse(tbTransX.Text, out mt[0, 2]);
            else
                mt[0, 2] = 0;
            if (tbTransY.Text.Length > 0)
                double.TryParse(tbTransY.Text, out mt[1, 2]);
            else
                mt[1, 2] = 0;

            /*Console.WriteLine(mt[0, 0].ToString() + mt[0, 1].ToString() + mt[0, 2].ToString());
            Console.WriteLine(mt[1, 0].ToString() + mt[1, 1].ToString() + mt[1, 2].ToString());
            Console.WriteLine(mt[2, 0].ToString() + mt[2, 1].ToString() + mt[2, 2].ToString());*/

            /*Poligono pol = poligonos.ElementAt(dgvPoligonos.CurrentRow.Index);

            pol.setMA(multiplicaMatriz(mt, pol.getMA()));
            double[,] MA = pol.getMA();

            Ponto p;

            for(int i = 0; i < pol.Vertices.Count; i++)
            {
                p = pol.Vertices.ElementAt(i);

                Console.WriteLine((p.X * MA[0, 0]/* + p.Y * MA[0, 1] + MA[0, 2]).ToString());
                p.X = (int)(p.X * MA[0, 0] + p.Y * MA[0, 1] + MA[0, 2]);
                p.Y = (int)(p.X * MA[1, 0] + p.Y * MA[1, 1] + MA[1, 2]);
            }
            /*mt = poligonos.ElementAt(dgvPoligonos.CurrentRow.Index).getMA();
            Console.WriteLine(mt[0, 0].ToString() + mt[0, 1].ToString() + mt[0, 2].ToString());
            Console.WriteLine(mt[1, 0].ToString() + mt[1, 1].ToString() + mt[1, 2].ToString());
            Console.WriteLine(mt[2, 0].ToString() + mt[2, 1].ToString() + mt[2, 2].ToString());*/

            //redesenhaPoligonos();
            /*double[,] mat1 = new double[3, 3];
            double[,] mat2 = new double[3, 3];

            /*for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                    mat1[i, j] = mat2[i, j] = i + j;
            }

            mat1[0, 0] = 1;
            mat1[0, 1] = 0;
            mat1[0, 2] = -2;
            mat1[1, 0] = 0;
            mat1[1, 1] = 1;
            mat1[1, 2] = 3;
            mat1[2, 0] = 0;
            mat1[2, 1] = 0;
            mat1[2, 2] = 1;

            mat2[0, 0] = -0.5;
            mat2[0, 1] = 0;
            mat2[0, 2] = 0.5;
            mat2[1, 0] = 0;
            mat2[1, 1] = -1;
            mat2[1, 2] = 1;
            mat2[2, 0] = 0;
            mat2[2, 1] = 0;
            mat2[2, 2] = 1;

            multiplicaMatriz(mat1, mat2);*/
        }

        public void exibeCirculos()
        {
            for(int i = 0; i < circulos.Count; i++)
            {
                Circulo c = new Circulo(circulos.ElementAt(i).Centro, circulos.ElementAt(i).Raio);

                Console.WriteLine("Circulo [" + (i + 1) + "]: { CentroX: " + c.Centro.X + ", CentroY: " + c.Centro.Y + ", Raio:" + c.Raio + "}");
            }
        }

        private void escala(double ex, double ey)
        {
            double[,] mt = new double[3, 3];

            mt[2, 2] = 1;

            mt[0, 0] = ex;
            mt[1, 1] = ey;

            Poligono pol = poligonos.ElementAt(dgvPoligonos.CurrentRow.Index);

            pol.setMA(multiplicaMatriz(mt, pol.getMA()));
            double[,] MA = pol.getMA();

            Ponto p;

            for (int i = 0; i < pol.Vertices.Count; i++)
            {
                p = pol.Vertices.ElementAt(i);
                p.X = (int)(p.X * MA[0, 0] + p.Y * MA[0, 1] + MA[0, 2]);
                p.Y = (int)(p.X * MA[1, 0] + p.Y * MA[1, 1] + MA[1, 2]);
            }
            redesenhaPoligonos();
        }

        private void BtEscala_Click(object sender, EventArgs e)
        {
            double[,] mt = new double[3, 3];

            double ex, ey;

            mt[2, 2] = 1;

            if (tbEscX.Text.Length > 0)
                double.TryParse(tbEscX.Text, out ex);
            else
                ex = 0;
            if (tbEscY.Text.Length > 0)
                double.TryParse(tbEscY.Text, out ey);
            else
                ey = 0;

            Ponto p = poligonos.ElementAt(dgvPoligonos.CurrentRow.Index).Vertices.First();

            //translacao(p.X * (-1), -p.Y * (-1));
            escala(ex, ey);
            //translacao(p.X, p.Y);

            /*Console.WriteLine(mt[0, 0].ToString() + mt[0, 1].ToString() + mt[0, 2].ToString());
            Console.WriteLine(mt[1, 0].ToString() + mt[1, 1].ToString() + mt[1, 2].ToString());
            Console.WriteLine(mt[2, 0].ToString() + mt[2, 1].ToString() + mt[2, 2].ToString());*/

            /*Poligono pol = poligonos.ElementAt(dgvPoligonos.CurrentRow.Index);

            pol.setMA(multiplicaMatriz(mt, pol.getMA()));
            double[,] MA = pol.getMA();

            Ponto p;

            for (int i = 0; i < pol.Vertices.Count; i++)
            {
                p = pol.Vertices.ElementAt(i);

                Console.WriteLine((p.X * MA[0, 0]/* + p.Y * MA[0, 1] + MA[0, 2]).ToString());
                p.X = (int)(p.X * MA[0, 0] + p.Y * MA[0, 1] + MA[0, 2]);
                p.Y = (int)(p.X * MA[1, 0] + p.Y * MA[1, 1] + MA[1, 2]);
            }
            /*mt = poligonos.ElementAt(dgvPoligonos.CurrentRow.Index).getMA();
            Console.WriteLine(mt[0, 0].ToString() + mt[0, 1].ToString() + mt[0, 2].ToString());
            Console.WriteLine(mt[1, 0].ToString() + mt[1, 1].ToString() + mt[1, 2].ToString());
            Console.WriteLine(mt[2, 0].ToString() + mt[2, 1].ToString() + mt[2, 2].ToString());*/

            //redesenhaPoligonos();
        }

        public bool isOnPictureBox(int x, int y, PictureBox pb)
        {
            return x >= 0 && x < pb.Width && y >= 0 && y < pb.Height;
        }

        private void desenharPoligonoMA(Poligono pol)
        {
            //Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Bitmap bmp = new Bitmap(pictureBoxPoligonos.Image);
            Ponto p1, p2;

            for (int i = 0; i < pol.Vertices.Count() - 1; i++)
            {
                p1 = new Ponto(pol.Vertices.ElementAt(i).X, pol.Vertices.ElementAt(i).Y);
                p2 = new Ponto(pol.Vertices.ElementAt(i + 1).X, pol.Vertices.ElementAt(i + 1).Y);
                Bresenham(p1.X, p2.X, p1.Y, p2.Y, bmp, Color.Red, pictureBoxPoligonos);
                //pictureBox1.Image = bmp;
            }

            pictureBoxPoligonos.Image = bmp;
        }

        private double[,] multiplicaMatriz(double[,] mat1, double[,] mat2)
        {
            double[,] mat = new double[3, 3];
            int i, j, k;
            double val;

            for(i = 0; i < 3; i++)
            {
                for(j = 0; j < 3; j++)
                {
                    val = 0;
                    for(k = 0; k < 3; k++)
                    {
                        val += mat1[i, k] * mat2[k, j];
                    }
                    mat[i, j] = val;
                }
            }

            return mat;
        }

        public void redesenhaPoligonos()
        {
            Bitmap bmp = new Bitmap(pictureBoxPoligonos.Width, pictureBoxPoligonos.Height);
            Ponto p1, p2, v;
            Poligono pol;
            double[,] MA;


            for (int i = 0; i < poligonos.Count; i++)
            {
                pol = poligonos.ElementAt(i);
                MA = pol.getMA();

                for (int j = 0; j < pol.Vertices.Count() - 1; j++)
                {
                    p1 = new Ponto(pol.Vertices.ElementAt(j).X, pol.Vertices.ElementAt(j).Y);
                    p2 = new Ponto(pol.Vertices.ElementAt(j + 1).X, pol.Vertices.ElementAt(j + 1).Y);

                    /*Console.WriteLine(MA[0, 0].ToString() + MA[0, 1].ToString() + MA[0, 2].ToString());
                    Console.WriteLine(MA[1, 0].ToString() + MA[1, 1].ToString() + MA[1, 2].ToString());
                    Console.WriteLine(MA[2, 0].ToString() + MA[2, 1].ToString() + MA[2, 2].ToString());
                    Console.WriteLine("P1: " + p1.X + ", " + p1.Y);
                    Console.WriteLine("P2: " + p2.X + ", " + p2.Y);

                    p1.X = (int)(p1.X * MA[0, 0] + p1.Y * MA[0, 1] + MA[0, 2]);
                    p1.Y = (int)(p1.X * MA[1, 0] + p1.Y * MA[1, 1] + MA[1, 2]);

                    p2.X = (int)(p2.X * MA[0, 0] + p2.Y * MA[0, 1] +  MA[0, 2]);
                    p2.Y = (int)(p2.X * MA[1, 0] + p2.Y * MA[1, 1] +  MA[1, 2]);*/

                    Bresenham(p1.X, p2.X, p1.Y, p2.Y, bmp, Color.Black, pictureBoxPoligonos);
                    //pictureBox1.Image = bmp;
                }
            }

            pictureBoxPoligonos.Image = bmp;
        }
    }
}
