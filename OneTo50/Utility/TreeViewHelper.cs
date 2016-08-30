using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace OneTo50.Utility
{
    public class TreeViewHelper
    {
        /// <summary>
        /// 查找容器内的所有元素，并返回第一个是T类型的元素。
        /// </summary>
        /// <typeparam name="T">返回元素类型</typeparam>
        /// <param name="container">容器</param>
        /// <returns>第一个是T类型的元素</returns>
        public static T FindVisualChild<T>(DependencyObject root) where T : class
        {
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                DependencyObject current = queue.Dequeue();
                for (int i = VisualTreeHelper.GetChildrenCount(current) - 1; 0 <= i; i--)
                {
                    var child = VisualTreeHelper.GetChild(current, i);
                    var typedChild = child as T;
                    if (typedChild != null)
                    {
                        return typedChild;
                    }
                    queue.Enqueue(child);
                }
            }
            return null; 
        }

        /// <summary>
        /// 获取指定元素的指定视图状态组。
        /// </summary>
        /// <param name="element">目标元素</param>
        /// <param name="name">视图状态名</param>
        /// <returns>视图状态组</returns>
        public static VisualStateGroup FindVisualState(FrameworkElement element, string name)
        {
            if (element == null)
            {
                return null;
            }
            var groups = VisualStateManager.GetVisualStateGroups(element);
            foreach (VisualStateGroup group in groups)
            {
                if (group.Name == name)
                {
                    return group;
                }
            }
            return null;
        }
    }
}
