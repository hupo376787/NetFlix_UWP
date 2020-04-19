using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace NetFlix
{
    public sealed partial class StepThroughListView : UserControl, INotifyPropertyChanged
    {
        ScrollViewer _InternalListScrollViewer;

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); RaisePropertyChanged(); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), 
                typeof(StepThroughListView), new PropertyMetadata(DependencyProperty.UnsetValue));

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); RaisePropertyChanged(); }
        }

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate),
                typeof(StepThroughListView), new PropertyMetadata(DependencyProperty.UnsetValue));

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set
            {
                if (SelectedItem != value)
                {
                    SetValue(SelectedItemProperty, value);
                    RaisePropertyChanged();
                }
            }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object), 
                typeof(StepThroughListView), new PropertyMetadata(DependencyProperty.UnsetValue));

        public Visibility AlwaysShowButton
        {
            get { return (Visibility)GetValue(AlwaysShowButtonProperty); }
            set { SetValue(AlwaysShowButtonProperty, value); RaisePropertyChanged(); }
        }

        public static readonly DependencyProperty AlwaysShowButtonProperty =
            DependencyProperty.Register(nameof(AlwaysShowButton), typeof(Visibility),
                typeof(StepThroughListView), new PropertyMetadata(DependencyProperty.UnsetValue));

        public StepThroughListView()
        {
            InitializeComponent();
            if (AlwaysShowButton == Visibility.Visible)
            {
                ButtonLeft.Visibility = Visibility.Visible;
                ButtonRight.Visibility = Visibility.Visible;
            }

            SizeChanged += StepThroughListView_SizeChanged;
        }

        private void StepThroughListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            AlignSelectedItem();
        }

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if(AlwaysShowButton == Visibility.Collapsed)
            {
                ButtonLeft.Visibility = Visibility.Visible;
                ButtonRight.Visibility = Visibility.Visible;
            }
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (AlwaysShowButton == Visibility.Collapsed)
            {
                ButtonLeft.Visibility = Visibility.Collapsed;
                ButtonRight.Visibility = Visibility.Collapsed;
            }
        }

        private void ButtonRight_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScrollList(shouldScrollDown: true);
        }

        private void ButtonLeft_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScrollList(shouldScrollDown: false);
        }

        private void ScrollList(bool shouldScrollDown)
        {
            SelectedItem = null;

            //344 from HomeCardWidthConverter
            var step = Math.Floor(Window.Current.Bounds.Width / 400);

            if (!shouldScrollDown)
                step *= -1;

            //_InternalListScrollViewer.ScrollToVerticalOffset(
            //    _InternalListScrollViewer.VerticalOffset + height);
            System.Diagnostics.Debug.WriteLine(_InternalListScrollViewer.ScrollableWidth);
            _InternalListScrollViewer.ChangeView(_InternalListScrollViewer.HorizontalOffset + step, null, null);
        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            _InternalListScrollViewer = TreeHelper.FindVisualChild<ScrollViewer>((DependencyObject)sender);
        }

        private void ListView_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AlignSelectedItem();
        }

        private void AlignSelectedItem()
        {
            //var index = ListViewRiver.SelectedIndex;
            //double? itemHeight = 
            //    ((FrameworkElement)ListViewRiver.ContainerFromIndex(index))?.ActualHeight;

            //if (itemHeight.HasValue)
            //{
            //    var itemsInView = _InternalListScrollViewer.ActualHeight / itemHeight.Value;
            //    var topOffset = (itemsInView - 1) * itemHeight.Value / 2;

            //    _InternalListScrollViewer.ScrollToVerticalOffset(
            //        (index * itemHeight.Value) - topOffset);
            //}
            //else if(index != -1)
            //{
            //    //There is an item selected in the ListView, but it isn't rendered yet.
            //    //Hook up to ChoosingItemContainer event to try again...
            //    ListViewRiver.ChoosingItemContainer += ListView_ChoosingItemContainer;
            //}
        }

        private void ListView_ChoosingItemContainer(ListViewBase sender, ChoosingItemContainerEventArgs args)
        {
            ListViewRiver.ChoosingItemContainer -= ListView_ChoosingItemContainer;
            AlignSelectedItem();
        }

        void RaisePropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        
    }
}
