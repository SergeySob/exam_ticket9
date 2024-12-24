using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace exam_ticket9
{
    public partial class MainWindow : Window
    {
        private string currentFilePath = string.Empty;
        private List<string> recentFiles = new List<string>();
        private const string RecentFilesPath = "recent_files.txt";

        public MainWindow()
        {
            InitializeComponent();
            LoadRecentFiles();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                currentFilePath = openFileDialog.FileName;
                TextEditor.Text = File.ReadAllText(currentFilePath);
                UpdateRecentFiles(currentFilePath);
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
                };
                if (saveFileDialog.ShowDialog() == true)
                {
                    currentFilePath = saveFileDialog.FileName;
                }
            }

            File.WriteAllText(currentFilePath, TextEditor.Text);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Replace_Click(object sender, RoutedEventArgs e)
        {
            var replaceWindow = new ReplaceWindow(TextEditor);
            replaceWindow.ShowDialog();
        }

        private void UpdateRecentFiles(string filePath)
        {
            if (!recentFiles.Contains(filePath))
            {
                recentFiles.Insert(0, filePath);
                if (recentFiles.Count > 5) recentFiles.RemoveAt(recentFiles.Count - 1);
            }
            SaveRecentFiles();
            LoadRecentFiles();
        }

        private void LoadRecentFiles()
        {
            if (File.Exists(RecentFilesPath))
            {
                var files = File.ReadAllLines(RecentFilesPath);
                recentFiles = new List<string>(files);
                RecentFilesMenu.Items.Clear();

                foreach (var file in recentFiles)
                {
                    var item = new MenuItem { Header = file };
                    item.Click += (sender, e) =>
                    {
                        currentFilePath = file;
                        TextEditor.Text = File.ReadAllText(file);
                    };
                    RecentFilesMenu.Items.Add(item);
                }
            }
        }

        private void SaveRecentFiles()
        {
            File.WriteAllLines(RecentFilesPath, recentFiles);
        }

        private void TextEditor_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            int wordCount = TextEditor.Text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
            WordCountStatus.Content = $"Words: {wordCount}";
        }
    }

    
}
