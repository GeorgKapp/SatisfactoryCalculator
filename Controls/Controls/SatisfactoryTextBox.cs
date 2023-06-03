// ReSharper disable HeapView.BoxingAllocation
using System.Linq;
using System.Windows.Input;

namespace SatisfactoryCalculator.Controls.Controls;

public class SatisfactoryTextBox : TextBox
{
	static SatisfactoryTextBox()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(SatisfactoryTextBox), new FrameworkPropertyMetadata(typeof(SatisfactoryTextBox)));
	}

	public SatisfactoryTextBox()
	{
		PreviewTextInput += OnPreviewTextInput;
		DataObject.AddPastingHandler(this, OnClipboardPaste);
	}
	
	public bool IsOverclock
	{
		get => (bool)GetValue(IsOverclockProperty);
		set => SetValue(IsOverclockProperty, value);
	}
	
	private static readonly DependencyProperty IsOverclockProperty = 
		DependencyProperty.Register(
			nameof(IsOverclock), 
			typeof(bool), 
			typeof(SatisfactoryTextBox), 
			new PropertyMetadata(
				false
			));

	private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
	{
		if (!IsOverclock)
			return;
		
		var proposedText = GetProposedText(sender, e);
		if (!IsValidOverclockText(proposedText))
		{
			e.Handled = true;
		}
	}
	
	private void OnClipboardPaste(object sender, DataObjectPastingEventArgs e)
	{
		if (!IsOverclock)
			return;
		
		if (!e.DataObject.GetDataPresent(DataFormats.Text)) 
			return;
		
		var proposedText = GetProposedText(sender, e);
		if (!IsValidOverclockText(proposedText))
		{
			e.CancelCommand();
		}
	}
	
	private bool IsValidOverclockText(string proposedText)
	{
		//Allow empty strings
		if (string.IsNullOrEmpty(proposedText))
			return true;

		//Dont allow invalid characters
		// ReSharper disable once HeapView.ClosureAllocation
		var decimalSeperator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
		if (!proposedText.All(c => $"0123456789{decimalSeperator}".Contains(c)))
			return false;

		//Dont allow invalid doubles
		if (!double.TryParse(proposedText, out var parsedClockSpeed))
			return false;
		
		//Dont allow doubles that are not between 1 and 250
		if (parsedClockSpeed is < 0 or > 250)
			return false;
		
		//Dont allow doubles with more than 4 decimal digits
		// ReSharper disable once StringIndexOfIsCultureSpecific.1
		var decimalSeparatorIndex = proposedText.IndexOf(decimalSeperator);
		if (decimalSeparatorIndex >= 0)
		{
			var decimalPlaces = proposedText.Length - decimalSeparatorIndex - 1;
			if (decimalPlaces > 4)
				return false;
		}

		//Dont allow comma seperator on 250 because of limit
		if (parsedClockSpeed == 250 && proposedText.EndsWith(decimalSeperator))
			return false;

		return true;
	}
	
	#region ParseIncomingText
	private string GetProposedText(object sender, DataObjectPastingEventArgs e)
	{
		var clipboardText = (e.DataObject.GetData(DataFormats.Text) as string)!;
		return GetProposedText(sender, clipboardText);
	}
	
	private string GetProposedText(object sender, TextCompositionEventArgs e)
	{
		return GetProposedText(sender, e.Text);
	}
	
	private string GetProposedText(object sender, string inputText)
	{
		var textBox = (TextBox)sender;

		var caretIndex = textBox.CaretIndex;
		var text = textBox.Text;

		var leftPart = text[..caretIndex];
		var rightPart = text[(caretIndex + textBox.SelectionLength)..];

		return leftPart + inputText + rightPart;
	}
	#endregion
}