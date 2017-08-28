﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace StudentsBase.ViewModels.Extensions {
    public class TextBoxExtensions {
        public static DependencyProperty IsDirtyEnabledProperty = DependencyProperty.RegisterAttached("IsDirtyEnabled",
     typeof(bool), typeof(TextBoxExtensions), new PropertyMetadata(false, OnIsDirtyEnabledChanged));
        public static bool GetIsDirtyEnabled(TextBox target) { return (bool)target.GetValue(IsDirtyEnabledProperty); }
        public static void SetIsDirtyEnabled(TextBox target, bool value) { target.SetValue(IsDirtyEnabledProperty, value); }

        public static DependencyProperty ShowErrorTemplateProperty = DependencyProperty.RegisterAttached("ShowErrorTemplate",
                 typeof(bool), typeof(TextBoxExtensions), new PropertyMetadata(false));
        public static bool GetShowErrorTemplate(TextBox target) { return (bool)target.GetValue(ShowErrorTemplateProperty); }
        public static void SetShowErrorTemplate(TextBox target, bool value) { target.SetValue(ShowErrorTemplateProperty, value); }

        private static void OnIsDirtyEnabledChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args) {
            TextBox textBox = (TextBox)dependencyObject;
            if (textBox != null) {
                textBox.LostFocus += (s, e) => {
                    if ((bool)textBox.GetValue(ShowErrorTemplateProperty) == false) {
                        textBox.SetValue(ShowErrorTemplateProperty, true);
                    }
                };
            }
        }
    }
}
