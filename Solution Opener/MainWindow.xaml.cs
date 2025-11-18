using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Solution_Opener.Models;
using Solution_Opener.ViewModels;

namespace Solution_Opener
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            
            // Handle Ctrl+F to focus search box
            CommandBindings.Add(new CommandBinding(
                ApplicationCommands.Find,
                (s, e) => { SearchTextBox.Focus(); SearchTextBox.SelectAll(); }
            ));
        }

        private void SearchTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                // Find the TabControl in the window
                var tabControl = FindVisualChild<TabControl>(this);
                if (tabControl?.SelectedItem != null)
                {
                    // The content is wrapped in a ContentPresenter
                    var contentPresenter = FindVisualChild<ContentPresenter>(tabControl);
                    if (contentPresenter != null)
                    {
                        // Now find the DataGrid inside the content presenter
                        var dataGrid = FindVisualChild<DataGrid>(contentPresenter);
                        if (dataGrid != null && dataGrid.Items.Count > 0)
                        {
                            dataGrid.Focus();
                            
                            // Select the first item if nothing is selected
                            if (dataGrid.SelectedIndex == -1)
                            {
                                dataGrid.SelectedIndex = 0;
                            }
                            
                            // Update layout to ensure the item is rendered
                            dataGrid.UpdateLayout();
                            
                            // Scroll the selected item into view and focus it
                            if (dataGrid.SelectedItem != null)
                            {
                                dataGrid.ScrollIntoView(dataGrid.SelectedItem);
                                
                                // Try to focus the row
                                dataGrid.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    var row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
                                    if (row != null)
                                    {
                                        row.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                                    }
                                }), System.Windows.Threading.DispatcherPriority.Background);
                            }
                            
                            e.Handled = true;
                        }
                    }
                }
            }
        }

        private void TabControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                var tabControl = sender as TabControl;
                if (tabControl?.SelectedItem == null)
                    return;

                // Check if the focused element is within the tab header area (not in the content)
                var focusedElement = Keyboard.FocusedElement as DependencyObject;
                
                // If focused element is null or is already a DataGrid or within a DataGrid, don't handle
                if (focusedElement == null)
                    return;
                
                // Check if focus is within the DataGrid content area
                var dataGridParent = FindParent<DataGrid>(focusedElement);
                if (dataGridParent != null)
                    return; // Focus is already in the grid, let it handle navigation
                
                // Check if focused element is within a TabItem header
                var tabItem = focusedElement is TabItem ? focusedElement : FindParent<TabItem>(focusedElement);
                if (tabItem != null)
                {
                    // Focus is on tab header, move to grid
                    var contentPresenter = FindVisualChild<ContentPresenter>(tabControl);
                    if (contentPresenter != null)
                    {
                        var dataGrid = FindVisualChild<DataGrid>(contentPresenter);
                        if (dataGrid != null && dataGrid.Items.Count > 0)
                        {
                            dataGrid.Focus();
                            
                            // Select the first item if nothing is selected
                            if (dataGrid.SelectedIndex == -1)
                            {
                                dataGrid.SelectedIndex = 0;
                            }
                            
                            // Update layout to ensure the item is rendered
                            dataGrid.UpdateLayout();
                            
                            // Scroll the selected item into view and focus it
                            if (dataGrid.SelectedItem != null)
                            {
                                dataGrid.ScrollIntoView(dataGrid.SelectedItem);
                                
                                // Try to focus the row
                                dataGrid.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    var row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
                                    if (row != null)
                                    {
                                        row.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                                    }
                                }), System.Windows.Threading.DispatcherPriority.Background);
                            }
                            
                            e.Handled = true;
                        }
                    }
                }
            }
        }

        private T? FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            if (child == null)
                return null;

            var parent = System.Windows.Media.VisualTreeHelper.GetParent(child);
            
            if (parent is T typedParent)
            {
                return typedParent;
            }
            
            return FindParent<T>(parent);
        }

        private T? FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null)
                return null;

            for (int i = 0; i < System.Windows.Media.VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = System.Windows.Media.VisualTreeHelper.GetChild(parent, i);
                
                if (child is T typedChild)
                {
                    return typedChild;
                }
                
                var result = FindVisualChild<T>(child);
                if (result != null)
                {
                    return result;
                }
            }
            
            return null;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Restore window settings
            var settings = ViewModel.GetWindowSettings();
            
            if (settings.Width > 0 && settings.Height > 0)
            {
                Width = settings.Width;
                Height = settings.Height;
                Left = settings.Left;
                Top = settings.Top;

                // Ensure window is visible on screen
                if (Left < 0 || Top < 0 || 
                    Left > SystemParameters.VirtualScreenWidth || 
                    Top > SystemParameters.VirtualScreenHeight)
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }

                if (settings.IsMaximized)
                {
                    WindowState = WindowState.Maximized;
                }
            }

            await ViewModel.InitializeAsync();

            // Focus search box on startup
            SearchTextBox.Focus();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Save window settings
            var settings = new WindowSettings
            {
                Width = RestoreBounds.Width,
                Height = RestoreBounds.Height,
                Left = RestoreBounds.Left,
                Top = RestoreBounds.Top,
                IsMaximized = WindowState == WindowState.Maximized
            };

            ViewModel.SaveWindowSettings(settings);
        }

        private void SolutionsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            // Handle Repository column visibility based on IsQuickAccess
            if (sender is DataGrid grid && grid.DataContext is RepositoryTabViewModel vm)
            {
                if (grid.Columns.Count > 1)
                {
                    var repoColumn = grid.Columns[1]; // Repository column (after Status Icon column)
                    
                    // Set width: 1.5* for Quick Access, 0 for others
                    repoColumn.Width = vm.IsQuickAccess 
                        ? new DataGridLength(1.5, DataGridLengthUnitType.Star) 
                        : new DataGridLength(0);
                }
            }
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update Repository column visibility when tab changes
            if (sender is TabControl tabControl && tabControl.SelectedItem is RepositoryTabViewModel vm)
            {
                // Find the DataGrid in the current tab's content
                var contentPresenter = FindVisualChild<ContentPresenter>(tabControl);
                if (contentPresenter != null)
                {
                    var dataGrid = FindVisualChild<DataGrid>(contentPresenter);
                    if (dataGrid != null && dataGrid.Columns.Count > 1)
                    {
                        var repoColumn = dataGrid.Columns[1]; // Repository column
                        repoColumn.Width = vm.IsQuickAccess 
                            ? new DataGridLength(1.5, DataGridLengthUnitType.Star) 
                            : new DataGridLength(0);
                    }
                }
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //switch tabs with Ctrl+1 to Ctrl+9
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                if (e.Key >= Key.D1 && e.Key <= Key.D9)
                {
                    int tabIndex = e.Key - Key.D1;
                    
                    // Find the TabControl in the window
                    var tabControl = FindVisualChild<TabControl>(this);
                    if (tabControl != null && tabIndex < tabControl.Items.Count)
                    {
                        tabControl.SelectedIndex = tabIndex;
                        e.Handled = true;
                    }
                }
            }
        }
    }
}