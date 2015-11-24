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

namespace WordForWord
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        //List<Label> fieldOfLabels = new List<Label>();
        private Dictionary<CoOrd, Label> _fieldOfLabels = new Dictionary<CoOrd, Label>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeFieldOfLabels();

            GameLogic();
        }



        private void InitializeFieldOfLabels()
        {
            _fieldOfLabels.Add(new CoOrd(0, 0), label00);
            _fieldOfLabels.Add(new CoOrd(0, 1), label01);
            _fieldOfLabels.Add(new CoOrd(0, 4), label04);
            _fieldOfLabels.Add(new CoOrd(0, 3), label03);
            _fieldOfLabels.Add(new CoOrd(0, 2), label02);
            _fieldOfLabels.Add(new CoOrd(1, 0), label10);
            _fieldOfLabels.Add(new CoOrd(1, 1), label11);
            _fieldOfLabels.Add(new CoOrd(1, 4), label14);
            _fieldOfLabels.Add(new CoOrd(1, 3), label13);
            _fieldOfLabels.Add(new CoOrd(1, 2), label12);
            _fieldOfLabels.Add(new CoOrd(2, 0), label20);
            _fieldOfLabels.Add(new CoOrd(2, 1), label21);
            _fieldOfLabels.Add(new CoOrd(2, 4), label24);
            _fieldOfLabels.Add(new CoOrd(2, 3), label23);
            _fieldOfLabels.Add(new CoOrd(2, 2), label22);
            _fieldOfLabels.Add(new CoOrd(3, 0), label30);
            _fieldOfLabels.Add(new CoOrd(3, 1), label31);
            _fieldOfLabels.Add(new CoOrd(3, 4), label34);
            _fieldOfLabels.Add(new CoOrd(3, 3), label33);
            _fieldOfLabels.Add(new CoOrd(3, 2), label32);
            _fieldOfLabels.Add(new CoOrd(4, 0), label40);
            _fieldOfLabels.Add(new CoOrd(4, 1), label41);
            _fieldOfLabels.Add(new CoOrd(4, 4), label44);
            _fieldOfLabels.Add(new CoOrd(4, 3), label43);
            _fieldOfLabels.Add(new CoOrd(4, 2), label42);
        }

        public void FieldUpdate(Stack<CoOrd> coOrds)
        {
            foreach (var coOrd in coOrds)
            {
                _fieldOfLabels[coOrd].Background = Brushes.Transparent;
            }

            foreach (var coOrd in coOrds.Reverse())
            {
                if (_fieldOfLabels.ContainsKey(coOrd))
                {
                    _fieldOfLabels[coOrd].Background = Brushes.Red;
                    this.Show();
                    //MessageBox.Show(coOrd.ToString());
                }
            }
            UpdateLayout();
        }

        public void GameLogic()
        {
            char[][] inputArray = new char[][]
            {
                new char[] {'ь', 'с', 'е', 'р', 'д'},
                new char[] {'т', 'а', 'л', 'е', 'ц'},
                new char[] {'г', 'о', 'в', 'у', 'к'},
                new char[] {'л', 'о', 'м', 'н', 'у'},
                new char[] {'б', 'о', 'т', 'л', 'а'}
            };
            //char[][] inputArray = new char[][] // село, гол, лаг, стог, вол, гот, сел, сало
            //{
            //    new char[] {'ь', 'с', 'е'},
            //    new char[] {'т', 'а', 'л'},
            //    new char[] {'г', 'о', 'в'},
            //};


            Logic.GameField gameField = new Logic.GameField(inputArray, this);
            gameField.ArrayDetour();
        }
    }

    public struct CoOrd
    {
        public int x, y;

        public CoOrd(int p1, int p2)
        {
            x = p1;
            y = p2;
        }

        public override string ToString()
        {
            return "CoOrd: (" + x + ";" + y + ")";
        }
    }
}