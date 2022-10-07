namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class CalculationService
{
    public double? CalculateAmountPerMinte(Form? form, double? amount, double manufactoringDuration)
    {
        if (amount is null)
            return null;

        amount = CalculateAmount(form, amount);

        double factor = 60.0 / manufactoringDuration;
        return amount * factor;
    }

    public double CalculateAmount(Form? form, double sourceInput)
    {
        if (form is not null && (form == Form.Liquid || form == Form.Gas))
            sourceInput /= 1000;

        return sourceInput;
    }

    public double? CalculateAmount(Form? form, double? sourceInput) =>
        sourceInput is not null
            ? CalculateAmount(form, sourceInput.Value)
            : null;
}
