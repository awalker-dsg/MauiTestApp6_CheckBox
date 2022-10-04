namespace MauiTestApp6_CheckBox
{
    internal class XCheckBoxWorkaround : CheckBox
    {
        public XCheckBoxWorkaround()
        {
            SetColor();

            // This is part of a work-around for https://github.com/dotnet/maui/issues/10469.
            // Calling SetColor twice is necessary to ensure the color is set correctly regardless
            // of whether the initial state of the checkbox is checked or unchecked. 
            SetColor();

            CheckedChanged += XCheckBox_CheckedChanged;
        }

        public virtual Color CheckedColor   => Colors.Green;
        public virtual Color UncheckedColor => Colors.Red;

        private void XCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            SetColor();
        }

        protected void SetColor()
        {
            Color = IsChecked ? CheckedColor : UncheckedColor;

            // This is part of a work-around for https://github.com/dotnet/maui/issues/10469.
            // For some reason, setting the MAUI CheckBox control's Color only works if you
            // set it and then set it to a different color. We could choose any color not in 
            // use, but to ensure that the color is different the original color is used but
            // the Alpha value is tweaked.
            Color = new Color(Color.Red, Color.Green, Color.Blue, 0.9f * Color.Alpha);
        }
    }
}