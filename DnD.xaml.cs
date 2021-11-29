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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for DnD.xaml
    /// </summary>
    public partial class DnD : Window
    {
        private bool isDragged;
        private Point touchPoint;
        public DnD()
        {
            InitializeComponent();
            isDragged = false;
        }
        private void Mouse_Up(object sender, MouseButtonEventArgs e)
        {
            switch (e.ChangedButton)
            {
                case MouseButton.Left:
                    isDragged = false;
                    Field.ReleaseMouseCapture(); //освобождение мыши
                    break;
            }
        }

        private void Mouse_Move(object sender, MouseEventArgs e)
        {
            if (isDragged)
            {
                Canvas.SetLeft(Subj,
                    e.GetPosition(Field).X - Subj.Width / 2 - touchPoint.X);

                Canvas.SetTop(Subj,
                    e.GetPosition(Field).Y - Subj.Height / 2 - touchPoint.Y);
            }
        }
        private void Subj_MouseDown(object sender, MouseButtonEventArgs e) //событие нажатие мыши
        {
            switch (e.ChangedButton)
            {
                case MouseButton.Left:
                    isDragged = true;
                    touchPoint = e.GetPosition(Subj);
                    Field.CaptureMouse(); //захват - события
                    //будут попадать в это окно, даже если курсор из него выйдет
                    break;
                case MouseButton.Middle:
                    break;
                case MouseButton.Right:
                    break;
                case MouseButton.XButton1:
                    break;
                case MouseButton.XButton2:
                    break;
            }

        }
    }
}
/*
 * DnD
 * Технология визуального интерфейса, связывания с "перетаскиванием"
 * объектов мышью
 * Для реализации необходимо:
 * -по событию - нажатию - перейти в режим перетаскивания.
 * - по событию движению - менять позиции объекта
 * - по событие отжатия - зафиксировать новую позицию
 * 
 * Вариации:
 * "Фантомы" - копия объектов, перетягиваемые вместо оригиналов
 * "контейнеры" - если не попадаем в контейнер, перетягивание отменяется
 * 
 * Особеннсти:
 * - событие нажатия получает сам объект, тогда движение и отжатие - све окно.
 * Иначе при резких движениях курсор уходит с объекта и он теряет событие.
 * для перетягивания за точку "взятия " необходимо запоминать координаты этой точки в событии
 * нажатия, а корректировать в событии движения мыши.
 *  - похожая на 1.п ситуация возможна с окном: при выходе
 *  указателя мыши из окна "теряется" событие отжатия кнопки
 *  Решается захватом указателя на время нажатия
 * 

 
 */
