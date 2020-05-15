# How to programmatically implement converter to Xamarin.Forms DataForm (SfDataForm) 

You can apply the converter for Editor in Xamarin.Forms [SfDataForm](https://help.syncfusion.com/xamarin/dataform/getting-started?) by using custom editor.

You can create and add custom editor to SfDataForm by overriding the [DataFormEditor](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfDataForm.XForms~Syncfusion.XForms.DataForm.Editors.DataFormEditor%601.html?) class, where the CustomNumericEditor is inherited using the DataFormEditor<Entry>. 

Refer to the [online user guide documentation](https://help.syncfusion.com/xamarin/sfdataform/editors?&_ga=2.215218846.843378045.1589170218-1204678185.1570168583#custom-editor) for creating new custom editor in DataForm.

You can also refer the following article.

https://www.syncfusion.com/kb/11541/how-to-programmatically-implement-converter-to-xamarin-forms-dataform-sfdataform

**C#**

CustomNumericEditor extends DataFormEditor to add converter programmatically.
``` c#
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
```
Refer to the following code example for register the editor using [RegisterEditor](https://help.syncfusion.com/cr/cref_files/xamarin-android/Syncfusion.SfDataForm.Android~Syncfusion.Android.DataForm.SfDataForm~RegisterEditor.html?) as CustomNumericEditor to make data form item as custom editor in DataForm.

**C#**

Numeric editor registered to DataForm. 
``` c#
public class DataFormBehavior : Behavior<ContentPage>
{
    SfDataForm dataForm;
    protected override void OnAttachedTo(ContentPage bindable)
    {
        base.OnAttachedTo(bindable);
        dataForm = bindable.FindByName<SfDataForm>("dataForm");
        dataForm.RegisterEditor("Numeric", new CustomNumericEditor(dataForm));
        dataForm.RegisterEditor("Amount", "Numeric");
    }
}
```
