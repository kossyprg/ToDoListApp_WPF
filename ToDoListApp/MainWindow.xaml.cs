using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDoListApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<TodoItem> tasks = new ObservableCollection<TodoItem>();

        public MainWindow()
        {
            InitializeComponent();
            TaskList.ItemsSource = tasks;
        }

        /* WPFのイベント処理は MainWindowクラスのメソッドとして定義する必要がある */
        // MainWindowの外、namespace の中には定義できない (CS0116エラー)
        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string task = TaskInput.Text;
            if (!string.IsNullOrWhiteSpace(task))
            {
                tasks.Add(new TodoItem
                {
                    Title = task,
                    IsDone = false,
                    SelectedPriority = "中",
                    CreatedAt = DateTime.Now  // ← 現在時刻を記録！
                });
                TaskInput.Text = ""; // 入力欄クリア
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            //var selectedTask = TaskList.SelectedItem;
            var selected = TaskList.SelectedItem as TodoItem;
            if (selected != null)
            {
                //TaskList.Items.Remove(selectedTask);
                tasks.Remove(selected);
            }
            else
            {
                MessageBox.Show("削除するタスクを選んでください。");
            }
        }

    }

    public class TodoItem
    {
        public string Title { get; set; }
        public bool IsDone { get; set; }

        public DateTime CreatedAt { get; set; }

        public string SelectedPriority { get; set; }
        public List<string> AvailablePriorities { get; } = new List<string> { "高", "中", "低" };
    }



}
