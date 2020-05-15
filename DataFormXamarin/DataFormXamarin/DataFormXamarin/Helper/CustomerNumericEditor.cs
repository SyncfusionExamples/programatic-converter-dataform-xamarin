using Syncfusion.XForms.DataForm;   
using Syncfusion.XForms.DataForm.Editors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace DataFormXamarin
{
    public class CustomNumericEditor : DataFormEditor<Entry>
    {
        public CustomNumericEditor(SfDataForm dataForm) : base(dataForm)
        {
        }
        protected override Entry OnCreateEditorView(DataFormItem dataFormItem)
        {
            return new Entry();
        }
        protected override void OnInitializeView(DataFormItem dataFormItem, Entry view)
        {
            base.OnInitializeView(dataFormItem, view);
            view.Keyboard = Keyboard.Numeric;
            this.OnUpdateValue(dataFormItem, view);
            this.OnUpdateReadOnly(dataFormItem, view);
        }
        protected override void OnWireEvents(Entry view)
        {
            view.TextChanged += OnViewTextChanged;
            view.Focused += View_Focused;
            view.Unfocused += View_Unfocused;
        }
        private void View_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            var view = sender as Entry;
            view.TextColor = Color.Red;

            if (this.DataForm.CommitMode == Syncfusion.XForms.DataForm.CommitMode.LostFocus || this.DataForm.ValidationMode == ValidationMode.LostFocus)
                this.OnValidateValue(view);
        }
        private void View_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            var view = (sender as Entry);
            view.TextColor = Color.Green;
        }
        private void OnViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnValidateValue(sender as Entry);
        }
        protected override bool OnValidateValue(Entry view)
        {
            return this.DataForm.Validate("Amount");
        }
        private void OnViewTextChanged(object sender, TextChangedEventArgs e)
        {
            var view = sender as Entry;
            if (DataForm.CommitMode == Syncfusion.XForms.DataForm.CommitMode.PropertyChanged || DataForm.ValidationMode == ValidationMode.PropertyChanged)
                this.OnValidateValue(view);
        }
        protected override void OnCommitValue(Entry view)
        {
            if (string.IsNullOrEmpty(view.Text))
                return;
            var dataFormItemView = view.Parent as DataFormItemView;
            var value = Convert.ToInt32(view.Text);
            view.Text = (value * 10).ToString();
            this.DataForm.ItemManager.SetValue(dataFormItemView.DataFormItem, view.Text);
        }
        protected override void OnUpdateValue(DataFormItem dataFormItem, Entry view)
        {
            var cellValue = this.DataForm.ItemManager.GetValue(dataFormItem);
            if (cellValue != null && view.Text == cellValue.ToString())
                return;
            view.Text = cellValue == null ? string.Empty : cellValue.ToString();
        }
        protected override void OnUpdateReadOnly(DataFormItem dataFormItem, Entry view)
        {
            base.OnUpdateReadOnly(dataFormItem, view);
        }
        protected override void OnUnWireEvents(Entry view)
        {
            view.TextChanged -= OnViewTextChanged;
            view.Focused -= View_Focused;
            view.Unfocused -= View_Unfocused;
        }
    }
}
