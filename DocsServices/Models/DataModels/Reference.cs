namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class Reference : IBase
{
	public Reference(string className, double amount)
	{
		ClassName = className;
		Amount = amount;
	}

	public string ClassName { get; set; }
	public double Amount { get; set; }
}
