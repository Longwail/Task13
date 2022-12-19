using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Practicum13_Task1_WF
{
    public partial class Form1 : Form
    {
        List<Trans> transport = new List<Trans>();
        string brand;
        int number = 0, speed = 0, loadCapacity = 0;
        bool isCarriage = true;
        bool isTrailer = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int loadCap = (int)numericUpDown5.Value;
            richTextBox2.Clear();
            int count = 0;
            foreach (var t in transport)
            {
                if (t.LoadCapacity == loadCap) 
                {
                    richTextBox2.Text += t.OutInfo();
                    count++;
                } 
            }
            if (count == 0) MessageBox.Show("Подходящих т/с не найдено", "Ошибка!");
        }

		private void button6_Click(object sender, EventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "Text files(*.txt)|*.txt";
            dlg.InitialDirectory = "E:\\УП\\Practicum13\\Practicum13\\bin\\Debug";
            
            if (dlg.ShowDialog() == DialogResult.Cancel) return;

            string fileName = dlg.FileName;
            string filePath = Path.GetFullPath(fileName);
			string[] arr = File.ReadAllLines(filePath);

			for (int i = 0; i < arr.Length; i++) {
				if (arr[i] == "Автомобиль") 
                {
					brand = arr[i + 1];
					number = Convert.ToInt32(arr[i + 2]);
					speed = Convert.ToInt32(arr[i + 3]);
					loadCapacity = Convert.ToInt32(arr[i + 4]);
					Car car = new Car(brand, number, speed, loadCapacity);
					transport.Add(car);
					richTextBox1.Text += car.OutInfo();
				}

                else if (arr[i] == "Мотоцикл") 
                {
                    brand = arr[i + 1];
                    number = Convert.ToInt32(arr[i + 2]);
					speed = Convert.ToInt32(arr[i + 3]);
                    string answ = arr[i + 4];
                            switch (answ)
                            {
                                case "Да":
                                    isCarriage = true;
                                    loadCapacity = Convert.ToInt32(arr[i + 5]);
                                    break;
                                case "Нет":
                                    isCarriage = false;
                                    break; 
                            }
                    Motorcycle bike = new Motorcycle(brand, number, speed, loadCapacity, isCarriage);
                    bike.GetLoadCapacity();
                    transport.Add(bike);
                    richTextBox1.Text += bike.OutInfo();
				}

                else if (arr[i] == "Грузовик") 
                {
                    brand = arr[i + 1];
                    number = Convert.ToInt32(arr[i + 2]);
					speed = Convert.ToInt32(arr[i + 3]);
                    loadCapacity = Convert.ToInt32(arr[i + 4]);

                    string str = arr[i + 5];
                            switch (str)
                            {
                                case "Да":
                                    isTrailer = true;
                                    break;
                                case "Нет":
                                    isTrailer = false;
                                    break;
                            }
                    Truck truck = new Truck(brand, number, speed, loadCapacity, isTrailer);
                    truck.GetLoadCapacity();
                    transport.Add(truck);
                    richTextBox1.Text += truck.OutInfo();
				}
			}
		}
    }

    //абстрактный класс
    abstract class Trans
    {
        public abstract int LoadCapacity { get; }
        public abstract string OutInfo();
        public abstract string GetLoadCapacity();
    }

    //реализующие классы
    class Car : Trans
    {
        string brand;
        int number;
        int speed;
        int loadCapacity;
        public override int LoadCapacity
        {
            get { return loadCapacity; }
        }

        public Car(string brand, int number, int speed, int loadCapacity)
        {
            this.brand = brand;
            this.number = number;
            this.speed = speed;
            this.loadCapacity = loadCapacity;
        }

        public override string OutInfo()
        {
            return $"\n------------------------\n" +
                $"Информация о машине:\n" +
                $"Марка: {brand}\n" +
                $"Номер: {number}\n" +
                $"Скорость: {speed}\n" +
                $"Грузоподъемность: {loadCapacity}";
        }
        public override string GetLoadCapacity()
        {
            return $"Грузоподъемность: {loadCapacity}";
        }
    }

    class Motorcycle : Trans
    {
        string brand;
        int number;
        int speed;
        int loadCapacity;
        bool isСarriage;
        public override int LoadCapacity
        {
            get { return loadCapacity; }
        }

        public Motorcycle(string brand, int number, int speed, int loadCapacity, bool isCarriage)
        {
            this.brand = brand;
            this.number = number;
            this.speed = speed;
            this.loadCapacity = loadCapacity;
            this.isСarriage = isCarriage;
        }

        public override string OutInfo()
        {
            string carriage = (isСarriage) ? "Да" : "Нет"; ;
            return $"\n------------------------\n" +
                $"Информация о мотоцикле:\n" +
                $"Марка: {brand}\n" +
                $"Номер: {number}\n" +
                $"Скорость: {speed}\n" +
                $"Грузоподъемность: {loadCapacity}\n" +
                $"Есть коляска: {carriage}";
        }

        public override string GetLoadCapacity()
        {
            if (isСarriage)
            {
                return $"Так как есть коляска, то грузоподъемность мотоцикла {loadCapacity}";
            }
            else
            {
                loadCapacity = 0;
                return $"Так как коляски нет, то грузоподъемность мотоцикла {loadCapacity}";
            }
        }
    }

    class Truck : Trans
    {
        string brand;
        int number;
        int speed;
        int loadCapacity;
        bool isTrailer;
        public override int LoadCapacity
        {
            get { return loadCapacity; }
        }
        public Truck(string brand, int number, int speed, int loadCapacity, bool isTrailer)
        {
            this.brand = brand;
            this.number = number;
            this.speed = speed;
            this.loadCapacity = loadCapacity;
            this.isTrailer = isTrailer;
        }
        public override string OutInfo()
        {
            string trailer = (isTrailer) ? "Да" : "Нет";
            return $"\n------------------------\n" +
                $"Информация о грузовике:\n" +
                $"Марка: {brand}\n" +
                $"Номер: {number}\n" +
                $"Скорость: {speed}\n" +
                $"Грузоподъемность: {loadCapacity}\n" +
                $"Есть прицеп: {trailer}";
        }

        public override string GetLoadCapacity()
        {
            if (isTrailer)
            {
                loadCapacity *= 2;
                return $"Так как есть прицеп, то грузоподъемность грузовика: {loadCapacity}";
            }
            else
            {
                return $"Так как прицепа нет, то грузоподъемность грузовика: {loadCapacity}";
            }
        }
    }
}
