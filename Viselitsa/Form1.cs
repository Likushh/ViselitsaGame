using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;


namespace Viselitsa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox3.KeyDown += new KeyEventHandler(textBox2_KeyDown);
            button3.Click += new EventHandler(button3_Click);
        }
        public string text;
        public char[] word;
        public int hp;
        public int hptrue;
        public List<char> enteredLetters = new List<char>();

        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Text = ""; // очистить полотно
            textBox2.Text = null; // очистить список введенных букв
            enteredLetters.Clear(); // очистить список введенных букв

            // Чтение всех слов из файла
            var lines = File.ReadAllLines("words.txt");
            var random = new Random();
            var randomLineNumber = random.Next(0, lines.Length - 1);
            text = lines[randomLineNumber]; // выбор случайного слова

            hp = -1; // начать с -1
            label1.Text = null;
            word = new char[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                label1.Text += '.';
                word[i] = '.';
            }
        }

        private string[] hangmanStages = new string[]
{
            @"
              + -- - +
              |   |
                  |
                  |
                  |
                  |
            ======== = ",
            @"
              + -- - +
              |   |
             O  |
                  |
                  |
                  |
            ======== = ",
            @"
              + -- - +
              |   |
             O   |
              |   |
                  |
                  |
            ======== = ",
            @"
              + -- - +
              |   |
             O  |
             /|   |
                  |
                  |
            ======== = ",
            @"
              + -- - +
              |   |
             O  |
            /|\   |
                  |
                  |
            ======== = ",
            @"
              + -- - +
              |   |
             O  |
            /|\   |
            /     |
                  |
            ======== = ",
            @"
              + -- - +
              |   |
             O  |
            /|\   |
            / \   |
                  |
            ======== = "
};

        private void button2_Click(object sender, EventArgs e)
        {
            bool words = false;

            char enteredLetter = textBox3.Text.Length == 1 ? textBox3.Text[0] : '\0'; // Получить введенную букву

            if (enteredLetters.Contains(enteredLetter)) // Проверить, была ли уже введена буква
            {
                MessageBox.Show("Эта буква уже была введена!");
                return;
            }

            if (textBox3.Text.Length == 1) // Для одной буквы
            {
                enteredLetters.Add(enteredLetter); // Добавить введенную букву в список

                for (int i = 0; i < text.Length; i++)
                {
                    if (enteredLetter == text[i])
                    {
                        words = true;
                        hptrue++;
                        word[i] = text[i];
                        label1.Text = null;
                    }
                }
                if (words == true)
                {
                    for (int i = 0; i < text.Length; i++)
                    {
                        label1.Text += word[i];
                    }
                    if (hptrue == text.Length)
                    {
                        MessageBox.Show("Ты победил! Слово было угадано!");
                        // Очистить все после выигрыша
                        textBox4.Text = "";
                        textBox2.Text = null;
                        enteredLetters.Clear();
                        label1.Text = null;
                        word = null;
                        hp = -1;
                        hptrue = 0;
                    }
                }
                else
                {
                    textBox2.Text += enteredLetter + ", "; // Добавить введенную букву в textBox4
                    hp++;
                    if (hp < hangmanStages.Length)
                    {
                        textBox4.Text = hangmanStages[hp];
                    }
                    if (hp == hangmanStages.Length - 1)
                    {
                        MessageBox.Show("Ты проиграл! Слово было: " + text);
                        // Очистить все после проигрыша
                        textBox4.Text = "";
                        textBox2.Text = null;
                        enteredLetters.Clear();
                        label1.Text = null;
                        word = null;
                        hp = -1;
                        hptrue = 0;
                    }
                }

            }
            else if (textBox3.Text.Length > 1) // Для полного ответа 
            {
                if (textBox3.Text == text)
                {
                    label1.Text = null;
                    for (int i = 0; i < text.Length; i++)
                    {
                        label1.Text += text[i];
                    }
                    MessageBox.Show("Ты победил! Слово было угадано!");
                    // Очистить все после выигрыша
                    textBox4.Text = "";
                    textBox2.Text = null;
                    enteredLetters.Clear();
                    label1.Text = null;
                    word = null;
                    hp = -1;
                    hptrue = 0;
                }
                else
                {
                    hp = hangmanStages.Length - 1;
                    if (hp < hangmanStages.Length)
                    {
                        textBox4.Text = hangmanStages[hp];
                    }
                    MessageBox.Show("Ты проиграл! Слово было: " + text);
                    // Очистить все после проигрыша
                    textBox4.Text = "";
                    textBox2.Text = null;
                    enteredLetters.Clear();
                    label1.Text = null;
                    word = null;
                    hp = -1;
                    hptrue = 0;
                }
            }
            textBox3.Text = null;
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2.PerformClick(); // Имитация нажатия кнопки "Угадать букву"
                e.SuppressKeyPress = true; // Предотвращение звукового сигнала
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

    }
}