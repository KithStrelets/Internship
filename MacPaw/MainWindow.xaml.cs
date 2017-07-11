using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MacPaw
{

    public partial class MainWindow : Window
    {
        private string from = "", to = "";
        List<Station> red = new List<Station>(), green = new List<Station>(), blue = new List<Station>(), fullWay = new List<Station>();

        public MainWindow()
        {
            InitializeComponent();

            red.Add(new Station("Академгородок", Colors.Red));
            red.Add(new Station("Житомирская", Colors.Red));
            red.Add(new Station("Святошин", Colors.Red));
            red.Add(new Station("Нивки", Colors.Red));
            red.Add(new Station("Берестейская", Colors.Red));
            red.Add(new Station("Шулявская", Colors.Red));
            red.Add(new Station("Политехнический институт", Colors.Red));
            red.Add(new Station("Вокзальная", Colors.Red));
            red.Add(new Station("Университет", Colors.Red));
            red.Add(new Station("Театральная", Colors.Red));
            red.Add(new Station("Крещатик", Colors.Red));
            red.Add(new Station("Арсенальная", Colors.Red));
            red.Add(new Station("Днепр", Colors.Red));
            red.Add(new Station("Гидропарк", Colors.Red));
            red.Add(new Station("Левобережная", Colors.Red));
            red.Add(new Station("Дарница", Colors.Red));
            red.Add(new Station("Черниговская", Colors.Red));
            red.Add(new Station("Лесная", Colors.Red));

            green.Add(new Station("Сырец", Colors.Green));
            green.Add(new Station("Дорогожичи", Colors.Green));
            green.Add(new Station("Лукьяновская", Colors.Green));
            Predicate<Station> match = delegate (Station x) { return x.name.Equals("Театральная"); };
            green.Add(new Station("Золотые ворота", Colors.Green, red.Find(match)));
            green.Add(new Station("Дворец спорта", Colors.Green));
            green.Add(new Station("Кловская", Colors.Green));
            green.Add(new Station("Печерская", Colors.Green));
            green.Add(new Station("Дружбы Народов", Colors.Green));
            green.Add(new Station("Выдубичи", Colors.Green));
            green.Add(new Station("Славутич", Colors.Green));
            green.Add(new Station("Осокорки", Colors.Green));
            green.Add(new Station("Позняки", Colors.Green));
            green.Add(new Station("Харьковская", Colors.Green));
            green.Add(new Station("Вырлица", Colors.Green));
            green.Add(new Station("Бориспольская", Colors.Green));
            green.Add(new Station("Красный хутор", Colors.Green));

            blue.Add(new Station("Героев Днепра", Colors.Blue));
            blue.Add(new Station("Минская", Colors.Blue));
            blue.Add(new Station("Оболонь", Colors.Blue));
            blue.Add(new Station("Петровка", Colors.Blue));
            blue.Add(new Station("Тараса Шевченко", Colors.Blue));
            blue.Add(new Station("Контрактовая площадь", Colors.Blue));
            blue.Add(new Station("Почтовая площадь", Colors.Blue));
            match = delegate (Station x) { return x.name.Equals("Крещатик"); };
            blue.Add(new Station("Площадь Независимости", Colors.Blue, red.Find(match)));
            match = delegate (Station x) { return x.name.Equals("Дворец спорта"); };
            blue.Add(new Station("Площадь Льва Толстого", Colors.Blue, green.Find(match)));
            blue.Add(new Station("Олимпийская", Colors.Blue));
            blue.Add(new Station("Дворец «Украина»", Colors.Blue));
            blue.Add(new Station("Лыбедская", Colors.Blue));
            blue.Add(new Station("Демеевская", Colors.Blue));
            blue.Add(new Station("Голосеевская", Colors.Blue));
            blue.Add(new Station("Васильковская", Colors.Blue));
            blue.Add(new Station("Выставочный центр", Colors.Blue));
            blue.Add(new Station("Ипподром", Colors.Blue));
            blue.Add(new Station("Теремки", Colors.Blue));

            InsertToSelect(red, SelectFrom);
            InsertToSelect(blue, SelectFrom);
            InsertToSelect(green, SelectFrom);

            InsertToSelect(red, SelectTo);
            InsertToSelect(blue, SelectTo);
            InsertToSelect(green, SelectTo);

            BlueRouteDraw(blue, fullWay, canv);
            GreenRouteDraw(green, fullWay, canv);
            RedRouteDraw(red, fullWay, canv);

        }

        List<Station> targetRoute(string selectedfrom, string selectedto, List<Station> red, List<Station> blue, List<Station> green)
        {

            Station from = new Station(), to = new Station();
            List<Station> listFrom = new List<Station>(), listTo = new List<Station>();
            int indexFrom = 0, indexTo = 0;
            Predicate<Station> match = delegate (Station x) { return x.name.Equals(selectedfrom); };
            if (red.Exists(match)) { indexFrom = red.FindIndex(match); from = red[indexFrom]; listFrom = red; }
            else if (blue.Exists(match)) { indexFrom = blue.FindIndex(match); from = blue[indexFrom]; listFrom = blue; }
            else if (green.Exists(match)) { indexFrom = green.FindIndex(match); from = green[indexFrom]; listFrom = green; }

            match = delegate (Station x) { return x.name.Equals(selectedto); };
            if (red.Exists(match)) { indexTo = red.FindIndex(match); to = red[indexTo]; listTo = red; }
            else if (blue.Exists(match)) { indexTo = blue.FindIndex(match); to = blue[indexTo]; listTo = blue; }
            else if (green.Exists(match)) { indexTo = green.FindIndex(match); to = green[indexTo]; listTo = green; }
            List<Station> sublist = new List<Station>();

            if (listFrom.Equals(listTo))
            {
                if (indexFrom < indexTo)
                {
                    for (int a = indexFrom; a <= indexTo; a++)
                    {
                        sublist.Add(listFrom[a]);
                    }
                }
                else if (indexTo < indexFrom)
                {
                    for (int a = indexFrom; a >= indexTo; a--)
                    {
                        sublist.Add(listFrom[a]);
                    }
                }
                else if (indexFrom == indexTo) { sublist.Add(listFrom[indexFrom]); }
                return sublist;
            }

            else if (!listFrom.Equals(listTo))
            {
                match = delegate (Station x) { if (x.connection != null) return x.connection.color.Equals(to.color); else { return false; } };
                Station connector = listFrom.Find(match);
                int indexConnect = listFrom.FindIndex(match);
                if (indexFrom < indexConnect)
                {
                    for (int a = indexFrom; a <= indexConnect; a++)
                    {
                        sublist.Add(listFrom[a]);
                    }
                }
                else if (indexConnect < indexFrom)
                {
                    for (int a = indexFrom; a >= indexConnect; a--)
                    {
                        sublist.Add(listFrom[a]);
                    }
                }
                match = delegate (Station x) { if (x.connection != null) return x.connection.color.Equals(from.color); else { return false; } };
                indexFrom = listTo.FindIndex(match);
                if (indexFrom < indexTo)
                {
                    for (int a = indexFrom; a <= indexTo; a++)
                    {
                        sublist.Add(listTo[a]);
                    }
                }
                else if (indexTo < indexFrom)
                {
                    for (int a = indexFrom; a >= indexTo; a--)
                    {
                        sublist.Add(listTo[a]);
                    }
                }
                else if (indexFrom == indexTo) { sublist.Add(listTo[indexFrom]); }
                return sublist;

            }
            else { return sublist; }
        }

        private void InsertToSelect(List<Station> list, ComboBox box)
        {
            foreach (Station st in list)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = st.name;
                if (st.color.Equals(Colors.Red)) item.Background = new SolidColorBrush(Color.FromRgb(255, 104, 104));
                else if (st.color.Equals(Colors.Green)) item.Background = Brushes.LightGreen;
                else if (st.color.Equals(Colors.Blue)) item.Background = Brushes.LightSteelBlue;

                box.Items.Add(item);
            }
        }

        private void FullWayDraw(List<Station> sublist, List<Station> red, List<Station> blue, List<Station> green, Canvas canvas)
        {
            BlueRouteDraw(blue, sublist, canvas);
            GreenRouteDraw(green, sublist, canvas);
            RedRouteDraw(red, sublist, canvas);
        }

        //Далее идут три метода с дезигном для каждой ветки
        private void RedRouteDraw(List<Station> list, List<Station> sublist, Canvas canvas)
        {
            Thickness thick = new Thickness(0, 325, 0, 0);
            int index = 0, switchside = 0;
            foreach (Station st in list)
            {
                Ellipse station = new Ellipse();
                station.Height = 20; station.Width = 20;
                if (sublist.Count != 0 && sublist.Contains(st)) { station.Fill = Brushes.Gold; station.Stroke = Brushes.DarkTurquoise; }
                else { station.Fill = Brushes.Red; }
                station.Margin = thick;
                Rectangle route = new Rectangle();
                index = list.IndexOf(st);
                if (!st.Equals(list[list.Count - 1]))
                {
                    Thickness routeThick = new Thickness();
                    routeThick.Left = thick.Left + station.Width - 1;
                    routeThick.Top = thick.Top + station.Height / 3;
                    route.Height = 6;
                    if (st.connection != null | (index < list.Count - 1 && list[index + 1].connection != null)) route.Width = 30 * 2;
                    else { route.Width = 25; }
                    if (sublist.Count != 0 && !sublist.Contains(list[index + 1])) route.Fill = Brushes.Red;
                    else { route.Fill = station.Fill; }
                    route.Margin = routeThick;
                }
                Predicate<Station> match = delegate (Station x) { return x.name.Equals("Политехнический институт"); };
                switchside = list.FindIndex(match);
                TextBlock stationName = new TextBlock();
                Thickness txtThick = new Thickness();
                if (st.Equals(list[switchside]) | st.Equals(list[switchside + 1]) | st.Equals(list[switchside + 2]))
                {
                    if (st.name.Equals("Политехнический институт")) { txtThick.Left = thick.Left - station.Width * 18 / 3; txtThick.Top = thick.Top + 135; }
                    else { txtThick.Left = thick.Left - station.Width * 9 / 3; txtThick.Top = thick.Top + 75; }
                }
                else { txtThick.Left = thick.Left + station.Width; txtThick.Top = thick.Top - 15; }
                stationName.Margin = txtThick;
                RotateTransform rotateTrans = new RotateTransform(-45);
                stationName.RenderTransform = rotateTrans;
                stationName.Text = st.name;
                stationName.Foreground = station.Fill;
                if (sublist.Count != 0 && sublist.Contains(st)) { stationName.TextDecorations = TextDecorations.Underline; stationName.Foreground = Brushes.Goldenrod; }
                if (st.connection != null | (index < list.Count - 1 && list[index + 1].connection != null)) thick.Left += 75;
                else { thick.Left += 40; }
                canvas.Children.Add(station);
                if (sublist.Count != 0 && sublist.Contains(st)) Panel.SetZIndex(station, 100);
                canvas.Children.Add(route);
                canvas.Children.Add(stationName);
            }
        }

        private void BlueRouteDraw(List<Station> list, List<Station> sublist, Canvas canvas)
        {
            Thickness thick = new Thickness(470, 10, 0, 0);
            int index = 0, switchside;
            foreach (Station st in list)
            {
                Ellipse station = new Ellipse();
                station.Height = 20; station.Width = 20;
                if (sublist.Count != 0 && sublist.Contains(st)) { station.Fill = Brushes.Gold; station.Stroke = Brushes.DarkTurquoise; }
                else { station.Fill = Brushes.Blue; }
                station.Margin = thick;
                Rectangle route = new Rectangle();
                index = list.IndexOf(st);
                if (!st.Equals(list[list.Count - 1]))
                {
                    Thickness routeThick = new Thickness();
                    RotateTransform rotateTrans = new RotateTransform(90);
                    route.RenderTransform = rotateTrans;
                    routeThick.Left = thick.Left + station.Width * 2 / 3;
                    routeThick.Top = thick.Top + station.Height - 1;
                    route.Height = 6;
                    if (st.connection != null | (index < list.Count - 1 && list[index + 1].connection != null)) route.Width = 30 * 2;
                    else { route.Width = 25; }
                    if (sublist.Count != 0 && !sublist.Contains(list[index + 1])) route.Fill = Brushes.Blue;
                    else { route.Fill = station.Fill; }
                    route.Margin = routeThick;
                }
                TextBlock stationName = new TextBlock();
                Thickness txtThick = new Thickness();
                Predicate<Station> match = delegate (Station x) { return x.name.Equals("Площадь Независимости"); };
                switchside = list.FindIndex(match);
                if (st.Equals(list[switchside]) | st.Equals(list[switchside + 1]) | st.Equals(list[switchside + 2]) | st.Equals(list[switchside + 3]))
                {
                    if (st.Equals(list[switchside])) { txtThick.Left = thick.Left + station.Width * 3 / 2; txtThick.Top = thick.Top + 20; }
                    else if (st.Equals(list[switchside + 1]))
                    {
                        txtThick.Left = thick.Left - station.Width * 7; txtThick.Top = thick.Top + 15;
                    }
                    else if (st.Equals(list[switchside + 2])) { txtThick.Left = thick.Left - station.Width * 9 / 2; txtThick.Top = thick.Top + 5; }
                    else { txtThick.Left = thick.Left - station.Width * 11.5 / 2; txtThick.Top = thick.Top + 5; }
                }
                else { txtThick.Left = thick.Left + station.Width * 3 / 2; txtThick.Top = thick.Top; }
                stationName.Margin = txtThick;
                stationName.Text = st.name;
                stationName.Foreground = station.Fill;
                if (sublist.Count != 0 && sublist.Contains(st)) { stationName.TextDecorations = TextDecorations.Underline; stationName.Foreground = Brushes.Goldenrod; }
                if (st.connection != null | (index < list.Count - 1 && list[index + 1].connection != null)) thick.Top += 75;
                else { thick.Top += 40; }
                canvas.Children.Add(station);
                if (sublist.Count != 0 && sublist.Contains(st)) Panel.SetZIndex(station, 100);
                canvas.Children.Add(route);
                canvas.Children.Add(stationName);
            }
        }

        private void GreenRouteDraw(List<Station> list, List<Station> sublist, Canvas canvas)
        {
            Thickness thick = new Thickness(240, 170, 0, 0);
            int index = 0;
            foreach (Station st in list)
            {
                Ellipse station = new Ellipse();
                station.Height = 20; station.Width = 20;
                if (sublist.Count != 0 && sublist.Contains(st)) { station.Fill = Brushes.Gold; station.Stroke = Brushes.DarkTurquoise; }
                else { station.Fill = Brushes.ForestGreen; }
                station.Margin = thick;
                Rectangle route = new Rectangle();
                index = list.IndexOf(st);
                if (!st.Equals(list[list.Count - 1]))
                {
                    Thickness routeThick = new Thickness();
                    RotateTransform rotateTrans = new RotateTransform(45);
                    route.RenderTransform = rotateTrans;
                    routeThick.Left = thick.Left + station.Width * 2.5 / 3;
                    routeThick.Top = thick.Top + station.Height * 2 / 3;
                    route.Height = 6;
                    if (st.connection != null | (index < list.Count - 1 && list[index + 1].connection != null)) route.Width = 30 * 3;
                    else { route.Width = 25 * 2; }
                    if (sublist.Count != 0 && !sublist.Contains(list[index + 1])) route.Fill = Brushes.ForestGreen;
                    else { route.Fill = station.Fill; }
                    route.Margin = routeThick;
                }
                TextBlock stationName = new TextBlock();
                Thickness txtThick = new Thickness();
                if (st.name.Equals("Золотые ворота"))
                {
                    RotateTransform rotateTrans = new RotateTransform(-45);
                    stationName.RenderTransform = rotateTrans;
                    txtThick.Left = thick.Left - station.Width * 4; txtThick.Top = thick.Top + 85;
                }
                else { txtThick.Left = thick.Left + station.Width * 3 / 2; txtThick.Top = thick.Top; }
                stationName.Margin = txtThick;
                stationName.Text = st.name;
                stationName.Foreground = station.Fill;
                if (sublist.Count != 0 && sublist.Contains(st)) { stationName.TextDecorations = TextDecorations.Underline; stationName.Foreground = Brushes.Goldenrod; }
                if (st.connection != null | (index < list.Count - 1 && list[index + 1].connection != null)) { thick.Top += 75; thick.Left += 75; }
                else { thick.Top += 40; thick.Left += 40; }
                canvas.Children.Add(station);
                if (sublist.Count != 0 && sublist.Contains(st)) Panel.SetZIndex(station, 100);
                canvas.Children.Add(route);
                canvas.Children.Add(stationName);
            }
        }

        private void SelectTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selected = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            to = (string)selected.Content;
            if (SelectFrom.SelectedItem != null)
            {
                SelectWay.Items.Clear();
                fullWay = targetRoute(from, to, red, blue, green);
                InsertToSelect(fullWay, SelectWay);
                label0.Content = "↓ Ваш путь по " + fullWay.Count + " станциям ↓";
                SelectWay.IsDropDownOpen = true;
                canv.Children.Clear();
                FullWayDraw(fullWay, red, blue, green, canv);
            }
        }

        private void SelectFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selected = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            from = (string)selected.Content;
            if (SelectTo.SelectedItem != null)
            {
                SelectWay.Items.Clear();
                fullWay = targetRoute(from, to, red, blue, green);
                InsertToSelect(fullWay, SelectWay);
                label0.Content = "↓ Ваш путь по " + fullWay.Count + " станциям ↓";
                SelectWay.IsDropDownOpen = true;
                canv.Children.Clear();
                FullWayDraw(fullWay, red, blue, green, canv);
            }
        }
    }
    class Station
    {
        public string name { get; set; }
        public Color color { get; set; }
        public Station connection { get; set; }
        public Station()
        {

        }
        public Station(string _name, Color _color)
        {
            name = _name;
            color = _color;
        }
        public Station(string _name, Color _color, Station _connect)
        {
            name = _name;
            color = _color;
            connection = _connect;
            _connect.connection = this;
        }
    }
}

