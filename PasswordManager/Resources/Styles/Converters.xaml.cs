using PasswordManager.Model.Converter;

namespace PasswordManager.Resources.Styles
{
    public partial class Converters : ResourceDictionary
    {
        public Converters()
        {
            InitializeComponent();

            //If initialized in xaml throws an exception
            Add("FirstValidationConverter", new FirstValidationConverter());
        }
    }
}