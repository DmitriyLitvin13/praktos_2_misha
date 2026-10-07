using System;
using System.Windows;
using Microsoft.Win32;
using LibMas;
using Lib_5;
using Пример_таблицы_WPF;
namespace praktos2
{
    public partial class MainWindow : Window
    {
        private int[,] matrixData;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void BtnFill_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int rows = Convert.ToInt32(tbRows.Text);
                int columns = Convert.ToInt32(tbColumns.Text);
                MatrixHelper.InitMatrix(out matrixData, rows, columns, 1, 20);
                dgMatrix.ItemsSource = VisualArray.ToDataTable(matrixData).DefaultView;
                tbResult.Text = string.Empty;
            }
            catch
            {
                MessageBox.Show("Введите корректные целые числа для размеров матрицы!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            if (matrixData == null)
            {
                MessageBox.Show("Сначала заполните матрицу данными!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            double[] results = TaskSolver.CalculateOddRowAverages(matrixData);
            string outputText = string.Empty;
            for (int i = 0; i < results.Length; i++)
            {
                int humanRowIndex = (i * 2) + 1;
                outputText += $"Строка {humanRowIndex}: {results[i]:F2};  ";
            }

            tbResult.Text = outputText;
        }

        private void MenuClear_Click(object sender, RoutedEventArgs e)
        {
            matrixData = null;
            dgMatrix.ItemsSource = null;
            tbResult.Text = string.Empty;
        }
        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Разработчик: Бучмеюк Михаил\n" +
                "Практическая работа №2, Вариант №5\n" +
                "Задание: Для каждой строки матрицы с нечетным номером (1, 3, ...) найти среднее арифметическое ее элементов без использования условного оператора.",
                "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            if (matrixData == null)
            {
                MessageBox.Show(
                    "Сначала заполните или откройте матрицу!",
                    "Предупреждение",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            SaveFileDialog save = new SaveFileDialog();

            save.DefaultExt = ".txt";
            save.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            save.Title = "Сохранение матрицы";

            if (save.ShowDialog() == true)
            {
                try
                {
                    MatrixHelper.SaveMatrix(save.FileName, matrixData);

                    MessageBox.Show(
                        "Матрица успешно сохранена!",
                        "Сохранение",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show(
                        "Ошибка при сохранении файла!",
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }
        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.DefaultExt = ".txt";
            open.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            open.Title = "Открытие матрицы";

            if (open.ShowDialog() == true)
            {
                try
                {
                    matrixData = MatrixHelper.OpenMatrix(open.FileName);
                    dgMatrix.ItemsSource = VisualArray.ToDataTable(matrixData).DefaultView;
                    tbRows.Text = matrixData.GetLength(0).ToString();
                    tbColumns.Text = matrixData.GetLength(1).ToString();
                    tbResult.Text = string.Empty;

                    MessageBox.Show(
                        "Матрица успешно открыта!",
                        "Открытие",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show(
                        "Ошибка при открытии файла!",
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }
    }
}