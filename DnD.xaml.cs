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
        private FrameworkElement draggedObject;
        private FrameworkElement phantomObject;

        private Point touchPoint;
        private Point initialPoint;

        public DnD()
        {
            InitializeComponent();
            draggedObject = null;
            phantomObject = null;
        }

        private void Mouse_Up(object sender, MouseButtonEventArgs e)
        {
            switch (e.ChangedButton)
            {
                case MouseButton.Left:
                    if (draggedObject != null)
                    {
                        if (IsInside(TargetFrame, draggedObject))
                        {   // Центруем draggedObject в рамке
                            SetInCenter(TargetFrame, draggedObject);
                        }
                        else
                        {       // При не попадании в рамку возвращаем draggedObject
                                // в исходную позицию
                            Canvas.SetLeft(draggedObject, initialPoint.X);
                            Canvas.SetTop(draggedObject, initialPoint.Y);
                        }
                    }
                    break;

                case MouseButton.Right:
                    if (phantomObject != null)
                    {
                        if (IsInside(TargetFrame, phantomObject))
                        {   // Центруем phantomObject в рамке
                            SetInCenter(TargetFrame, phantomObject);
                        }
                        else
                        {       // При не попадании в рамку удаляем phantomObject
                            Field.Children.Remove(phantomObject);
                        }
                    }
                    break;
            }
            phantomObject = null;   
            draggedObject = null;
            Field.ReleaseMouseCapture();  // освобождение мыши       
        }
        private void Mouse_Move(object sender, MouseEventArgs e)
        {
            if (draggedObject != null)
            {
                Canvas.SetLeft(draggedObject,
                    e.GetPosition(Field).X - touchPoint.X);
                Canvas.SetTop(draggedObject,
                    e.GetPosition(Field).Y - touchPoint.Y);
            }

            /***** Phantom *****/
            if (phantomObject != null)
            {
                Canvas.SetLeft(phantomObject,
                    e.GetPosition(Field).X - touchPoint.X);
                Canvas.SetTop(phantomObject,
                    e.GetPosition(Field).Y - touchPoint.Y);
            }
        }
        private void Subj_MouseDown(object sender, MouseButtonEventArgs e)
        {
            draggedObject = sender as FrameworkElement;
            touchPoint = e.GetPosition(draggedObject);
            initialPoint.X = Canvas.GetLeft(draggedObject);
            initialPoint.Y = Canvas.GetTop(draggedObject);

            switch (e.ChangedButton)
            {
                case MouseButton.Left:

                    if (draggedObject == null) return;

                    Field.CaptureMouse();  // захват - события
                    // мыши будут попадать в это окно, даже если
                    // указатель из него выйдет
                    break;

                case MouseButton.Right:
                    // Вариация с фантомным объектом
                    phantomObject = new Ellipse
                    {
                        Width = draggedObject.Width,
                        Height = draggedObject.Height,
                        Stroke = Brushes.BlueViolet,
                        Fill = Brushes.Transparent
                    };

                    Field.Children.Add(phantomObject);
                    Canvas.SetLeft(phantomObject, Canvas.GetLeft(draggedObject));
                    Canvas.SetTop(phantomObject, Canvas.GetTop(draggedObject));

                    draggedObject = null;
                    break;
            }
        }

        private bool IsInside(FrameworkElement target, FrameworkElement element)
        {
            return (Canvas.GetLeft(element) > Canvas.GetLeft(target) && 
                Canvas.GetTop(element) > Canvas.GetTop(target) &&   
                Canvas.GetLeft(element) + element.Width <
                Canvas.GetLeft(target) + target.Width &&
                Canvas.GetTop(element) + element.Height <
                Canvas.GetTop(target) + target.Height);  
        }

        private void SetInCenter(FrameworkElement target, FrameworkElement element)
        {
            Canvas.SetLeft(element, Canvas.GetLeft(target) +
                  (target.Width - element.Width) / 2);
            Canvas.SetTop(element, Canvas.GetTop(target) +
                              (target.Height - element.Height) / 2);
        }
    }
}


/* DnD (Drag n'Drop)
 * Технология визуального интерфейса, связанная с "перетаскиванием"
 * объектов мышью
 * Для реализации необходимо:
 * - по событию нажатия перейти в режим перетаскивания
 * - по событию движения менять позицию объекта
 * - по событию отжатия - зафиксировать новую позицию
 * 
 * Вариации:
 * "фантомы" - копии объектов, перетягиваемые вместо оригиналов
 * "контейнеры" - если не попадаем в контейнер, перетягивание отменяется
 * 
 * Особенности:
 * - событие нажатия получает сам объект, тогда как
 *    движение и отжатие - всё окно. Иначе при резких движениях
 *    курсор уходит с объекта и он теряет события
 * - для перетягивания за точку "взятия" необходимо запоминать
 *    координаты этой точки в событии нажатия, а корректировать
 *    в событии движения мыши
 * - похожая на п.1 ситуация возможна с окном: при выходе
 *    укателя мыши из окна "теряется" событие отжатия кнопки.
 *    Решается "захватом" указателя на время нажатия
 */
