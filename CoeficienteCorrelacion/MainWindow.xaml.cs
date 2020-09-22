using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoeficienteCorrelacion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            #region variable declarations
            List<double> matrixX = new List<double>();
            List<double> matrixY = new List<double>();
            double sumX = 0;
            double sumY = 0;
            double medianX;
            double medianY;

            double ssX = 0;
            //ssX = Σ(x-medianX)^2
            double ssY = 0;
            //ssY = Σ(y-medianY)^2

            string xMatrixText = this.Matrix_X.Text;
            string yMatrixText = this.Matrix_Y.Text;

            string xString = "";
            string yString = "";
            #endregion

            #region populating matrix lists
            //Populates the matrixX list
            foreach (char item in xMatrixText)
            {
                if ((Char.IsDigit(item))|| (item == '.'))
                {
                    xString += item;
                }
                else
                {
                    if (xString != null)
                    {
                        matrixX.Add(Convert.ToDouble(xString));
                        xString = null;
                    }
                }
            }
            if (xString != null)
                matrixX.Add(Convert.ToDouble(xString));

            //Populates de matrixY list
            foreach (char item in yMatrixText)
            {
                if ((Char.IsDigit(item)) || (item == '.'))
                {
                    yString += item;
                }
                else
                {
                    if (yString != null)
                    {
                        matrixY.Add(Convert.ToDouble(yString));
                        yString = null;
                    }
                }
            }
            if (yString != null)
                matrixY.Add(Convert.ToDouble(yString));
            #endregion

            #region math
            //r = ∑((X - Mx)(Y - My)) / √((SSx)(SSy))
            foreach (double x in matrixX)
                sumX += x;
            foreach (double y in matrixY)
                sumY += y;

            medianX = (sumX / matrixX.Count);
            medianY = (sumY / matrixY.Count);

            foreach (double x in matrixX)
                ssX += Math.Pow((x - medianX), 2);
            foreach (double y in matrixY)
                ssY += Math.Pow((y - medianY), 2);

            double nominator = 0;
            for (int i = 0; i < matrixX.Count; i++)
                nominator += ((matrixX[i] - medianX) * (matrixY[i] - medianY));

            double denominator = Math.Sqrt(ssX * ssY);

            double correlationCoefficient = (nominator / denominator);

            this.Output.Text = Convert.ToString(correlationCoefficient);
            if (correlationCoefficient <= 0.5)
            {
                this.Correlation.Text = "Correlación negativa fuerte";
            }
            else
            {
                if ((correlationCoefficient < 0) && (correlationCoefficient > 0.5))
                {
                    this.Correlation.Text = "Correlación negativa débil";
                }
                else
                {
                    if (correlationCoefficient == 0)
                    {
                        this.Correlation.Text = "No hay correlación.";
                    }
                    else
                    {
                        if ((correlationCoefficient > 0) && (correlationCoefficient < 0.5))
                        {
                            this.Correlation.Text = "Correlación positiva débil";
                        }
                        else
                        {
                            this.Correlation.Text = "Correlación positiva fuerte";
                        }
                    }
                }
            }

            //System.IO.File.WriteAllText(@"C:\Users\Fernando\Desktop\xjson.json", Newtonsoft.Json.JsonConvert.SerializeObject(matrixX));
            //System.IO.File.WriteAllText(@"C:\Users\Fernando\Desktop\yjson.json", Newtonsoft.Json.JsonConvert.SerializeObject(matrixY));
            //
            //string verify = "";
            //verify += ("sumX:" + Convert.ToString(sumX) + "sumY:" + Convert.ToString(sumY));
            //verify += ("MedianX:" + Convert.ToString(medianX) + "\n " + "MedianY:" + Convert.ToString(medianY) + "\n " + "ssX:" + Convert.ToString(ssX) + "\n " + "ssY:" + Convert.ToString(ssY) + "\n " + "nominator:" + Convert.ToString(nominator) + "\n " + "denominator:" + Convert.ToString(denominator));
            //System.IO.File.WriteAllText(@"C:\Users\Fernando\Desktop\verify.txt", verify);

            #endregion
        }
    }
}
